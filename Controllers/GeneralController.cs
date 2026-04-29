using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wedding_Planner.Models;
using Wedding_Planner.App_Code;


namespace Wedding_Planner.Controllers
{
     
    public class GeneralController : Controller
    {  
        Wedding_PlannerEntities db = new Wedding_PlannerEntities();
        static HumanDetection.CaptchaDetails cd;
        HumanDetection hd = new HumanDetection();
        [NonAction]
        void GenerateCaptcha()
        {
            cd = hd.GenerateNewCaptcha();
            ViewBag.Img = cd.FolderName + "/" + cd.CaptchaImageName;

        }
       
        // GET: General
        [HttpGet]
        public ActionResult Home()
        {

            return View();
        }
        [HttpGet]
        public ActionResult ContactUs()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ContactUs(ContactMaster cm)
        {
            string msg = "";
            try
            {
                cm.Date_Time = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt");
                db.ContactMasters.Add(cm);
                db.SaveChanges();
                msg = "Contact Saved successfully.";
            }
            catch
            {
                msg = "Sorry!Unable to save contact.";
            }
            ViewBag.Message = msg;
            return View();
        }
        public ActionResult FAQS()
        {
            return View();
        }
        public ActionResult Gallery()
        {
            return View();
        }
       
        [HttpPost]
        public ActionResult Enquiery(EnquiryMaster em)
        {
            string msg= "";
            try
            {
                em.Enquiry_DateTime = DateTime.Now;
                db.EnquiryMasters.Add(em);
                db.SaveChanges();
                msg = "Enquiery saved successfully";
            }
            catch
            {
                msg = "Sorry! unable saved enquiery try again..";
            }
            ViewBag.Message = msg;
            return RedirectToAction("Home");
        }
        [HttpGet]
        public ActionResult Services()
        {
            return View();
        }
        public ActionResult AboutUs()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginMaster lm)
        {
            string msg = "";
            /* LoginMaster lmdb = db.LoginMasters.Find(lm.UserId);
             if (lmdb != null)
             {
                 if(lmdb.User_Password==lm.User_Password)
                 {
                     if(lmdb.User_Type==lm.User_Type)
                     {
                         //session
                         return RedirectToAction("CustomerDashboard", "User");
                     }
                     else
                     {
                         msg = "Invalid User Type.Please try again"; 

                     }

                 }
                 else
                 {
                     msg = "Invalid password please try again.";
                 }

             }
             else
             {
                 msg = "Invalid User Id.Please try again.";
             }*/
            LoginMaster lmdb = db.LoginMasters.SingleOrDefault(x => x.UserId == lm.UserId && x.User_Type==lm.User_Type);

            if (lmdb != null)
            {
                Cryptography cg = new Cryptography();
                string DecryptedPass = cg.DecryptMyData(lmdb.User_Password);
                if (lm.User_Password == DecryptedPass)
                {
                    if (lmdb.User_Status == true)
                    {
                        // create log of login in table
                        lmdb.Last_Login_Time = DateTime.Now;
                        lmdb.Login_Count = lmdb.Login_Count + 1;
                        db.Entry(lmdb);
                        db.SaveChanges();
                        // transfer into next zone
                        if (lmdb.User_Type == "USER")
                        {
                            Session["uid"] = lmdb.UserId;
                            Session["UserType"] = lmdb.User_Type;
                            return RedirectToAction("CustDashBoard", "Customer");
                        }
                        else
                        {
                            Session["aid"] = lmdb.UserId;
                            Session["UserType"] = lmdb.User_Type;
                            return RedirectToAction("AnalyticsDashboard", "Admin");
                        }

                        // session

                    }
                    else
                    {
                        msg = "Sorry! your account is suspended.";
                    }
                }
                else
                {
                    msg = "Invalid Password. Please Try Again";
                }
            }
            else
            {
                msg = "Invalid UserId or User Type ";
            }
            ViewBag.Message = msg;
            return View();
        }
        public ActionResult DestiManage()
        {
            return View();
        }
        public ActionResult DestiInvetation()
        {
            return View();
        }
        public ActionResult BridalCare()
        {
            return View();
        }
        public ActionResult VenueSel()
        {
            return View();
        }
        public ActionResult Batchlor() 
        {
            return View();
        }
        public ActionResult MenuPlan()
        {
            return View();
        }
        public ActionResult ArtistDetial()
        {
            return View();
        }
        public ActionResult SangeetDetail()
        {
            return View();
        }
        public ActionResult WeddingStationary()
        {
            return View();
        }
        public ActionResult Photography()
        {
            return View();
        }
        public ActionResult GuestAccom()
        {
            return View();
        }
        public ActionResult ThemeConcept()
        {
            return View();
        }
        public ActionResult VendorDetails()
        {
            return View();
        }
        public ActionResult EntertainmentSol()
        {
            return View();
        }
        public ActionResult LightSound()
        {
            return View();
        }
        public ActionResult Wedsits()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Ragistration()
        {
            BindCitiesInDDL();
            GenerateCaptcha();
            return View();
        }
        [NonAction]
        void BindCitiesInDDL()
        {
            ViewBag.Related_City_Id = db.CityMasters.ToList().Select(x => new SelectListItem()
            {
                Value = x.City_Id.ToString(),
                Text = x.City_Name
            });
        }
        public JsonResult GetAreaUsingAJAX(int CityId)
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<AreaMaster> LstArea = db.AreaMasters.Where(x => x.Related_City_Id == CityId).ToList();
            return Json(LstArea, JsonRequestBehavior.AllowGet);

        }

        //to get new captcha image using AJAX
        public JsonResult GetNewCaptchaImage()
        {
            GenerateCaptcha();
            string s = "/Content/" + cd.FolderName + "/" + cd.CaptchaImageName;
            return Json(s, JsonRequestBehavior.AllowGet);


        }
        [HttpPost]
        public ActionResult Ragistration(UserMaster um,string Pass,string ConfPass,string CaptchaCode)
        {
            string msg = "";
            try
            {
                if (cd.CaptchaCode == CaptchaCode)
                {
                    if (Pass == ConfPass)
                    {
                        // To validate file
                        HttpPostedFileBase userfile = Request.Files["UserPic"];
                        FileManager fm = new FileManager();
                        fm.PostedFile = userfile;
                        fm.AllowedExtensions = new string[] { ".jpg", ".png", ".jpeg", ".jfif" };
                        fm.FolderName = "User_Profile_Pics";
                        msg = fm.UploadMyFile();  // validate & upload 
                        if (msg == "SUCCESS" || msg== "Please choose a file.")
                        {
                            // database code
                            um.DateTime_Of_Reg = DateTime.Now;
                            um.Is_Del = false;
                            if (msg == "SUCCESS")
                                um.Picture_File_Name = fm.FileName;
                            db.UserMasters.Add(um);
                            // setting login info
                            Cryptography cg = new Cryptography();
                            string EncryptedPass = cg.EncryptMyData(Pass);
                            LoginMaster lm = new LoginMaster();
                            lm.UserId = um.EmailId; // PK of regtable
                            lm.User_Password = EncryptedPass;
                            lm.User_Status = true;
                            lm.Login_Count = 0;
                            lm.User_Type = "USER";   // costumer
                            db.LoginMasters.Add(lm);
                            db.SaveChanges();
                            msg = "Congratulations ! You are registerd successfully.";
                            // to send email alert
                            MyEmailSender es = new MyEmailSender();
                            es.SendTo = um.EmailId;
                            es.Subject = "Welcome to Wedding planner";
                            es.Message = "Hello" + um.Name + ", Welcome to Event Planner & Venue Booking System. \n You are successfully registered in oue web portal. Your user id is : " + um.EmailId + " and Password is: " + Pass + "\n\n Regards- \nTeams Event Planner";
                            es.SendEmail();

                        }
                    }
                    else
                    {
                        msg = "Password and confirm password must be same.";
                    }
                }
                else
                {
                    msg = "Invalid captcha code . Please try again.";
                }
            }
            catch
            {
                msg = "Due To some technical issues; we are unable to create your account right now.";
            }
            BindCitiesInDDL();
            GenerateCaptcha();
            ViewBag.Message = msg;
            return View();
        }



    }
}
