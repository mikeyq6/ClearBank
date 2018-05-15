using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Data
{
    public interface IDataStore
    {
        IAccount GetAccount(string accountNumber);

        void UpdateAccount(IAccount account);
    }

    public enum DataStoreTypes
    {
        Account,
        Backup
    }
}
