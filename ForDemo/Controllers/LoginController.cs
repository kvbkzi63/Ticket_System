using ForDemo.Models;
using ForDemo.Utility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ForDemo.Controllers
{
    public class LoginController : Controller
    {
        [AllowAnonymous]
        public ActionResult Login()
        { 
            return View();
        }
        [HttpPost] 
        public ActionResult Login(UserViewModel user)
        { 
            if (!string.IsNullOrWhiteSpace(user.UserId) && !string.IsNullOrWhiteSpace(user.UserPd))
            {
                try
                { 
                    List<UserViewModel> users = new DataUtility().GetData<UserViewModel>(Server.MapPath("~/Data/UserData.json"));
                    UserViewModel currentUser = users.Where(x => x.UserId == user.UserId && x.UserPd == user.UserPd).First(); 
                    if (currentUser == null)
                    {
                        TempData["Error"] = "使用者不存在!!";
                        return View();
                    }
                    Session["UserId"] = currentUser.UserId;
                    Session["UserName"] = currentUser.UserName;
                    Session["Roles"] = currentUser.Roles;
                    if ((int)Session["Roles"] == 3)
                    {
                        return RedirectToAction("Index", "Admin"); 
                    }
                    else
                    {
                        return RedirectToAction("Index", "TicketRecord"); 
                    }
                }
                catch (Exception ex)
                {
                    return RedirectToAction("RedirectError", "Login");
                }
            }
            TempData["Error"] = "帳號或密碼錯誤!";

            return View();
        }
        //登出
        public ActionResult SignOut()
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Login", "Login");
        }
        public ActionResult RedirectError()
        {
            ViewBag.Title = "發生錯誤。";
            ViewBag.Errmag = "發生錯誤，請再嘗試一次。";
            Session.Clear();
            Session.Abandon();
            return View();
        }

        [AllowAnonymous]
        /// <summary>
        /// 提供各系統呼叫登出
        /// </summary>
        /// <returns></returns>
        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Login", "Login");
        }
    }
}