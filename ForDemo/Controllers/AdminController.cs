using ForDemo.Models;
using ForDemo.Utility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ForDemo.Controllers
{
    public class AdminController : Controller
    {
        [MyAuthorize]
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 查詢資料
        /// </summary>
        /// <param name="Model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Query()
        {
            try
            {
                List<UserViewModel> users = new DataUtility().GetData<UserViewModel>(Server.MapPath("~/Data/UserData.json"));
                return Json(JsonConvert.SerializeObject(new { rows = users }), JsonRequestBehavior.AllowGet); 
            }
            catch (Exception ex)
            { 
                throw ex;
            }
        } 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserViewModel Model)
        {
            try
            {
                var status = Enum.EnumUtility.UserRoles.GetValues(typeof(Enum.EnumUtility.UserRoles)).Cast<Enum.EnumUtility.UserRoles>()
                      .Select(se => new
                      {
                          value = (int)se,
                          text = se.ToString()
                      }); ;
                var result = new SelectList(status, "value", "text");
                ViewBag.Roles = result; 
                return View("Create", Model);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NewData(UserViewModel Model)
        {
            try
            {
                List<UserViewModel> users = new DataUtility().GetData<UserViewModel>(Server.MapPath("~/Data/UserData.json"));
                 users.Add(Model);
                string content = JsonConvert.SerializeObject(users);
                new DataUtility().WriteToJsonFile(Server.MapPath("~/Data/UserData.json"), content);
                return Json(JsonConvert.SerializeObject(new { rows = users }), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            } 
        }


        /// <summary>
        /// Remove
        /// </summary>
        /// <param name="Model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Remove(UserViewModel Model)
        {
            try
            {
                List<UserViewModel> users = new DataUtility().GetData<UserViewModel>(Server.MapPath("~/Data/UserData.json"));
                users = users.Where(x =>  x.UserId != Model.UserId).ToList();
                string content = JsonConvert.SerializeObject(users);
                new DataUtility().WriteToJsonFile(Server.MapPath("~/Data/UserData.json"), content);
                return Json(JsonConvert.SerializeObject(new { result = users }));
            }
            catch (Exception ex)
            {
                 throw ex;
            }
             
        }
    }
}