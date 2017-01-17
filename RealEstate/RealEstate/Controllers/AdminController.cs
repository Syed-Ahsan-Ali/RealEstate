using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RealEstate.Controllers
{
   
    public class AdminController : Controller
    {
        EstateDbEntities1 obj = new EstateDbEntities1();
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Agent()
        {
            return View();
        }
        public ActionResult Blog()
        {
            return View();
        }
        public ActionResult Cards()
        {
            return View();
        }
        public ActionResult Development()
        {
            return View();
        }
        public ActionResult UpdateHistoryPage(int id)
        {
            try
            {
                if (Request.Files.Count > 0)
                {
                    HttpPostedFileBase file = Request.Files[0];
                    string img = @"~\images\" + file.FileName;
                    file.SaveAs(Server.MapPath(img));

                    History h = obj.Histories.First();
                    h.Title = Request["title"];
                    h.Description = Request["description"];
                    h.Image = @"~/images/" + file.FileName; ;
                    obj.SaveChanges();
                }

            }
            catch (DbEntityValidationException e)
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
            return View(obj.Histories.First());
        }
        public ActionResult Map()
        {
            return View(obj.Maps.ToList());
        }
        public ActionResult OurTeam()
        {
            return View();
        }

        
    }
}