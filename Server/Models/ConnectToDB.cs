using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Server.Models
{
    internal class ConnectToDB
    {
        private static string _connectionStr = @"Server=DESKTOP-440KLQF\SQLEXPRESS;Database=ServerList;Trusted_Connection=True;";

        //public ConnectToDB() { }

        public static List<User> GetUsers()
        {
            using (IDbConnection db = new SqlConnection(_connectionStr))
            {
                return db.Query<User>("SELECT * FROM Users").ToList();
            }
        }

        public static void Create(User user)
        {
            using (IDbConnection db = new SqlConnection(_connectionStr))
            {
                var sqlQuery = "INSERT INTO Users (Email, Password) VALUES(@Email, @Password)";
                db.Execute(sqlQuery, user);
            }
        }

        public static bool LoginCheck(string email)
        {
            foreach (var item in GetUsers())
            {
                if (item.Email == email)
                {
                    return true;
                }              
            }
            return false;
        }

        public static bool PasswordCheck(string password)
        {
            foreach (var item in GetUsers())
            {
                if(PasswordCrypt.Veryfy(password, item.Password))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
