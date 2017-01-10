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
        EstateDbEntities3 obj = new EstateDbEntities3();
       
     
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {

            string Uname = Request["uname"];
            string passwod = Request["pswd"];
            //var r = obj.Users.Where(x => x.UserName == Uname && x.Password == password).FirstOrDefault();
            if (Uname != null)
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

                    History h = obj.Histories.First();
        h.Title = Request["title"];
                    h.Description = Request["description"];
                    h.Image = @"~/images/" + file.FileName; ;
                    obj.SaveChanges();
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
            return View(obj.Histories.First());
        }
        public ActionResult OurHistory()
        {
            ICollection<Address> test = obj.Addresses.ToList();
            return View(test);
        }
        public ActionResult UpdateHistory(int id=1)
        {
           // var item = obj.Addresses.FirstOrDefault(c => c.Property_Adress_Id == id);
           // item.Property_Name = "Scoter";
          //  obj.Addresses.Update(item);
         //   obj.SaveChanges();
            return View();
        }
        public ActionResult OurTeam()
        {
            return View();
        }

        public ActionResult Agent()
        {
            return View(obj.Agents.ToList());
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
            return View(obj.Maps.ToList());
        }
        public ActionResult Blog()
        {
            return View();
        }
        public ActionResult Chat()
        {
            return View();
        }


        public ActionResult AddAgent()
        {


            return View();
        }

        /*ACTION METHOD TO ADD AGENT TO DATABASE*/
        public ActionResult Agentadded()
        {
            try
            {
                Agent agent = new Agent();

                agent.Name = Request["name"];
                agent.Email = Request["email"];
                agent.Phone_Number = Convert.ToInt64(Request["phone"]);
                agent.CNIC = Convert.ToInt64(Request["cnic"]);
                agent.Address = Request["adress"];
                agent.Office_Number = Convert.ToInt64(Request["officeNo"]);
                agent.About_Agent = "Nice";

                HttpPostedFileBase file = Request.Files[0];
                agent.Picture = @"/Images/Agent/" + file.FileName;
                string img = @"~\Images\Agent\" + file.FileName;
                obj.Agents.Add(agent);
                file.SaveAs(Server.MapPath(img));

                obj.SaveChanges();
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


            return RedirectToAction("Agent");
        }

        /*Action Method for displying AGENT PANEL  to admin*/

        public ActionResult AgentPanel()
        {
            return View(obj.Agents.ToList());
        }

        public ActionResult DeleteAgent(int id)
        {
            Agent agent = obj.Agents.First(x => x.Id == id);
            obj.Agents.Remove(agent);
            obj.SaveChanges();
            return View("AgentPanel");
        }

    }
}