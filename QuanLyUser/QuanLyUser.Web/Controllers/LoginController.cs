using QuanLyUser.Common;
using QuanLyUser.Service;
using QuanLyUser.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyUser.Web.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        IUserService _userService;
        public LoginController(IUserService userService)
        {
            this._userService = userService;
        }
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                //gọi Login trong UserDao để xử lý tài khoản và mật khẩu
                //Đồng thời mã hóa mật khẩu
                var result = _userService.Login(model.UserName, Encryptor.MD5Hash(model.Password));
                //result == 1 đăng nhập đúng chuyển đến trang chủ
                if (result == 1)
                {
                    var user = _userService.GetById(model.UserName);
                    var userSession = new UserLogin();
                    userSession.UserName = user.UserName;
                    userSession.UserID = user.ID;
                    //gán session
                    Session.Add(CommonConstants.USER_SESSION, userSession);
                    return RedirectToAction("Index", "Home");
                }
                else if (result == 0)
                {
                    ModelState.AddModelError("", "Tài khoản không tồn tại.");
                }
                else if (result == -2)
                {
                    ModelState.AddModelError("", "Tài khoản đang bị khóa.");
                }
                else if (result == -1)
                {
                    ModelState.AddModelError("", "Mật khẩu không đúng.");
                }
                else
                {
                    ModelState.AddModelError("", "Đăng nhập không đúng.");
                }
            }
            return View("Index");
        }
    }
}