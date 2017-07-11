using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QuanLyUser.Web.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Mời nhập user")]
        public string UserName { set; get; }
        [Required(ErrorMessage = "Mời nhập password")]
        public string Password { set; get; }
    }
}