using System.Collections.Generic;

namespace ClearBank.DeveloperTest.Types
{
    public class Account : IAccount
    {
        public string AccountNumber { get; set; }
        public decimal Balance { get; set; }
        public AccountStatus Status { get; set; }
        public List<PaymentScheme> AllowedPaymentSchemes { get; set; }
    }
}
