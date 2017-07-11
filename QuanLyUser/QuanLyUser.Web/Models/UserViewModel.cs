using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLyUser.Web.Models
{
    public class UserViewModel
    {
        public int ID { set; get; }

        public string Name { set; get; }

        public string Email { set; get; }

        public string UserName { get; set; }

        public string Password { set; get; }

        public DateTime? CreatedDate { set; get; }

        public string Phone { set; get; }

        public bool? Status { get; set; }
    }
}