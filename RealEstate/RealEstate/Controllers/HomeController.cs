using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Web.Mvc;

namespace RealEstate.Controllers
{
    public class HomeController : Controller
    {
        EstateDbEntities1 db = new EstateDbEntities1();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login(string Uname="admin",string password="admin")
        {

            //string Uname = Request["uname"];
            //string password = Request["pswd"];
            var r=db.Users.Where(x => x.UserName == Uname && x.Password == password).FirstOrDefault();
            if (r != null)
            {
                return RedirectToAction("Index", "Admin");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        [HttpPost]
        public ActionResult UpdateHistoryPage(int id)
        {
            try
            {
                if (Request.Files.Count > 0)
                {
                    HttpPostedFileBase file = Request.Files[0];
                    string img = @"~\images\" + file.FileName;
                    file.SaveAs(Server.MapPath(img));

                    History h = db.Histories.First();
                    h.Title = Request["title"];
                    h.Description = Request["description"];
                    h.Image = @"~/images/" + file.FileName; ;
                    db.SaveChanges();
                }
                
            }catch(DbEntityValidationException e)
            {
                foreach (var validationErrors in e.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}",
                                                validationError.PropertyName,
                                                validationError.ErrorMessage);
                    }
                }
            }
            return RedirectToAction("History", "Home");
        }


        public ActionResult History()
        {
             return View(db.Histories.First());
        }
        public ActionResult OurHistory()
        {
            ICollection<Address> test = db.Addresses.ToList();
            return View(test);
        }
        public ActionResult UpdateHistory(int id=1)
        {
        //    var item = db.Addresses.FirstOrDefault(c=>c.Property_Adress_Id==id);
        //    item.Property_Name = "Scoter";
        //    db.Addresses.Update(item);
        //    db.SaveChanges();
            return View();
        }
        public ActionResult OurTeam()
        {
            return View();
        }

        public ActionResult Agent()
        {
            return View();
        }
        public ActionResult Agents()
        {
            return View();
        }

        public ActionResult Development()
        {
            return View();
        }

        public ActionResult Postproperty()
        {
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Cards()
        {
            return View();
        }

        public ActionResult SearchBoxAndNotification()
        {
            return View();
        }
        public ActionResult Map()
        {
                return View(db.Maps.ToList());
        }
       public ActionResult Blog()
        {
            return View();
        }
        public ActionResult Chat()
        {
            return View();
        }

    }
}