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
using Capstone.Web;
using System.Transactions;

namespace Capstone.Web.Tests.Dal
{

    [TestClass]
    public class UserDalTests
    {
        //private string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=CityTour;User ID=te_student;Password=sqlserver1";
        //TransactionScope t;

        //[TestInitialize]
        //public void Initialize()
        //{
        //    t = new TransactionScope();
        //}

        //[TestCleanup]
        //public void CleanUp()
        //{
        //    t.Dispose();
        //}

        //[TestMethod]
        //public void SaveUserTest()
        //{
        //    UserModel testUser = new UserModel();
        //    testUser.Admin = false;
        //    testUser.FirstName = "FakeFirst";
        //    testUser.LastName = "FakeLast";
        //    testUser.Password = "FakePassword";
        //    testUser.PasswordSalt = "FakeSalt";
        //    testUser.UserName = "Take@Fake.com";

        //    UserDAL dal = new UserDAL(connectionString);

        //    bool output = dal.SaveUser(testUser);

        //    Assert.IsTrue(output);

        //}
    }
}
