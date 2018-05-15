using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClearBank.DeveloperTest.Types
{
    public interface IPaymentRequest
    {
        string CreditorAccountNumber { get; set; }

        string DebtorAccountNumber { get; set; }

        decimal Amount { get; set; }

        DateTime PaymentDate { get; set; }

        PaymentScheme PaymentScheme { get; set; }
    }
}
