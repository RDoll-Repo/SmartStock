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
            Models.User u = new Models.User();
            return View(u);
        }

        [HttpPost]
        public ActionResult SignIn(FormCollection col)
        {
            try
            {
                Models.User u = new Models.User();

                if (col["btnSubmit"] == "signin")
                {
                    u.User_Name = col["User_Name"];
                    u.Password = col["Password"];

                    u = u.Login();
                    if (u != null && u.User_ID > 0)
                    {
                        u.SaveUserSession();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        u = new Models.User();
                        u.User_Name = col["User_Name"];
                        u.ActionType = Models.User.ActionTypes.LoginFailed;
                    }
                }
                return View(u);
            }
            catch (Exception)
            {
                Models.User u = new Models.User();
                return View(u);
            }
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