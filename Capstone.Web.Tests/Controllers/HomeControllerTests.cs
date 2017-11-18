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

namespace Capstone.Web.Controllers.Tests
{
    [TestClass()]
    public class HomeControllerTests
    {
        private Mock<IUserDAL> mockUserDal = new Mock<IUserDAL>();

        [TestMethod()]
        public void HomeController_IndexAction_ReturnIndexView()
        {
            var controller = new HomeController(mockUserDal.Object);

            var result = controller.Index() as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.ViewName);
        }


        [TestMethod()]
        public void HomeController_LoginAction_ReturnLoginRegisterView()
        {
            var controller = new HomeController(mockUserDal.Object);

            var result = controller.LoginRegister() as ViewResult;

            mockUserDal.Setup(m => m.SelectUser(It.IsAny<string>())).Returns(new UserModel());

            Assert.IsNotNull(result);
            Assert.AreEqual("LoginRegister", result.ViewName);
        }


        [TestMethod()]
        public void HomeController_LoginActionPost_ReturnsUserInfo()
        { 
            var newUser = new UserModel();
            newUser.UserName = "fakeUser@fakeEmail.com";
            newUser.FirstName = "FakeFisrtName";
            newUser.LastName = "FakeLastName";
            newUser.Password = "FakePassWord123!";
            newUser.PasswordSalt = "f1sdThz9oDg=";

            var controller = new HomeController(mockUserDal.Object);

            var savedUser = mockUserDal.Setup(m => m.SelectUser(It.IsAny<string>())).Returns(newUser);

            var result = controller.LoginRegister(newUser, 0) as ViewResult;

            Assert.AreEqual("fakeUser@fakeEmail.com", savedUser);
            Assert.AreEqual("Index", result.ViewName);
        }
    }
}