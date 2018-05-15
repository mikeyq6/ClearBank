﻿using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Data
{
    public class AccountDataStore : IDataStore
    {
        public IAccount GetAccount(string accountNumber)
        {
            // Access database to retrieve account, code removed for brevity 
            return new Account();
        }

        public void UpdateAccount(IAccount account)
        {
            // Update account in database, code removed for brevity
        }
    }
}
