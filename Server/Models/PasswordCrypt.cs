using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Models
{
    internal class PasswordCrypt
    {
        public static string Generate(string pass)
        {
            return BCrypt.Net.BCrypt.HashPassword(pass);
        }
        public static bool Veryfy(string pass, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(pass, hash);
        }
    }
}
