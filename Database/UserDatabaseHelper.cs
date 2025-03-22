using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PersonalFinanceTracker.Models;

namespace PersonalFinanceTracker.Database
{
    public class UserDatabaseHelper
    {
        private readonly SQLiteAsyncConnection _database;

        public UserDatabaseHelper()
        {
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "users.db3");
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<User>().Wait();
        }

        // Add User (Register)
        public Task<int> AddUser(User user)
        {
            return _database.InsertAsync(user);
        }

        // Check User (Login)
        public async Task<User> GetUser(string username, string password)
        {
            return await _database.Table<User>()
                             .Where(u => u.Username == username && u.Password == password)
                             .FirstOrDefaultAsync();
        }
    }
}
