using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using QuanLyUser.Data.Infrastructure;
using QuanLyUser.Data.Repositories;
using QuanLyUser.Model.Models;
using QuanLyUser.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        [TestMethod]
        public void User_Service_Delete()
        {
            var user1 = new User() { ID = 1, Name = "PERSON_CODE=1" };
            var user2 = new User() { ID = 2, Name = "PERSON_CODE=2" };
            var user3 = new User() { ID = 3, Name = "PERSON_CODE=3" };
            var events = new List<User>() { user1, user2, user3};
            _mockRepository.Setup(m => m.Delete(It.IsAny<int>()))
                .Callback((int userId)=>{
                    events.Remove(user2);
                });
            int count = events.Count();
            Assert.AreEqual(3, count);
        }

        //[TestMethod]
        //public void User_Service_Update()
        //{
        //    const int customerId = 5;

        //    var mockCustomer = new Mock<User>();

        //    mockCustomer.SetupGet(x => x.ID)
        //        .Returns(customerId);

        //    _mockRepository.Setup(x => x.GetSingleById(It.IsAny<int>()));

        //    var result = _userService.GetById(customerId);

        //    Assert.AreEqual(customerId, result.ID);
        //}
    }
}
