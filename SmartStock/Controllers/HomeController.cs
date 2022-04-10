using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SmartStock.Models;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

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

            //Create db context object here 
            DBModels dbContext = new DBModels();
            //Get the value from database and then set it to ViewBag to pass it View
            IEnumerable<SelectListItem> items2 = dbContext.TRoles.Select(c => new SelectListItem
            {
                Value = c.intRoleID.ToString(),
                Text = c.strRoleName

            });
            ViewBag.rolename = items2;
            //return View();
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
                u.Role_ID = Convert.ToInt32(col["rolename"]);

                if (u.First_Name.Length == 0 || u.Last_Name.Length == 0 || u.Phone_Number.Length == 0 || u.Email.Length == 0 || u.User_Name.Length == 0 || u.Password.Length == 0 || u.Role_ID == 0) {
                    u.ActionType = Models.User.ActionTypes.RequiredFieldsMissing;
                    return View(u);
                }
                else {
                    if (col["btnSubmit"] == "signup") { //sign up button pressed
                        // create if/else statement to determine if they are a new business signing up or a manager/employee signingup
                        // if owner, prompt to initialize stock
                        // if manager/employee, allow signup via
                        u.SaveUserSession();
                        if (u.Role_ID == 1)
                        {
                            return RedirectToAction("InitializeInventory");
                        }
                        else {
                            return RedirectToAction("Dashboard", "Profile");
                        }
                        
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
            //Create db context object here 
            DBModels dbContext = new DBModels();
            //Get the value from database and then set it to ViewBag to pass it View
            IEnumerable<SelectListItem> items3 = dbContext.TCategories.Select(g => new SelectListItem
            {
                Value = g.intCategoryID.ToString(),
                Text = g.strCategory

            });
            ViewBag.catagoryName = items3;

            IEnumerable<SelectListItem> items4 = dbContext.TStatus.Select(s => new SelectListItem
            {
                Value = s.intStatusID.ToString(),
                Text = s.strStatusType

            });
            ViewBag.statusName = items4;
            IEnumerable<SelectListItem> items5 = dbContext.TProductLocations.Select(l => new SelectListItem
            {
                Value = l.intProductLocationID.ToString(),
                Text = l.strLocation

            });
            ViewBag.location = items5;
            //return View();
            Models.Inventory i = new Models.Inventory();
            Models.Product p = new Models.Product();
            return View();
        }



        [HttpPost] // when submit button pressed in signup:
        public ActionResult InitializeInventory(FormCollection col, Inventory model)
        {
            try
            {
                Models.Inventory i = new Models.Inventory();
                Models.Product p = new Models.Product();
                Models.User u = new Models.User();

                p.Product_Name = model.Product_Name;
                p.Product_Desc = model.Product_Desc;
                p.intCategoryID = Convert.ToInt32(col["catagoryName"]);
                i.ProductlocationID = Convert.ToInt32(col["ProductlocationID"]);
                i.StatusID = Convert.ToInt32(col["StatusID"]);
                i.UnitsPerCase = model.UnitsPerCase;
                i.Cases = model.Cases;
                //i.ProductID = 


                if (p.Product_Name.Length == 0 || p.intCategoryID == 0 ||  i.UnitsPerCase == 0 || i.Cases == 0 || i.StatusID == 0 || i.ProductlocationID == 0)
                {
                    i.ActionType = Models.Inventory.ActionTypes.RequiredFieldsMissing;
                    p.ActionType = Models.Product.ActionTypes.RequiredFieldsMissing;
                    return View(i);
                }
                else
                {
                    if (col["btnSubmit"] == "addproduct")
                    { //sign up button pressed
                      // create if/else statement to determine if they are a new business signing up or a manager/employee signingup
                      // if owner, prompt to initialize stock
                      // if manager/employee, allow signup via

                        u = u.GetUserSession();
                        i.Save();
                        i.SaveInventorySession();
                        p.Save();
                        p.SaveProductSession();
                        u.Save();
                       

                        return RedirectToAction("Dashboard", "Profile");

                    }
                    return View(u);
                }
            }
            catch (Exception)
            {
                Models.Product p = new Models.Product();
                Models.Inventory i = new Models.Inventory();
                return View();
            }
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