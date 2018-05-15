using ClearBank.DeveloperTest.Data;
using ClearBank.DeveloperTest.Types;
using System.Configuration;
using Ninject;

namespace ClearBank.DeveloperTest.Services
{
    public class PaymentService : IPaymentService
    {
        protected IDataStore DataStore { get; private set; }
        
        // Setup for constructor injection
        public PaymentService(IDataStore dataStore)
        {
            DataStore = dataStore;
        }

        public MakePaymentResult MakePayment(IPaymentRequest request)
        {
            IAccount account = DataStore.GetAccount(request.DebtorAccountNumber);

            // Assume will be unsuccessful
            var result = new MakePaymentResult() { Success = false };

            // Conditions for success
            if (account != null && account.Status == AccountStatus.Live && account.Balance >= request.Amount &&
                    account.AllowedPaymentSchemes.Contains(request.PaymentScheme))
            {
                result.Success = true;
            }

            if (result.Success)
            {
                account.Balance -= request.Amount;
                DataStore.UpdateAccount(account);
            }

            return result;
        }
        
    }
}
