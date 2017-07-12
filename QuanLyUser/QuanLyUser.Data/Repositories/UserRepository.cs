using QuanLyUser.Data.Infrastructure;
using QuanLyUser.Model.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyUser.Data.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        int Login(string userName, string passWord);
        User GetById(string userName);
        IEnumerable<User> GetKeyword(string keyword, DateTime? toDate, DateTime? fromDate, int page, int pageSize);
        int GetCount(string keyword, DateTime? toDate, DateTime? fromDate);
        long spAddUser(string name, string email, string userName, string password, string phone, bool status);
        User spTrade(int idA,int idB,int amount);
    }

    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(IDbFactory dbFactory)
            : base(dbFactory)
        {
        }

        public User GetById(string userName)
        {
            return DbContext.Users.SingleOrDefault(x => x.UserName == userName);
        }

        public int GetCount(string keyword, DateTime? toDate, DateTime? fromDate)
        {
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@keyword",keyword),
                new SqlParameter("@fromDate",fromDate ?? (object)DBNull.Value),
                new SqlParameter("@toDate",toDate ?? (object)DBNull.Value)
            };
            var datas = DbContext.Database.SqlQuery<int>("SearchUserCount @keyword,@fromDate,@toDate", parameters).FirstOrDefault();

            return datas;
        }

        public IEnumerable<User> GetKeyword(string keyword, DateTime? toDate, DateTime? fromDate, int page, int pageSize)
        {
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@keyword",keyword),
                new SqlParameter("@fromDate",fromDate ?? (object)DBNull.Value),
                new SqlParameter("@toDate",toDate ?? (object)DBNull.Value),
                new SqlParameter("@page",page-1),
                new SqlParameter("@pageSize",pageSize)
            };
            //parameters[5].Direction = ParameterDirection.Output;
            var datas = DbContext.Database.SqlQuery<User>("SearchUser @keyword,@fromDate,@toDate,@page,@pageSize", parameters).ToList();
            //int totalRow = parameters[5].Value == null ? 0 : Convert.ToInt32(parameters[5].Value);
            return datas;
        }

        public int Login(string userName, string passWord)
        {
            var result = DbContext.Users.SingleOrDefault(x => x.UserName == userName);
            if (result == null)
            {
                return 0;
            }
            else
            {
                if (result.Status == false)
                {
                    return -2;
                }
                else
                {
                    if (result.Password == passWord)
                        return 1;
                    else
                        return -1;
                }
            }
        }

        public long spAddUser(string name,string email,string userName,string password,string phone,bool status)
        {
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@Name",name),
                new SqlParameter("@Email",email),
                new SqlParameter("@UserName",userName),
                new SqlParameter("@Password",password),
                new SqlParameter("@Phone",phone),
                new SqlParameter("@Status",status)
            };

            var datas = DbContext.Database.SqlQuery<long>("spAddUser @Name,@Email,@UserName,@Password,@Phone,@Status", parameters).FirstOrDefault();
            return datas;
        }

        public User spTrade(int idA, int idB, int amount)
        {
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@User_Id_A",idA),
                new SqlParameter("@User_Id_B",idB),
                new SqlParameter("@Amount",amount)
            };

            var datas = DbContext.Database.ExecuteSqlCommand("spTradeUser @User_Id_A,@User_Id_B,@Amount", parameters);
            return new User();
        }
    }
}
