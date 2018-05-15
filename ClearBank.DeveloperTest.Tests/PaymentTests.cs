using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClearBank.DeveloperTest.Data;
using ClearBank.DeveloperTest.Services;
using ClearBank.DeveloperTest.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Ninject.Modules;

namespace ClearBank.DeveloperTest.Tests
{
    [TestClass]
    public class PaymentTests
    {
        Mock<IDataStore> dataStoreMock;
        Mock<IAccount> accountMock;
        Mock<IPaymentRequest> requestMock;

        [TestInitialize]
        public void Setup()
        {
            dataStoreMock = new Mock<IDataStore>();
            accountMock = new Mock<IAccount>();
            requestMock = new Mock<IPaymentRequest>();

            accountMock.Setup(x => x.Status).Returns(AccountStatus.Live);
            accountMock.Setup(x => x.Balance).Returns(10000000m);
            accountMock.Setup(x => x.AllowedPaymentSchemes).Returns(new List<PaymentScheme> { PaymentScheme.Bacs, PaymentScheme.Chaps });
            requestMock.Setup(x => x.PaymentScheme).Returns(PaymentScheme.Bacs);
            requestMock.Setup(x => x.Amount).Returns(10m);
            dataStoreMock.Setup(x => x.GetAccount(It.IsAny<string>())).Returns(accountMock.Object);
        }

        [TestMethod]
        public void Payment_Succeeds()
        {

            // Arrange
            PaymentService service = new PaymentService(dataStoreMock.Object);

            // Act
            MakePaymentResult result = service.MakePayment(requestMock.Object);

            // Assert
            Assert.IsTrue(result.Success, "Should succeed when all conditions are met");
        }

        [TestMethod]
        public void Payment_FailsWhenAccountIsDisabled()
        {

            // Arrange
            PaymentService service = new PaymentService(dataStoreMock.Object);
            accountMock.Setup(x => x.Status).Returns(AccountStatus.Disabled);

            // Act
            MakePaymentResult result = service.MakePayment(requestMock.Object);

            // Assert
            Assert.IsFalse(result.Success, "Should fail when account is disabled");
        }

        [TestMethod]
        public void Payment_FailsWhenAccountIsNull()
        {

            // Arrange
            PaymentService service = new PaymentService(dataStoreMock.Object);
            dataStoreMock.Setup(x => x.GetAccount(It.IsAny<string>())).Returns<IAccount>(null);

            // Act
            MakePaymentResult result = service.MakePayment(requestMock.Object);

            // Assert
            Assert.IsFalse(result.Success, "Should fail when account is null");
        }

        [TestMethod]
        public void Payment_FailsWhenBalanceIsLessThanAmount()
        {

            // Arrange
            PaymentService service = new PaymentService(dataStoreMock.Object);
            accountMock.Setup(x => x.Balance).Returns(9m);

            // Act
            MakePaymentResult result = service.MakePayment(requestMock.Object);

            // Assert
            Assert.IsFalse(result.Success, "Should fail when account balance is less than amount");
        }

        [TestMethod]
        public void Payment_FailsWhenPaymentSchemeNotAvailable()
        {
            // Arrange
            PaymentService service = new PaymentService(dataStoreMock.Object);
            requestMock.Setup(x => x.PaymentScheme).Returns(PaymentScheme.FasterPayments);

            // Act
            MakePaymentResult result = service.MakePayment(requestMock.Object);

            // Assert
            Assert.IsFalse(result.Success, "Should fail when payment scheme not available");
        }
    }
}
