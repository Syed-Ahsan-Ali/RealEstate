using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Data.Entity.Validation;

using System.Diagnostics;
using System.Web.Mvc;

namespace RealEstate.Controllers
{
    public class HomeController : Controller
    {
        EstateDbEntities1 obj = new EstateDbEntities1();
       
     
        public ActionResult Index()
        {
            return View();
        }

      
        public ActionResult Login()
        {
            

            string Uname = Request["uname"].ToString();
            string password = Request["pswd"].ToString();
            var r = obj.Users.Where(x => x.UserName == Uname && x.Password == password).FirstOrDefault();
            if (Uname != null)
            {
                return this.RedirectToAction("Index", "Admin");
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

        //    property.Property_Type = Request[];
        //    property.Address = Request[];
        //    property.Area_Size = Request[];
        //    property.City = Request[];
        //    property.No_Of_Rooms = Request[];
        //    property.Price = Request[];
        //    property.
            return View();
        }

        public ActionResult Postproperty()
        {
          
            return View();
        }

        [HttpPost]
        public ActionResult PropertyPosted()
        {
            Property property = new Property();
            Address address = new Address();
            Feature feature = new Feature();
            Seller seller = new Seller();
            Room room = new Room();




            address.Area_Name = Request["area_name"];
            address.City = Request["city"];
            address.Floor_No = Request["floor_no"];
            address.Plot_Number = Encoding.ASCII.GetBytes(Request["plot_no"].ToString());
            address.Property_Name = Request["property_name"];
            obj.Addresses.Add(address);
            obj.SaveChanges();




            var r = obj.Addresses.ToList().Last();

            property.Property_Adress_Id = r.Property_Adress_Id;
                property.Area_Size =Request["property_size"];
                property.City = Request["city"];
                property.No_Of_Rooms = Convert.ToInt32(Request["no_of_rooms"]);
                property.Property_Type = Request["property_type"];
                property.Price = Convert.ToInt32(Request["price"]);
                property.Property_Picture = "My images";
                
                DateTime today = DateTime.Today;
                property.Property_Date = today;

                
                try
                {
                   // obj.SaveChanges();
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                {
                    Exception raise = dbEx;
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            string message = string.Format("{0}:{1}",
                                validationErrors.Entry.Entity.ToString(),
                                validationError.ErrorMessage);
                            // raise a new exception nesting
                            // the current instance as InnerException
                            raise = new InvalidOperationException(message, raise);
                        }
                    }
                    throw raise;
                }
            byte[] sevenItems = new byte[2];
            sevenItems[0] = 0x20;
            sevenItems[1] = 0x30;
            room.Property_Id =1;
            room.Room_Name = "Room1";
            room.Area = "123";
            room.Room_Image = sevenItems;
            obj.Rooms.Add(room);
            obj.SaveChanges();

            property.Property_Id = 1;
            obj.Properties.Add(property);
            obj.SaveChanges();

            var R = obj.Properties.ToList().Last();



            seller.User_Id = 1;
                        seller.Post_Property_For = Request["posted_for"];
                        seller.Property_Id = R.Property_Id;
                  
               

                obj.Sellers.Add(seller);
                obj.SaveChanges();
            
          

            return RedirectToAction("Index","Home");
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
        [HttpPost]
        public ActionResult AddMap()
        {
            if (Request.Files.Count > 0)
            {
                Map h = new Map();
                HttpPostedFileBase file = Request.Files[0];
                h.Title = @"/Images/Maps/" + file.FileName;
                string img = @"~\Images\Maps\" + file.FileName;
                h.Name = Request["title"];

                obj.Maps.Add(h);
                obj.SaveChanges();
                file.SaveAs(Server.MapPath(img));


                
           
                
            }

            return RedirectToAction("Map","Home");

        }

        [HttpPost]
        public ActionResult MapUpdate(int id)
        {

            Map h = obj.Maps.Where(x => x.Id == id).FirstOrDefault();
            if (Request.Files.Count > 0)
            {


                HttpPostedFileBase file = Request.Files[0];
                string name = file.FileName;

                string img = @"~\Images\Maps\" + file.FileName;

                if (string.IsNullOrEmpty(name))
                {
                    h.Title = Request["image"];
                }
                else
                {
                    h.Title = @"/Images/Maps/" + file.FileName;

                }


            }
                h.Name = Request["title"];
               obj.SaveChanges();
            

            return RedirectToAction("Map","Home");

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
                agent.About_Agent = Request["Description"];

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

        /*Method for updating Agent Record*/

        /*ACTION METHOD TO ADD AGENT TO DATABASE*/
        public ActionResult AgentUpdate()
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
                agent.About_Agent = Request["Description"];

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
            return View("Agent");
        }

    }
}