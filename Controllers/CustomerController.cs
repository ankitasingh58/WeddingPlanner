using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations.Sql;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using Wedding_Planner.App_Code;
using Wedding_Planner.Models;

namespace Wedding_Planner.Controllers
{
    [AuthoriseUserSession]
    public class CustomerController : Controller
    {
        Wedding_PlannerEntities db=new Wedding_PlannerEntities();
        // GET: Customer
        public ActionResult CustDashBoard()
        {
            DashboardVM model = new DashboardVM();
            string uid = Session["uid"].ToString();
            model.TotalBookings = db.BookingMasters.Count(x => x.BookedBy == uid);
            model.TotalFeedbacks = db.FeedBackMasters.Count(x => x.FeedBackBy == uid);
            model.TotalComplaints = db.ComplaintsMasters.Count(x => x.ComplainBy == uid);
            model.TotalEmails = db.SendEmailMasters.Count(x => x.EmailId == uid);
            ShowUserPicName();
            return View(model); 
        }
        [HttpGet]
        public ActionResult UserProfile()
        {
            ShowUserPicName();
            BindCitiesAndAreaInDDL();
            string uid = Session["uid"].ToString();
            UserMaster um = new UserMaster();
            um = db.UserMasters.Find(uid);
            return View(um);
        }
        [HttpPost]
        public ActionResult UserProfile(UserMaster um)
        {
            HttpPostedFileBase NewImageFile = Request.Files["New_Pic"];
            FileManager fm = new FileManager();
            fm.AllowedExtensions = new string[] { ".jpg", ".png", ".jpeg" };
            fm.FolderName = "User_Profile_Pics";
            fm.MaxAllowedFileSizeInKB = 150;
            fm.PostedFile = NewImageFile;
            string msg = fm.UploadMyFile();
            UserMaster umdb = db.UserMasters.Find(um.EmailId);
            if (msg == "SUCCESS")
            {
                umdb.Picture_File_Name = fm.FileName;
            }
            if (msg == "Please choose a file."|| msg=="SUCCESS")
            {
                umdb.Name = um.Name;
                umdb.Gender = um.Gender;
                umdb.Address= um.Address;
                umdb.Related_City_Id=um.Related_City_Id;
                umdb.Related_Area_Id=um.Related_Area_Id;
                umdb.MobileNo= um.MobileNo;
                db.Entry(umdb);
                db.SaveChanges();
                msg = "Your profile updated successfully";
            }
            ShowUserPicName();
            BindCitiesAndAreaInDDL();
            ViewBag.Message = msg;
            UserMaster umNew=db.UserMasters.Find(um.EmailId);
            return View(umNew);
        }
        [HttpGet]
        public ActionResult ChangePassword()
        {
            ShowUserPicName();
            return View();
        }
        [HttpPost]
        public ActionResult ChangePassword(string Pass,string NewPass,string ConfNewPass)
        {
            string msg = "";
            if(NewPass==ConfNewPass)
            {
                string uid = Session["uid"].ToString();
                LoginMaster lm=db.LoginMasters.Find(uid);
                Cryptography cg = new Cryptography();
                string DecrPass=cg.DecryptMyData(lm.User_Password);
                if(Pass==DecrPass)
                {
                    string EncrPass=cg.EncryptMyData(NewPass);
                    lm.User_Password = EncrPass;
                    db.Entry(lm);
                    db.SaveChanges();
                    msg = "Password updated successfully.";
                }
                else
                {
                    msg = "Invalid current password.please try again.";
                }
            }
            else
            {
                msg = "New password and confirm password must be same.";
            }
            ShowUserPicName();
            ViewBag.Message = msg;
            return View();
        }
        [HttpGet]
        public ActionResult SendEmail()
        {
            ShowUserPicName();
            return View();
        }
        [HttpPost]
        public ActionResult SendEmail(SendEmailMaster sm)
        {
            string msg = "";
            try               
            {               
                SendEmailMaster tse = new SendEmailMaster();
                tse.Subject = sm.Subject;
                tse.EmailId = sm.EmailId;
                tse.Message = sm.Message;
                tse.Send_On = DateTime.Now;
                db.SendEmailMasters.Add(tse);
                db.SaveChanges();
                MyEmailSender es = new MyEmailSender();
                es.SendTo = sm.EmailId;
                es.Subject = sm.Subject;
                es.Message = sm.Message;
                bool b = es.SendEmail();
                msg = "Email Send Successfully.";
                }
                catch
                {
                    msg = "sorry ! Unable to send Email. Please try again.";
                }                
            ViewBag.Message = msg;
            ShowUserPicName();
            return View();
        }

