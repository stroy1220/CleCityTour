using Microsoft.VisualStudio.TestTools.UnitTesting;
using Capstone.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Capstone.Web.DAL;
using System.Data.SqlClient;
using Moq;
using Capstone.Web.Models;

namespace Capstone.Web.Tests.Dal
{
    [TestClass]
    public class UserDalTests
    {
      

        [TestMethod]
        public void SaveUserTest()
        {
            UserDAL dal = new UserDAL();
            UserModel testUser = new UserModel();
            testUser.Admin = false;
            testUser.FirstName = "FakeFirst";
            testUser.LastName = "FakeLast";
            testUser.Password = "FakePassword";
            testUser.PasswordSalt = "FakeSalt";
            testUser.UserName = "Fake@Fake.com";




            Assert.IsTrue(dal.SaveUser(testUser) == true);

        }
    }
}
