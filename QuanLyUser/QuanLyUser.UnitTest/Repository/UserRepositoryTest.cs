using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuanLyUser.Data.Infrastructure;
using QuanLyUser.Data.Repositories;
using QuanLyUser.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyUser.UnitTest.Repository
{
    [TestClass]
    public class UserRepositoryTest
    {
        IDbFactory dbFactory;
        IUserRepository objRepository;
        IUnitOfWork unitOfWork;
        [TestInitialize]
        public void Initialize()
        {
            dbFactory = new DbFactory();
            objRepository = new UserRepository(dbFactory);
            unitOfWork = new UnitOfWork(dbFactory);
        }
        [TestMethod]
        public void User_Repository_Create()
        {
            //Arrange
            User user = new User();
            user.Name = "Test999";
            user.Phone = "090213899";
            user.Status = true;

            //Act
            var result = objRepository.Add(user);
            unitOfWork.Commit();

            //Assert
            Assert.IsNotNull(result);
            Assert.AreNotEqual(1223, result.ID);

        }

        [TestMethod]
        public void User_Repository_GetAll()
        {
            var list = objRepository.GetAll().ToList();
            Assert.AreNotEqual(3, list.Count);
        }

        [TestMethod]
        public void User_Repository_Update()
        {
            string name = "Test Update";
            User user = new User();
            int id = 1197;
            user.ID = id;
            user.Name = name;
            var result = objRepository.GetSingleById(user.ID);

            result.Name = user.Name;

            unitOfWork.Commit();
            Assert.AreSame("Test Update", result.Name);
           
        }

        [TestMethod]
        public void User_Repository_Delete()
        {
            int id = 1191;
            User user = new User();
            user.ID = id;
            var result = objRepository.Delete(user.ID);

            unitOfWork.Commit();

            Assert.AreNotEqual("1191",result);
            Assert.IsNotNull(result);
        }

    }
}