        public ActionResult FeedBack()
        {
            ShowUserPicName();
            return View();
        }
        // to save feedback by saving ajax
        public JsonResult SaveFeedBack(FeedBackMaster fm)
        {
            string msg = "";
            try
            {
                fm.FeedBackBy = Session["uid"].ToString();
                fm.Date_Time = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt");
                db.FeedBackMasters.Add(fm);
                db.SaveChanges();
                msg = "Thanks for your valuable feedback";
            }
            catch
            {
                msg = "Sorry!unable to save your feedback..";
            }
            ViewBag.Msg = msg;
            return Json(msg, JsonRequestBehavior.AllowGet);
        }
       
        [HttpGet]
        public ActionResult MyBooking()
        {
            ShowUserPicName();
            BindEventsInDDL();           
            return View();
        }
        [HttpPost]
        public ActionResult MyBooking(BookingMaster bm)
        {
            string msg = "";
            ShowUserPicName();
            try {
            bm.BookedBy = Session["uid"].ToString();
               
           // bm.TotalAmount = int.Parse(bm.Hall + bm.Tent + bm.Lawn + bm.Parlour + bm.Music);
            db.BookingMasters.Add(bm);
            db.SaveChanges();
            msg = "Congratulation! Booking Successfully.";
            }
            catch
            {
                msg = "Booking Not Corfirmed! Try Again";
            }
            BindEventsInDDL();
            ViewBag.Msg = msg;
            return View();
        }
        [NonAction]
        void BindEventsInDDL()
        {
            ViewBag.EventList = db.EventMasters.ToList().Select(x => new SelectListItem()
            {
                Value = x.EventName.ToString(),
                Text = x.EventName
            });
        }
        public JsonResult GetAmentiUsingAJAX(string EventName)
        {
            db.Configuration.ProxyCreationEnabled = false;
            AmentiesMaster Amenties = db.AmentiesMasters.SingleOrDefault(x => x.EventName == EventName);
            return Json(Amenties, JsonRequestBehavior.AllowGet);

        }
        [HttpGet]
        public ActionResult Complaints()
        {
            ShowUserPicName();
            return View();
        }
        [HttpPost]
        public ActionResult Complaints(ComplaintsMaster cm)
        {
            string message = "";
            ShowUserPicName();
            try
            {
                cm.ComplainBy = Session["uid"].ToString();
                cm.Date_Time = DateTime.Now.ToString();
                db.ComplaintsMasters.Add(cm);
                db.SaveChanges();
                message = "Your Complaints saved now.";
            }
            catch
            {
                message = "Sorry unable saved your complaints. try again.";
            }
            ViewBag.Msg = message;
            return View();
        }
        [NonAction]
        void ShowUserPicName()
        {
            string userid = Session["uid"].ToString();
            UserMaster um = db.UserMasters.Find(userid);
            if(um.Picture_File_Name!=null)
            {
                ViewBag.UserPicName = um.Picture_File_Name;
            }
            else if(um.Gender.ToUpper()=="MALE")
            {
                ViewBag.UserPicName = "malePic.png";
            }
            else
            {
                ViewBag.UserPicName = "femalePic.png";
            }
            ViewBag.UserName = um.Name;
        }
        [NonAction]
        void BindCitiesAndAreaInDDL()
        {
            string userid = Session["uid"].ToString();
            UserMaster um = db.UserMasters.Find(userid);
            // for city
            ViewBag.Related_City_Id = db.CityMasters.ToList().Select(x => new SelectListItem()
            {
                Value = x.City_Id.ToString(),
                Text = x.City_Name,
                Selected = x.City_Id == um.Related_City_Id ? true : false
            });
            // for area
            ViewBag.Related_Area_Id = db.AreaMasters.Where(x => x.Related_City_Id==um.Related_City_Id).ToList().Select(x => new SelectListItem()
            {
                Value = x.Area_Id.ToString(),
                Text = x.Area_Name,
                Selected = x.Area_Id == um.Related_Area_Id ? true : false
            });
        }
        public JsonResult GetAreaUsingAJAX(int CityId)
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<AreaMaster> LstArea = db.AreaMasters.Where(x => x.Related_City_Id == CityId).ToList();
            return Json(LstArea, JsonRequestBehavior.AllowGet);

        }
        [HttpGet]
        public ActionResult Logout()
        {
            Session.Clear();
            Session.Remove("uid");
            Session.Abandon();
            return RedirectToAction("/","General");
        }


        [HttpGet]
        public ActionResult CustomerBookingDetails()
        {
            ShowUserPicName();
            string uid = Session["uid"].ToString();
            List<BookingMaster> LstCus = db.BookingMasters.Where(x=>x.BookedBy== uid).ToList();
            return View(LstCus);
        }
    }
}