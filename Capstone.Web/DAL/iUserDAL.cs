using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capstone.Web.Models;

namespace Capstone.Web.DAL
{
    public interface IUserDAL
    {
        bool SaveUser(UserModel user);
        bool DeleteUser(string userName, string password);
        bool UpdatePassword(string userName);
        UserModel SelectUser(string userName);


    }
}