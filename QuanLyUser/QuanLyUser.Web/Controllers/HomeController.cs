
using QuanLyUser.Common;
using QuanLyUser.Data;
using QuanLyUser.Model.Models;
using QuanLyUser.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

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
        //SaveData bao gồm cả thêm mới và cập nhật bản ghi
        [HttpPost]
        public JsonResult SaveData(string strEmployee)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            User user = serializer.Deserialize<User>(strEmployee);
            bool status = false;
            string message = string.Empty;
            //Thêm người dùng nếu id = 0
            if (user.ID == 0)
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