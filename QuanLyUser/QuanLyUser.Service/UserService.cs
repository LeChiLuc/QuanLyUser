using QuanLyUser.Data.Infrastructure;
using QuanLyUser.Data.Repositories;
using QuanLyUser.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyUser.Service
{
    public interface IUserService
    {
        long Add(User User);

        User GetById(int id);

        void Save();

        User Delete(int id);

        IEnumerable<User> GetAll();

        User spTrade(int idA, int idB, int amount);

        int Login(string userName, string passWord);

        User GetById(string userName);

        //IEnumerable<User> GetKeyword(string keyword, DateTime? toDate, DateTime? fromDate);

        IEnumerable<User> LoadData(string keyword, DateTime? fromDate, DateTime? toDate, int page, int pageSize, out int totalRow);
    }
    public class UserService : IUserService
    {
        private IUserRepository _userRepository;
        private IUnitOfWork _unitOfWork;
        public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
            this._userRepository = userRepository;
        }

        public long Add(User User)
        {
            long user = _userRepository.spAddUser(User.Name,User.Email,User.UserName,User.Password,User.Phone,User.Status.HasValue);
            _unitOfWork.Commit();
            return user;
        }

        public User Delete(int id)
        {
            return _userRepository.Delete(id);
        }

        public IEnumerable<User> GetAll()
        {
            return _userRepository.GetAll();
        }

        public User GetById(string userName)
        {
            return _userRepository.GetById(userName);
        }

        public User GetById(int id)
        {
            return _userRepository.GetSingleById(id);
        }

        //public IEnumerable<User> GetKeyword(string keyword, DateTime? toDate, DateTime? fromDate)
        //{
        //    var model = _userRepository.GetAll();
        //    if (keyword.Count() > 0)
        //    {
        //        model = _userRepository.GetKeyword(keyword, toDate, fromDate);
        //    }
        //    return model;
        //}

        public IEnumerable<User> LoadData(string keyword, DateTime? fromDate, DateTime? toDate, int page, int pageSize, out int totalRow)
        {
            var data = _userRepository.GetKeyword(keyword, toDate, fromDate, page, pageSize);
            totalRow = _userRepository.GetCount(keyword, toDate, fromDate);

            //return data.Skip((page - 1) * pageSize).Take(pageSize);
            return data;
        }

        public int Login(string userName, string passWord)
        {
            return _userRepository.Login(userName, passWord);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public User spTrade(int idA, int idB, int amount)
        {
            return _userRepository.spTrade(idA, idB, amount);
        }
    }
}
