using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartStock.Controllers
{
	public class ProfileController : Controller
	{

		public ActionResult SignUp()
		{
			Models.User u = new Models.User();
			return View(u);
		}

		[HttpPost]
		public ActionResult SignUp(FormCollection col)
		{
			try
			{
				Models.User u = new Models.User();

				u.First_Name = col["First_Name"];
				u.Last_Name = col["First_Name"];
				u.Phone_Number = col["Phone_Number"];
				u.Email = col["Email"];
				u.Address_1 = col["Address_1"];
				u.Address_2 = col["Address_2"];
				u.Zip = col["Zip"];
				u.User_Name = col["User_Name"];
				u.Password = col["Password"];
				u.State_ID = col["State_ID"];
				u.Role_ID = col["Role_ID"];

				if (u.First_Name.Length == 0 || u.Last_Name.Length == 0 || u.Phone_Number.Length == 0 || u.Email.Length == 0 || u.Address_1.Length == 0 || u.Zip.Length == 0 || u.User_Name.Length == 0 || u.Password.Length == 0 || u.State_ID.Length == 0 || u.Role_ID.Length == 0)
				{
					u.ActionType = Models.User.ActionTypes.RequiredFieldsMissing;
					return View(u);
				}
				else
				{
					if (col["btnSubmit"] == "signup")
					{ //sign up button pressed
						u.Save();
						u.SaveUserSession();
						return RedirectToAction("Index");
					}
					return View(u);
				}
			}
			catch (Exception)
			{
				Models.User u = new Models.User();
				return View(u);
			}
		}

		public ActionResult Index()
		{
			Models.User u = new Models.User();
			u = u.GetUserSession();
			return View(u);
		}

		[HttpPost]
		public ActionResult Index(HttpPostedFileBase UserImage, FormCollection col)
		{
			Models.User u = new Models.User();
			u = u.GetUserSession();

			if (col["btnSubmit"] == "update")
			{ //update button pressed
				u = u.GetUserSession();

				u.First_Name = col["First_Name"];
				u.Last_Name = col["First_Name"];
				u.Phone_Number = col["Phone_Number"];
				u.Email = col["Email"];
				u.Address_1 = col["Address_1"];
				u.Address_2 = col["Address_2"];
				u.Zip = col["Zip"];
				u.User_Name = col["User_Name"];
				u.Password = col["Password"];
				u.State_ID = col["State_ID"];
				u.Role_ID = col["Role_ID"];

				u.Save();

				u.SaveUserSession();
				return RedirectToAction("Index");
			}
			return View(u);
		}


		public ActionResult SignIn()
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

		public ActionResult SignOut()
		{
			Models.User u = new Models.User();
			u.RemoveUserSession();
			return RedirectToAction("Index", "Home");
		}

		public ActionResult Inventory()
        {
			ViewBag.Message = "Inventory";

			return View();
        }

		public ActionResult SingleItem()
        {
			ViewBag.Message = "SingleItem";

			return View();
        }

		public ActionResult EditItem()
        {
			ViewBag.Message = "EditItem";

			return View();
        }

		public ActionResult CreateItem()
        {
			ViewBag.Message = "CreateItem";

			return View();
        }


		public ActionResult Users()
		{
			ViewBag.Message = "Users";

			return View();
		}

		public ActionResult CreateUser()
        {
			ViewBag.Message = "CreateUser";

			return View();
        }


		public ActionResult Suppliers()
        {
			ViewBag.Message = "Suppliers";

			return View();
        }

		public ActionResult EditSupplier()
		{
			ViewBag.Message = "Edit Supplier";

			return View();
		}
	}
}
