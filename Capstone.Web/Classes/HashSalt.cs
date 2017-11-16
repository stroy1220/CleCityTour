using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capstone.Web.Models;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Web.Classes
{
    public class HashSalt
    {
        static string HashPasswordWithMD5(string password)
        {
            MD5CryptoServiceProvider md5Provider = new MD5CryptoServiceProvider();

            byte[] source = ASCIIEncoding.ASCII.GetBytes(password);
            byte[] hash = md5Provider.ComputeHash(source);

            return Convert.ToBase64String(hash);
        }

        static string HashPasswordWithPBKDF2(string password, byte[] salt, int workFactor)
        {
            // Creates the crypto service provider and provides the salt - usually used to check for a password match
            Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, salt, workFactor);

            byte[] hash = rfc2898DeriveBytes.GetBytes(20);      //gets the hased password

            return Convert.ToBase64String(salt) + "|" + Convert.ToBase64String(hash);
        }

        static string HashPasswordWithPBKDF2(string password, int saltSize, int workFactor)
        {
            //Creates the crypto service provider and says to use a random salt of size "saltSize"
            Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, saltSize, workFactor);

            byte[] hash = rfc2898DeriveBytes.GetBytes(20);      //gets the hashed password
            byte[] salt = rfc2898DeriveBytes.Salt;              //gets the random salt

            return Convert.ToBase64String(salt) + "|" + Convert.ToBase64String(hash);
        }
    }
}