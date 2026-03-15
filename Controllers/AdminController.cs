using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Wedding_Planner.App_Code;
using Wedding_Planner.Models;

namespace Wedding_Planner.Controllers
{
    //[AuthoriseUserSession]
    public class AdminController : Controller
    {
        Wedding_PlannerEntities db = new Wedding_PlannerEntities();
        // GET: Admin
        public ActionResult AdminDashBoard()
        {
            ShowUserPicName();
            return View();
        }
        [NonAction]
        void ShowUserPicName()
        {
            string userid = Session["aid"].ToString();
            UserMaster um = db.UserMasters.Find(userid);
            if (um.Picture_File_Name != null)
            {
                ViewBag.UserPicName = um.Picture_File_Name;
            }
            else if (um.Gender.ToUpper() == "MALE")
            {
                ViewBag.UserPicName = "malePic.png";
            }
            else
            {
                ViewBag.UserPicName = "femalePic.png";
            }
            ViewBag.UserName = um.Name;
        }
        [HttpGet]
        public ActionResult Logout()
        {
            Session.Clear();
            Session.Remove("aid");
            Session.Abandon();
            return RedirectToAction("/", "General");
        }
        [HttpGet]
        public ActionResult BookingDetails()
        {
            ShowUserPicName();
            List<BookingMaster> LstCus = db.BookingMasters.ToList();
            return View(LstCus);
        }
        
        public ActionResult CustomerDetails()
        {
            ShowUserPicName();
            List<UserMaster> LstCust = db.UserMasters.ToList();
            return View(LstCust);
        }
        public ActionResult FeedBackDetails()
        {
            ShowUserPicName();
            List<FeedBackMaster> Lstfeed = db.FeedBackMasters.ToList();
            return View(Lstfeed);
        }
        public ActionResult ComplainDetails()
        {
            ShowUserPicName();
            List<ComplaintsMaster> Lstcomp = db.ComplaintsMasters.ToList();
            return View(Lstcomp);
        }
        public ActionResult EnquiryDetails()
        {
            ShowUserPicName();
            List<EnquiryMaster> Lstenq = db.EnquiryMasters.ToList();
            return View(Lstenq);
        }
        public ActionResult ContactsDetails()
        {
            ShowUserPicName();
            List<ContactMaster> Lstcon = db.ContactMasters.ToList();
            return View(Lstcon);
        }
        // booking deleted 
        public ActionResult DeleteCustomer(int id)
        {
            string msg = "";
            try
            {
                BookingMaster tc = db.BookingMasters.Find(id);
                db.BookingMasters.Remove(tc);
                db.SaveChanges();
                msg = "Record deleted successfully.";
            }
            catch
            {
                msg = "Sorry! unable to delete record.";
            }
            TempData["Message"] = msg;
            return RedirectToAction("BookingDetails");
        }
        // user records deleted
        public ActionResult DeleteCustomerRecords(string emailid)
        {
            string msg = "";
            try
            {
                List<FeedBackMaster> lstfm = db.FeedBackMasters.Where(x => x.FeedBackBy == emailid).ToList();
                if (lstfm.Count > 0)
                {
                    foreach (FeedBackMaster fm in lstfm)
                    {
                        db.FeedBackMasters.Remove(fm);
                    }
                }
                List<ComplaintsMaster> lstcm = db.ComplaintsMasters.Where(x => x.ComplainBy == emailid).ToList();
                if (lstcm.Count > 0)
                {
                    foreach (ComplaintsMaster cm in lstcm)
                    {
                        db.ComplaintsMasters.Remove(cm);
                    }
                }
                List<BookingMaster> lstbm = db.BookingMasters.Where(x => x.BookedBy == emailid).ToList();
                if (lstbm.Count > 0)
                {
                    foreach (BookingMaster bm in lstbm)
                    {
                        db.BookingMasters.Remove(bm);
                    }
                }
                LoginMaster lstlm = db.LoginMasters.Find(emailid);
                UserMaster um = db.UserMasters.Find(emailid);
                if (lstlm != null)
                {
                    db.LoginMasters.Remove(lstlm);
                    db.UserMasters.Remove(um);
                }

                db.SaveChanges();
                msg = "Record deleted successfully.";
            }
            catch
            {
                msg = "Sorry! unable to delete record.";
            }
            TempData["Message"] = msg;
            return RedirectToAction("CustomerDetails");
        }
        // feedback records deleted
        public ActionResult DeleteFeedBackRecords(int feedId)
        {
            string msg = "";
            try
            {
                FeedBackMaster fm = db.FeedBackMasters.Find(feedId);
                db.FeedBackMasters.Remove(fm);
                db.SaveChanges();
                msg = "Record deleted successfully.";
            }
            catch
            {
                msg = "Sorry! unable to delete record.";
            }
            TempData["Message"] = msg;
            return RedirectToAction("FeedBackDetails");
        }
        //
        public ActionResult DeleteComplaintsRecords(int Subid)
        {
            string msg = "";
            try
            {
                ComplaintsMaster fm = db.ComplaintsMasters.Find(Subid);
                db.ComplaintsMasters.Remove(fm);
                db.SaveChanges();
                msg = "Record deleted successfully.";
            }
            catch
            {
                msg = "Sorry! unable to delete record.";
            }
            TempData["Message"] = msg;
            return RedirectToAction("ComplainDetails");
        }
        // enquiery deleted 
        public ActionResult DeleteEnquieryDetails(int eqid)
        {
            string msg = "";
            try
            {
                EnquiryMaster tc = db.EnquiryMasters.Find(eqid);
                db.EnquiryMasters.Remove(tc);
                db.SaveChanges();
                msg = "Record deleted successfully.";
            }
            catch
            {
                msg = "Sorry! unable to delete record.";
            }
            TempData["Message"] = msg;
            return RedirectToAction("EnquiryDetails");
        }
        // contact details
        public ActionResult DeleteContactCustomer(int conid)
        {
            string msg = "";
            try
            {
                ContactMaster tc = db.ContactMasters.Find(conid);
                db.ContactMasters.Remove(tc);
                db.SaveChanges();
                msg = "Record deleted successfully.";
            }
            catch
            {
                msg = "Sorry! unable to delete record.";
            }
            TempData["Message"] = msg;
            return RedirectToAction("ContactsDetails");
        }
       
    }

}