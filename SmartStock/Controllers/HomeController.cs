using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartStock.Controllers
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

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult HomePage()
        {
            ViewBag.Message = "Login";
            //return RedirectToAction("Dashboard");

            return View();
        }

        public ActionResult SignUp()
        {
            ViewBag.Message = "Sign Up";

            return View();
        }

        public ActionResult InitializeInventory()
        {
            ViewBag.Message = "Initialize Inventory";

            return View();
        }

        public ActionResult InitializeEmployees()
        {
            ViewBag.Message = "Initialize Employees";

            return View();
        }

        public ActionResult InitializeSuppliers()
        {
            ViewBag.Message = "Initialize Suppliers";

            return View();
        }

    }
}