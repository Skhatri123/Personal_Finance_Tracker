using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PersonalFinanceTracker.Models;

namespace PersonalFinanceTracker.Database
{
    public class TransactionDatabaseHelper
    {
        private readonly SQLiteAsyncConnection _database;

        public TransactionDatabaseHelper()
        {
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "transactions.db3");
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Transaction>().Wait();
        }

        public Task<int> AddTransaction(Transaction transaction)
        {
            return _database.InsertAsync(transaction);
        }

        public async Task<List<Transaction>> GetTransactions()
        {
            // return _database.Table<Transaction>().ToListAsync();
            return await _database.Table<Transaction>()
                .Where(t => t.UserId == App.LoggedInUserId)  // Filter by logged-in user
                .ToListAsync();
        }

        public Task<int> DeleteTransaction(Transaction transaction)
        {
            return _database.DeleteAsync(transaction);
        }
    }
}
