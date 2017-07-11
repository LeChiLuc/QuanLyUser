using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyUser.Data.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        private QuanLyUserDbContext dbContext;

        public QuanLyUserDbContext Init()
        {
            return dbContext ?? (dbContext = new QuanLyUserDbContext());
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }
    }
}
