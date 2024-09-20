using Dapper;
using Dapper.Contrib.Extensions;
using FontDecoder.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Data.Sqlite;
using NETCore.Encrypt;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography.Xml;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FontDecoder.Service
{
    public class UserService : IUserService
    {
        public static readonly string connectionString = "Data Source=decoder.db";
        public void AddUser(User user)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();                
                user.Password = EncryptProvider.Sha1(user.Password);
                connection.Insert<User>(user);                
            }
        }

        public bool Authorize(string username, string password,out User user)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                password = EncryptProvider.Sha1(password);
                user = connection.QueryFirstOrDefault<User>("select * from user where username=@username and password=@password", new { username, password });
                if (user != null)
                {
                    return true;
                }
                return false;
            }
        }
        public User GetUser(string username)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var user = connection.QueryFirst<User>("select * from user where username=@username", new { username});
                return user;
            }
        }

        public int SetCredit(string username, int number)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                connection.Execute("update user set credit=credit+@number where username=@username", new { username, number });
                return connection.ExecuteScalar<int>("select credit from user where username=@username", new { username });
            }
        }

        public void RemoveUser(string username)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                connection.Execute("delete from user where username=@username", new { username });
            }
        }

        public void UpdateUser(User user)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                user.Password = EncryptProvider.Sha1(user.Password);
                connection.Update<User>(user);
            }
        }

        public int GetCredit(string username)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var credit = connection.ExecuteScalar<int>("select credit from user where username=@username", new { username });
                return credit;
            }
        }
    }
}
