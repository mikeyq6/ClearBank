using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClearBank.DeveloperTest.Types
{
    public interface IAccount
    {
        string AccountNumber { get; set; }
        decimal Balance { get; set; }
        AccountStatus Status { get; set; }
        List<PaymentScheme> AllowedPaymentSchemes { get; set; }
    }
}
