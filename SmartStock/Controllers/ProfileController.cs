using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SmartStock.Models;

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
				u.Last_Name = col["Last_Name"];
				u.Phone_Number = col["Phone_Number"];
				u.Email = col["Email"];
				u.User_Name = col["User_Name"];
				u.Password = col["Password"];
				//u.Role_ID = col["Role_ID"];

				if (u.First_Name.Length == 0 || u.Last_Name.Length == 0 || u.Phone_Number.Length == 0 || u.Email.Length == 0 || u.User_Name.Length == 0 || u.Password.Length == 0)
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
		public ActionResult Index(FormCollection col) // edit profile in web2
		{
			Models.User u = new Models.User();
			u = u.GetUserSession();

			if (col["btnSubmit"] == "update")
			{ //update button pressed
				u = u.GetUserSession();

				u.First_Name = col["First_Name"];
				u.Last_Name = col["Last_Name"];
				u.Phone_Number = col["Phone_Number"];
				u.Email = col["Email"];
				u.User_Name = col["User_Name"];
				u.Password = col["Password"];
				//u.Role_ID = col["Role_ID"];

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

		public ActionResult Dashboard()
		{
			ViewBag.Message = "Dashboard";

			return View();
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

		public ActionResult InventoryAdjustment()
		{
			ViewBag.Message = "InventoryAdjustment";

			return View();
		}

		public ActionResult Users()
		{
			return View(GetAllUsers());
		}

		IEnumerable<TUser> GetAllUsers()
		{
			using (DBModel db = new DBModel())
			{
				return db.TUsers.ToList<TUser>();
			}

		}

		public ActionResult CreateUser()
		{
			Models.User u = new Models.User();
			return View(u);
		}

		[HttpPost]
		public ActionResult CreateUser(FormCollection col)
		{
			try
			{
				Models.User u = new Models.User();

				u.First_Name = col["First_Name"];
				u.Last_Name = col["Last_Name"];
				u.Phone_Number = col["Phone_Number"];
				u.Email = col["Email"];
				u.User_Name = col["User_Name"];
				u.Password = col["Password"];
				//u.Role_ID = col["Role_ID"];

				if (u.First_Name.Length == 0 || u.Last_Name.Length == 0 || u.Phone_Number.Length == 0 || u.Email.Length == 0 || u.User_Name.Length == 0 || u.Password.Length == 0)
				{
					u.ActionType = Models.User.ActionTypes.RequiredFieldsMissing;
					return View(u);
				}
				else
				{
					if (col["btnSubmit"] == "adduser")
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

		public ActionResult EditUser()
		{
			ViewBag.Message = "EditUser";

			return View();
		}


		public ActionResult Suppliers()
		{
			return View(GetAllSuppliers());
		}
		IEnumerable<TSupplier> GetAllSuppliers()
		{
			using (DBModel db = new DBModel())
			{
				return db.TSuppliers.ToList<TSupplier>();
			}

		}

		public ActionResult EditSupplier()
		{
			ViewBag.Message = "Edit Supplier";

			return View();
		}

		public ActionResult CreateSupplier()
		{
			Models.Supplier s = new Models.Supplier();
			return View(s);
		}

		[HttpPost]
		public ActionResult CreateSupplier(FormCollection col)
		{
			try
			{
				Models.Supplier s = new Models.Supplier();

				s.Company_Name = col["Company_Name"];
				s.Contact_FirstName = col["Contact_FirstName"];
				s.Contact_LastName = col["Contact_LastName"];
				s.Contact_PhoneNumber = col["Contact_PhoneNumber"];
				s.Contact_Email = col["Contact_Email"];
				s.Contact_Address1 = col["Contact_Address1"];
				s.Contact_State = col["Contact_State"];
				s.Contact_Zip = col["Contact_Zip"];
				s.URL = col["URL"];
				s.Notes = col["Notes"];

				if (s.Company_Name.Length == 0 || s.Contact_FirstName.Length == 0 || s.Contact_LastName.Length == 0 || s.Contact_PhoneNumber.Length == 0 || s.Contact_Email.Length == 0 || s.Contact_Address1.Length == 0 || s.Contact_State.Length == 0 || s.Contact_Zip.Length == 0)
				{
					s.ActionType = Models.Supplier.ActionTypes.RequiredFieldsMissing;
					return View(s);
				}
				else
				{
					if (col["btnSubmit"] == "addsupplier")
					{ //sign up button pressed
						s.Save();
						s.SaveSupplierSession();
						return RedirectToAction("Index");
					}
					return View(s);
				}
			}
			catch (Exception)
			{
				Models.Supplier s = new Models.Supplier();
				return View(s);
			}
		}
	}
}
