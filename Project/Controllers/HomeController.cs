using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Project.Models;
namespace Project.Controllers
{
    public class HomeController : Controller
    {
        readonly ProjectEntities db = new ProjectEntities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }
        public ActionResult Register()
        {
            if (Session["UserID"] != null)
            {
                return RedirectToAction("UserDashBoard");
            }
            else
            {
                List<string> CountryList = new List<string>();
                CultureInfo[] CInfoList = CultureInfo.GetCultures(CultureTypes.SpecificCultures);
                foreach (CultureInfo CInfo in CInfoList)
                {
                    RegionInfo R = new RegionInfo(CInfo.LCID);
                    if (!(CountryList.Contains(R.EnglishName)))
                    {
                        CountryList.Add(R.EnglishName);
                    }
                }

                CountryList.Sort();
                ViewBag.CountryList = CountryList;
                return View();
            }
        }
        [HttpPost]
        public ActionResult Register(RegistrationViewModel obj)
        {
            if (Session["UserID"] != null)
            {
                return RedirectToAction("UserDashBoard");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    User user = new User
                    {
                        Fullname = obj.Fullname,
                        Age = obj.Age,
                        Gender = obj.Gender,
                        Country = obj.Country,
                        Email = obj.Email,
                        Password = obj.Password,
                        Mobile = obj.Mobile,
                    };
                    db.User.Add(user);
                    db.SaveChanges();
                }
                else
                {
                    return View("Register");
                }
                return RedirectToAction("Login");
            }
        }
        [AllowAnonymous]
        public ActionResult Login()
        {
            if (Session["UserID"] != null)
            {
                return RedirectToAction("UserDashBoard");
            }else
            {
                return View("Login");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel objUser)
        {
            if (Session["UserID"] != null)
            {
                return RedirectToAction("UserDashBoard");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    using (db)
                    {
                        var obj = db.User.Where(a => a.Email.Equals(objUser.Email) && a.Password.Equals(objUser.Password)).FirstOrDefault();
                        if (obj != null)
                        {
                            Session["UserID"] = obj.UserID.ToString();
                            Session["Email"] = obj.Email.ToString();
                            Session["Fullname"] = obj.Fullname.ToString();
                            FormsAuthentication.SetAuthCookie(obj.Email, false);
                            return RedirectToAction("UserDashBoard");
                        }
                        else
                        {
                            ViewBag.Message = "Login failed! Please check your Email or Password.";
                            return View();
                        }
                    }

                }
                else
                {
                    return View("Login");
                }
            }
        }
        public ActionResult UserDashBoard()
        {
            if (Session["UserID"] != null)
            {
                return View();
            }
            else
            {
                ViewBag.Message = "You should Login to see this page!";
                return View("Login");
            }
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index");
        }
        public ActionResult Post()
        {
            if (Session["UserID"] != null)
            {
                return View();
            }
            else
            {
                ViewBag.Message = "You should Login to see this page!";
                return View("Login");
            }
        }
        [HttpPost]
        public ActionResult Post(CaseViewModel newCase)
        {
            if (Session["UserID"] != null)
            {
                if (ModelState.IsValid)
                {
                    Case caseObj = new Case
                    {
                        CaseID = newCase.CaseID,
                        Title = newCase.Title,
                        SymptomsBegan = newCase.SymptomsBegan,
                        CurrentMedications = newCase.CurrentMedications,
                        BodySystemsAffected = newCase.BodySystemsAffected,
                        SymptomsDetail = newCase.SymptomsDetail,
                        MedicalHistory = newCase.MedicalHistory,
                        PersonalStruggle = newCase.PersonalStruggle,
                        PostedDate = DateTime.Now,
                        User = Session["Fullname"].ToString()
                    };
                    db.Case.Add(caseObj);
                    db.SaveChanges();
                }
                else
                {
                    return View("Post");
                }
                return RedirectToAction("AllCases");
            }
            else
            {
                ViewBag.Message = "You should Login to see this page!";
                return View("Login");
            }
        }
        public ActionResult AllCases()
        {
            if (Session["UserID"] != null)
            {
                var myQuery = db.Case.Where(c => c.CaseID > 0).Select(c => c);
                return View(myQuery);
            }
            else
            {
                ViewBag.Message = "You should Login to see this page!";
                return View("Login");
            }
        }
        public ActionResult CaseDetails(int id)
        {
            if (Session["UserID"] != null)
            {
                var myQuery = db.Case.Where(c => c.CaseID == id).Select(c => c);
                return View(myQuery);
            }
            else
            {
                ViewBag.Message = "You should Login to see this page!";
                return View("Login");
            }
        }
        public ActionResult MyCases()
        {
            string userFullName = Session["Fullname"].ToString();
            if (Session["UserID"] != null)
            {
                var myQuery = db.Case.Where(c => c.User == userFullName).Select(c => c);
                return View(myQuery);
            }
            else
            {
                ViewBag.Message = "You should Login to see this page!";
                return View("Login");
            }
        }
        public ActionResult UpdateCase(int id)
        {
            if (Session["UserID"] != null)
            {
                Case myCase = db.Case.Find(id);
                return View(myCase);
            }
            else
            {
                ViewBag.Message = "You should Login to see this page!";
                return View("Login");
            }
        }
        [HttpPost]
        public ActionResult UpdateCase(Case myCase)
        {
            if (Session["UserID"] != null)
            {
                Case updatedCase = db.Case.Where(c => c.CaseID == myCase.CaseID).FirstOrDefault();
                updatedCase.Title = myCase.Title;
                updatedCase.CaseID = myCase.CaseID;
                updatedCase.SymptomsBegan = myCase.SymptomsBegan;
                updatedCase.CurrentMedications = myCase.CurrentMedications;
                updatedCase.BodySystemsAffected = myCase.BodySystemsAffected;
                updatedCase.SymptomsDetail = myCase.SymptomsDetail;
                updatedCase.MedicalHistory = myCase.MedicalHistory;
                updatedCase.PersonalStruggle = myCase.PersonalStruggle;
                updatedCase.PostedDate = DateTime.Now;
                db.SaveChanges();
                return RedirectToAction("MyCases");
            }
            else
            {
                ViewBag.Message = "You should Login to see this page!";
                return View("Login");
            }
        }
        public ActionResult DeleteCase(int id)
        {
            if (Session["UserID"] != null)
            {
                var cs = db.Case.Find(id);
                return View(cs);
            }
            else
            {
                ViewBag.Message = "You should Login to see this page!";
                return View("Login");
            }
        }
        public ActionResult DeleteFromList(int id)
        {
            if (Session["UserID"] != null)
            {
                db.Case.RemoveRange(db.Case.Where(c => c.CaseID == id));
                db.Comment.RemoveRange(db.Comment.Where(co => co.CaseID == id));
                db.SaveChanges();
                return RedirectToAction("MyCases");
            }
            else
            {
                ViewBag.Message = "You should Login to see this page!";
                return View("Login");
            }
        }
        public ActionResult Comment()
        {
            if (Session["UserID"] != null)
            {
                return View();
            }
            else
            {
                ViewBag.Message = "You should Login to see this page!";
                return View("Login");
            }
        }
        [HttpPost]
        public ActionResult Comment(Comment newComment, int id)
        {
            if (Session["UserID"] != null)
            {
                if (ModelState.IsValid)
                {
                    Comment commentObj = new Comment
                    {
                        CommentID = newComment.CommentID,
                        Comment1 = newComment.Comment1,
                        User = Session["Fullname"].ToString(),
                        CaseID = id,
                        PostedDate = DateTime.Now,
                    };
                    db.Comment.Add(commentObj);
                    db.SaveChanges();
                }
                else
                {
                    return View("Comment");
                }
                return RedirectToAction("AllCases");
            }
            else
            {
                ViewBag.Message = "You should Login to see this page!";
                return View("Login");
            }
        }
        public ActionResult Comments(int id)
        {
            if (Session["UserID"] != null)
            {
                var myQuery = db.Comment.Where(c => c.CaseID == id).Select(c => c);
                return View(myQuery);
            }
            else
            {
                ViewBag.Message = "You should Login to see this page!";
                return View("Login");
            }
        }
        public ActionResult DeleteComment(int id)
        {
            if (Session["UserID"] != null)
            {
                var cs = db.Comment.Find(id);
                return View(cs);
            }
            else
            {
                ViewBag.Message = "You should Login to see this page!";
                return View("Login");
            }
        }
        public ActionResult DeleteFromList2(int id)
        {
            if (Session["UserID"] != null)
            {
                db.Comment.RemoveRange(db.Comment.Where(co => co.CommentID == id));
                db.SaveChanges();
                return RedirectToAction("AllCases");
            }
            else
            {
                ViewBag.Message = "You should Login to see this page!";
                return View("Login");
            }
        }
    }
}