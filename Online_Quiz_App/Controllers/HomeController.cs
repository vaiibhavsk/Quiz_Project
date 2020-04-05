using Online_Quiz_App.Models;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Online_Quiz_App.Controllers
{
    public class HomeController : Controller
    {
        DB_QiuzEntities db = new DB_QiuzEntities();

        public ActionResult LOGOUT()
        {
            Session.Abandon();
            Session.RemoveAll();
            return RedirectToAction("Index");
        }
        public ActionResult Tlogin()
        {

            return View();
        }

        [HttpPost]
        public ActionResult tlogin(Tbl_Admin a)
        {

            Tbl_Admin ad = db.Tbl_Admin.Where(x => x.Ad_Name == a.Ad_Name && x.Ad_Password == a.Ad_Password).SingleOrDefault();
            if (ad != null)
            {
                Session["Ad_ID"] = ad.Ad_ID;
                return RedirectToAction("Dashboard");
            }
            else
            {
                ViewBag.msg = "Invalid username and Password";
            }
            return View();
        }

        public ActionResult Slogin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult slogin(Tbl_Student s)
        {


            Tbl_Student std = db.Tbl_Student.Where(x => x.S_UserName == s.S_UserName && x.S_Password == s.S_Password).SingleOrDefault();

            if (std == null)
            {
                ViewBag.msg = "Invalid Username or Password";
            }
            else
            {
                Session["S_ID"] = std.S_ID;
                return RedirectToAction("StudentExam");
            }
            return View();
        }

        [HttpPost]
        public ActionResult StudentExam(String room)
        {

            List<Tbl_Category> list = db.Tbl_Category.ToList();
            TempData["score"] = 0;
            foreach (var item in list)
            {
                if (item.cat_encrypted == room)
                {
                    List<Tbl_Questions> li = db.Tbl_Questions.Where(x => x.Q_FK_catid == item.Cat_ID).ToList();
                    Queue<Tbl_Questions> queue = new Queue<Tbl_Questions>();
                    foreach (Tbl_Questions a in li)
                    {
                        queue.Enqueue(a);
                    }
                    TempData["examid"] = item.Cat_ID;
                    TempData["questions"] = queue;
                    TempData["score"] = 0;
                    TempData.Keep();
                    return RedirectToAction("QuizStart");
                }


                else
                {
                    ViewBag.error = "No  Room Found......";
                }
            }
            return View();
        }
        public ActionResult QuizStart()
        {

            if (Session["S_ID"] == null)
            {
                return RedirectToAction("Slogin");
            }
            Tbl_Questions q = null;
            if (TempData["questions"] != null)
            {
                Queue<Tbl_Questions> qlist = (Queue<Tbl_Questions>)TempData["questions"];
                if (qlist.Count > 0)
                {
                    q = qlist.Peek();
                    qlist.Dequeue();
                    TempData["questions"] = qlist;
            
                    TempData.Keep();
                }
                else
                {
                    return RedirectToAction("EndExam");
                }
            }
            else
            {
                return RedirectToAction("StudentExam");
            }

            return View(q);
        }

        [HttpPost]
        public ActionResult QuizStart(Tbl_Questions q)
        {
            string correctans = null;
            //try
            //{

                if (q.Op_A != null)
                {
                    correctans = "A";

                }
                else if (q.Op_B != null)
                {
                    correctans = "B";

                }
                else if (q.Op_C != null)
                {
                    correctans = "C";

                }
                else if (q.Op_D != null)
                {

                    correctans = "D";

                }

                if (correctans.Equals(q.Correct_Op))
                {
                    TempData["score"] = Convert.ToInt32(TempData["score"]) + 1;    

                }
                TempData.Keep();
            //}
            //catch (Exception)
            //{
            //    ViewBag.msg = "Data could not insert";
            //}

            
            return RedirectToAction("QuizStart");

        }

        public ActionResult EndExam()
        {
            return View();
        }


        public ActionResult StudentExam()
        {
            if (Session["S_ID"] == null)
            {
                return RedirectToAction("slogin");
            }
            return View();
        }


        [HttpGet]
        public ActionResult Sudent_Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Sudent_Register(Tbl_Student svw, HttpPostedFileBase imgfile)
        { 

            Tbl_Student s = new Tbl_Student();
            //try
            //{
                s.S_UserName= svw.S_UserName;
                s.S_Password = svw.S_Password;
                s.S_FirstName = svw.S_FirstName;
                s.S_LastName = svw.S_LastName;
                s.S_EmailID = svw.S_EmailID;
                s.S_ContactNo = svw.S_ContactNo;
                s.S_Birthdate = svw.S_Birthdate;
                s.S_Age = svw.S_Age;
                s.S_img = uploadimage(imgfile);
                db.Tbl_Student.Add(s);
                db.SaveChanges();
                return RedirectToAction("slogin");

            //  }
            //catch (Exception)
            //{
            //    ViewBag.msg = "Data could not insert";
            //}

          //  return View();
        }

        private string uploadimage(HttpPostedFileBase imgfile)
        {
            string path = "-1";
            try
            {
                if (imgfile != null && imgfile.ContentLength > 0)
                {
                    string extension = Path.GetExtension(imgfile.FileName);
                    if (extension.ToLower().Equals("jpg") || extension.ToLower().Equals("jpeg") || extension.ToLower().Equals("png"))
                    {
                        Random r = new Random();

                        path = Path.Combine(Server.MapPath("~/Content/img"), r + Path.GetFileName(imgfile.FileName));
                        imgfile.SaveAs(path);
                        path = "~/Content/img" + r + Path.GetFileName(imgfile.FileName);
                    }

                }
            }
            catch (Exception)
            {
                ViewBag.msg = "Successful not";

            }
            return path;
        }

        public ActionResult Dashboard()
        {

            if (Session["Ad_ID"] == null)
            {
                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpGet]
        public ActionResult AddCategory()
        {


            if (Session["Ad_ID"] == null)
            {
                return RedirectToAction("Index");
            }

            int adid = Convert.ToInt32(Session["Ad_ID"].ToString());
            List<Tbl_Category> li = db.Tbl_Category.Where(x => x.Cat_FK_adid == adid).OrderByDescending(x => x.Cat_ID).ToList();
            ViewData["List"] = li;

            return View();

        }
        [HttpPost]
        public ActionResult AddCategory(Tbl_Category cat)
        {

            List<Tbl_Category> li = db.Tbl_Category.OrderByDescending(x => x.Cat_ID).ToList();
            ViewData["List"] = li;

            Random r = new Random();

            Tbl_Category c = new Tbl_Category();
            c.Cat_Name = cat.Cat_Name;
            c.cat_encrypted = crypto.Encrypt(cat.Cat_Name.Trim() + r.Next().ToString(), true);
            c.Cat_FK_adid = Convert.ToInt32(Session["Ad_ID"].ToString());
            db.Tbl_Category.Add(c);
            db.SaveChanges();

            return RedirectToAction("AddCategory");
        }
        public ActionResult Index()
        {
            if (Session["Ad_ID"] != null)
            {
                return RedirectToAction("Dashboard");
            }
            return View();
        }

        [HttpGet]
        public ActionResult Addquestions()
        {
            int sid = Convert.ToInt32(Session["Ad_ID"]);
            List<Tbl_Category> li = db.Tbl_Category.Where(x => x.Cat_FK_adid == sid).ToList();
            ViewBag.List = new SelectList(li, "Cat_ID", "Cat_Name");
            return View();
        }



        [HttpPost]
        public ActionResult Addquestions(Tbl_Questions q)
        {


            int sid = Convert.ToInt32(Session["Ad_ID"]);
            List<Tbl_Category> li = db.Tbl_Category.Where(x => x.Cat_FK_adid == sid).ToList();
            ViewBag.List = new SelectList(li, "Cat_ID", "Cat_Name");


            Tbl_Questions QA = new Tbl_Questions();
            //try { 
            QA.Q_Text = q.Q_Text;
            QA.Op_A = q.Op_A;

            QA.Op_B = q.Op_B;
            QA.Op_C = q.Op_C;
            QA.Op_D = q.Op_D;
            QA.Correct_Op = q.Correct_Op;
            QA.Q_FK_catid = q.Q_FK_catid;
            
            db.Tbl_Questions.Add(QA);
            db.SaveChanges();
            
            TempData["msg"] = "Question added successfully...";
            TempData.Keep();
            //}
            //catch (Exception)
            //{
            //    ViewBag.msg = "Data could not insert";
            //}

            return RedirectToAction("Addquestions");
       
       }

        public ActionResult ViewAllQuestions(int? id)
        {
            if (Session["Ad_ID"] == null)
            {
                return RedirectToAction("Tlogin");
            }
            if(id==null)
            {
                return RedirectToAction("Dashboard");
            }

            return View(db.Tbl_Questions.Where(x=>x.Q_FK_catid==id).ToList());
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
