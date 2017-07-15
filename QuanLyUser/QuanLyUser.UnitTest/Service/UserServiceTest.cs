using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using QuanLyUser.Data.Infrastructure;
using QuanLyUser.Data.Repositories;
using QuanLyUser.Model.Models;
using QuanLyUser.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyUser.UnitTest.Service
{
    [TestClass]
    public class UserServiceTest
    {
        private Mock<IUserRepository> _mockRepository;
        private Mock<IUnitOfWork> _mockUnitOfWork;
        private IUserService _userService;
        private List<User> _listUser;
        [TestInitialize]
        public void Initialize()
        {
            _mockRepository = new Mock<IUserRepository>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _userService = new UserService(_mockRepository.Object, _mockUnitOfWork.Object);
            _listUser = new List<User>()
            {
                new User(){ID=1,Name="Test1",Status=true},
                new User(){ID=2,Name="Test2",Status=true},
                new User(){ID=3,Name="Test3",Status=true}
            };
        }

        [TestMethod]
        public void User_Service_GetAll()
        {
            //setup method
            _mockRepository.Setup(m => m.GetAll(null)).Returns(_listUser);

            //call action
            var result = _userService.GetAll() as List<User>;

            //compare
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count);
        }

        [TestMethod]
        public void User_Service_Create()
        {
            User user = new User();
            int id = 1;
            user.Name = "Test";
            user.Phone = "091238182";
            user.Status = true;

            _mockRepository.Setup(m => m.Add(user)).Returns((User p) =>
            {
                p.ID = 1;
                return p;
            });

            var result = _userService.Add(user);

            Assert.IsNotNull(result);
        }

        //[TestMethod]
        //public void User_Service_Delete()
        //{
        //    var user = new User();
        //    user.ID = 1185;
        //    _mockRepository.Setup(m => m.Delete(user.ID)).Returns((User p) =>
        //    {
        //        p.ID = 1181;
        //        return p;
        //    });
        //    var result = _userService.Delete(user);

        //    Assert.AreNotEqual(1181, result);
        //    Assert.IsNotNull(result);
        //}
    }
}
