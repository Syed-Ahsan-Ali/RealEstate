﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RealEstate.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult History()
        {
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

        public ActionResult Map()
        {
            return View();
        }
       public ActionResult Blog()
        {
            return View();
        }


    }
}