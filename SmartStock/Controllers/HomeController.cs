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

        [HttpPost] // sign-in button clicked
        public ActionResult HomePage(FormCollection col)
        {
            try
            {
                Models.User u = new Models.User();

                if (col["btnSubmit"] == "signin")
                {
                    u.User_Name = col["User_Name"];
                    u.Password = col["Password"];

                    if (u.User_Name.Length == 0 || u.Password.Length == 0) {
                        u.ActionType = Models.User.ActionTypes.RequiredFieldsMissing;
                        return View(u);
                    }

                    u = u.Login();
                    if (u != null && u.User_ID > 0)
                    {
                        u.SaveUserSession();
                        return RedirectToAction("Dashboard", "Profile");
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
            // get data from data base to put into drop down
            ////Create db context object here 
            //Database db = new Database();
            ////Get the value from database and then set it to ViewBag to pass it View
            //IEnumerable<SelectListItem> items = db.Employees.Select(c => new SelectListItem {
            //    Value = c.JobTitle,
            //    Text = c.JobTitle

            //});
            //ViewBag.JobTitle = items;

            Models.User u = new Models.User();
            return View(u);
        }

        [HttpPost] // when submit button pressed in signup:
        public ActionResult SignUp(FormCollection col) {
            try {
                Models.User u = new Models.User();

                u.First_Name = col["First_Name"];
                u.Last_Name = col["Last_Name"];
                u.Phone_Number = col["Phone_Number"];
                u.Email = col["Email"];
                u.User_Name = col["User_Name"];
                u.Password = col["Password"];
                //u.Role_ID = col["Role_ID"];

                if (u.First_Name.Length == 0 || u.Last_Name.Length == 0 || u.Phone_Number.Length == 0 || u.Email.Length == 0 || u.User_Name.Length == 0 || u.Password.Length == 0) {
                    u.ActionType = Models.User.ActionTypes.RequiredFieldsMissing;
                    return View(u);
                }
                else {
                    if (col["btnSubmit"] == "signup") { //sign up button pressed
                        // create if/else statement to determine if they are a new business signing up or a manager/employee signingup
                        // if owner, prompt to initialize stock
                        // if manager/employee, allow signup via
                        u.Save();
                        u.SaveUserSession();
                        return RedirectToAction("Index");
                    }
                    return View(u);
                }
            }
            catch (Exception) {
                Models.User u = new Models.User();
                return View(u);
            }
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