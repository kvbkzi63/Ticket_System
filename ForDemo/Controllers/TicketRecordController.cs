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
    public class TicketRecordController : Controller
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
                List<TicketRecordViewModel> users = new DataUtility().GetData<TicketRecordViewModel>(Server.MapPath("~/Data/TicketRecordData.json"));
                return Json(JsonConvert.SerializeObject(new { rows = users }), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TicketRecordViewModel Model)
        {
            try
            {
                var Priority = Enum.EnumUtility.Priority.GetValues(typeof(Enum.EnumUtility.Priority)).Cast<Enum.EnumUtility.Priority>()
                      .Select(se => new
                      {
                          value = (int)se,
                          text = se.ToString()
                      });
                var TicketStatus = Enum.EnumUtility.TicketStatus.GetValues(typeof(Enum.EnumUtility.TicketStatus)).Cast<Enum.EnumUtility.TicketStatus>()
                      .Select(se => new
                      {
                          value = (int)se,
                          text = se.ToString()
                      }); ;
                var TicketType = Enum.EnumUtility.TicketType.GetValues(typeof(Enum.EnumUtility.TicketType)).Cast<Enum.EnumUtility.TicketType>()
                      .Select(se => new
                      {
                          value = (int)se,
                          text = se.ToString()
                      });

                if ((Enum.EnumUtility.UserRoles)Session["Roles"] == Enum.EnumUtility.UserRoles.PM)
                {
                    TicketType = TicketType.Where(x => x.text != Enum.EnumUtility.TicketType.Test_Case.ToString()).ToList();
                }
                else if ((Enum.EnumUtility.UserRoles)Session["Roles"] == Enum.EnumUtility.UserRoles.QA)
                {
                    TicketType = TicketType.Where(x => x.text != Enum.EnumUtility.TicketType.Feature_Request.ToString()).ToList();
                }
                else
                {
                    TicketType = TicketType.Where(x => x.text != Enum.EnumUtility.TicketType.Feature_Request.ToString() && x.text != Enum.EnumUtility.TicketType.Test_Case.ToString()).ToList(); 
                }
                var Severity = Enum.EnumUtility.Severity.GetValues(typeof(Enum.EnumUtility.Severity)).Cast<Enum.EnumUtility.Severity>()
                      .Select(se => new
                      {
                          value = (int)se,
                          text = se.ToString()
                      }); ;
                ViewBag.Priority = new SelectList(Priority, "value", "text"); 
                ViewBag.TicketType = new SelectList(TicketType, "value", "text");
                ViewBag.Severity = new SelectList(Severity, "value", "text");
                ViewBag.Title = (string.IsNullOrWhiteSpace(Model.Create_User)) ? "新增資料" : "編輯資料";
                return View("Create", Model); 
            }
            catch (Exception ex)
            {
                throw ex;
            }

        } 

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NewData(TicketRecordViewModel Model)
        {
            try
            { 
                List<TicketRecordViewModel> record = new DataUtility().GetData<TicketRecordViewModel>(Server.MapPath("~/Data/TicketRecordData.json"));
                Model.Ticket_Seq = record.Count() + 1;
                Model.Create_User = Session["UserName"].ToString();
                Model.Modified_User = Session["UserName"].ToString();
                Model.Create_Time = DateTime.Now.ToString("yyyy/MM/dd");
                Model.Modified_Time = DateTime.Now.ToString("yyyy/MM/dd");
                record.Add(Model);
                string content = JsonConvert.SerializeObject(record);
                new DataUtility().WriteToJsonFile(Server.MapPath("~/Data/TicketRecordData.json"), content);
                return Json(JsonConvert.SerializeObject(new { rows = record }), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveData(TicketRecordViewModel Model)
        {
            try
            {
                List<TicketRecordViewModel> record = new DataUtility().GetData<TicketRecordViewModel>(Server.MapPath("~/Data/TicketRecordData.json"));
                Model.Modified_User = Session["UserName"].ToString(); 
                Model.Modified_Time = DateTime.Now.ToString("yyyy/MM/dd");
                record = record.Where(x => x.Ticket_Seq != Model.Ticket_Seq).ToList();
                record.Add(Model);
                string content = JsonConvert.SerializeObject(record);
                new DataUtility().WriteToJsonFile(Server.MapPath("~/Data/TicketRecordData.json"), content);
                return Json(JsonConvert.SerializeObject(new { rows = record }), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResolvedData(TicketRecordViewModel Model)
        {
            try
            {
                List<TicketRecordViewModel> record = new DataUtility().GetData<TicketRecordViewModel>(Server.MapPath("~/Data/TicketRecordData.json"));

                Model= record.Where(x => x.Ticket_Seq == Model.Ticket_Seq).First();
                Model.Modified_User = Session["UserName"].ToString();
                Model.Modified_Time = DateTime.Now.ToString("yyyy/MM/dd");
                Model.Ticket_Status = Enum.EnumUtility.TicketStatus.Resolved;
                record = record.Where(x => x.Ticket_Seq != Model.Ticket_Seq).ToList();
                record.Add(Model);
                string content = JsonConvert.SerializeObject(record);
                new DataUtility().WriteToJsonFile(Server.MapPath("~/Data/TicketRecordData.json"), content);
                return Json(JsonConvert.SerializeObject(new { rows = record }), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RemoveData(TicketRecordViewModel Model)
        {
            try
            {
                List<TicketRecordViewModel> record = new DataUtility().GetData<TicketRecordViewModel>(Server.MapPath("~/Data/TicketRecordData.json"));
                Model.Modified_User = Session["UserName"].ToString();
                Model.Modified_Time = DateTime.Now.ToString("yyyy/MM/dd");
                record = record.Where(x => x.Ticket_Seq != Model.Ticket_Seq).ToList(); 
                string content = JsonConvert.SerializeObject(record);
                new DataUtility().WriteToJsonFile(Server.MapPath("~/Data/TicketRecordData.json"), content);
                return Json(JsonConvert.SerializeObject(new { rows = record }), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}