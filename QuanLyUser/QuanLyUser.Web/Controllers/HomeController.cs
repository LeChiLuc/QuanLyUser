
using QuanLyUser.Common;
using QuanLyUser.Data;
using QuanLyUser.Model.Models;
using QuanLyUser.Service;
using QuanLyUser.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace QuanLyUser.Web.Controllers
{
    public class HomeController : BaseController
    {

        private IUserService _userService;
        public HomeController(IUserService userService)
        {
            this._userService = userService;
        }
        public ActionResult Index()
        {
            return View();
        }
        //Hiển thị dữ liệu trên table đồng thời phân trang
        [HttpGet]
        public JsonResult LoadData(string keyword, DateTime? fromDate, DateTime? toDate, int page, int pageSize=2)
        {
            int totalRow = 0;
            var db = _userService.LoadData(keyword ,fromDate,toDate,page,pageSize,out totalRow).ToList();
            return Json(new
            {
                data = db,
                total = totalRow,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }
        //Tìm bản ghi có id là:
        [HttpGet]
        public JsonResult GetDetail(int id)
        {
            var user = _userService.GetById(id);
            return Json(new
            {
                data = user,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Trade(string strEmployee)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            TradeViewModel user = serializer.Deserialize<TradeViewModel>(strEmployee);
            bool status = false;
            string message = string.Empty;
            try
            {
                _userService.spTrade(user.IDA,user.IDB,user.Amount);
                status = true;
            }
            catch (Exception ex)
            {
                status = false;
                message = ex.Message;
            }

            return Json(new
            {
                status = status,
                message = message
            }, JsonRequestBehavior.AllowGet);
        }


        public JsonResult CheckName(string UserName)
        {
            System.Threading.Thread.Sleep(1000);
            //var userName = _userService.CheckForDuplication(UserName);
            var checkUser = _userService.GetById(UserName);
            if(checkUser==null)
            {
                return Json(new
                {
                    data = checkUser,
                    status = true
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new
                {
                    data = checkUser,
                    status = false
                }, JsonRequestBehavior.AllowGet);
            }
            
        }
        //SaveData bao gồm cả thêm mới và cập nhật bản ghi
        [HttpPost]
        public JsonResult SaveData(string strEmployee)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            UserViewModel userViewModel = serializer.Deserialize<UserViewModel>(strEmployee);
            var user = new User();
            user.Name = userViewModel.Name;
            user.Email = userViewModel.Email;
            user.UserName = userViewModel.UserName;
            user.Password = userViewModel.Password;
            user.CreatedDate = userViewModel.CreatedDate;
            user.Phone = userViewModel.Phone;
            user.ID = userViewModel.ID;
            user.Status = userViewModel.Status;
            bool status = false;
            string message = string.Empty;
            var checkUser = _userService.GetById(user.UserName);
            
            //Thêm người dùng nếu id = 0
            if (user.ID == 0)
            {
                if(ModelState.IsValid)
                {
                    if (checkUser == null)
                    {
                        var encryptedMd5Pas = Encryptor.MD5Hash(user.Password);
                        user.Password = encryptedMd5Pas;
                        user.CreatedDate = DateTime.Now;
                        try
                        {
                            _userService.Add(user);
                            status = true;
                        }
                        catch (Exception ex)
                        {
                            status = false;
                            message = ex.Message;
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "UserName đã được sử dụng.");
                    }
                }
            }
            else
            {
                //cập nhật người dùng vào data nếu id # 0
                var encryptedMd5Pas = Encryptor.MD5Hash(user.Password);
                var entity = _userService.GetById(user.ID);
                entity.Password = encryptedMd5Pas;
                entity.Name = user.Name;
                entity.Email = user.Email;
                entity.Phone = user.Phone;
                entity.UserName = user.UserName;
                entity.Status = user.Status;
                try
                {
                    _userService.Save();
                    status = true;
                }
                catch (Exception ex)
                {
                    status = false;
                    message = ex.Message;
                }

            }
            return Json(new
            {
                status = status,
                message = message
            }, JsonRequestBehavior.AllowGet);
        }
        ////Xóa bản ghi
        [HttpPost]
        public JsonResult Delete(int id)
        {
            _userService.Delete(id);
            try
            {
                _userService.Save();
                return Json(new
                {
                    status = true
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    status = false,
                    message = ex.Message
                });
            }

        }
    }
}