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
	public class ProfileController : Controller
	{

		public ActionResult SignUp()
		{
			Models.User u = new Models.User();
			return View(u);
		}

		[HttpPost]
		

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
			return View(GetAllInventory());
		}
		IEnumerable<TInventory> GetAllInventory()
		{
			using (DBModel db = new DBModel())
			{
				return db.TInventories.ToList<TInventory>();
			}

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
			//Create db context object here 
			DBModel dbContext = new DBModel();
			//Get the value from database and then set it to ViewBag to pass it View
			IEnumerable<SelectListItem> items3 = dbContext.TCategories.Select(g => new SelectListItem
			{
				Value = g.intCategoryID.ToString(),
				Text = g.strCategory

			});
			ViewBag.catagoryName = items3;

			IEnumerable<SelectListItem> items5 = dbContext.TProductLocations.Select(l => new SelectListItem
			{
				Value = l.intProductLocationID.ToString(),
				Text = l.strLocation

			});
			ViewBag.location = items5;

			IEnumerable<SelectListItem> items4 = dbContext.TUsers.Select(tu => new SelectListItem
			{
				Value = tu.intUserID.ToString(),
				Text = tu.strFirstName

			});
			ViewBag.user = items4;

			IEnumerable<SelectListItem> items2 = dbContext.TSuppliers.Select(s => new SelectListItem
			{
				Value = s.intSupplierID.ToString(),
				Text = s.strCompanyName

			});
			ViewBag.supplier = items2;
			//return View();
			Models.Inventory i = new Models.Inventory();
			Models.ProductPriceHistory pph = new Models.ProductPriceHistory();
			return View();
		}



		[HttpPost] // when submit button pressed in signup:
		public ActionResult CreateItem(FormCollection col, Inventory model)
		{
			try
			{

				Models.Inventory i = new Models.Inventory();
				Models.ProductPriceHistory pph = new Models.ProductPriceHistory();


				i.ProductName = col["ProductName"];
				i.InvCount = Convert.ToInt32(col["InvCount"]);
				i.Status = col["Status"];
				i.CategoryID = Convert.ToInt32(col["catagoryName"]);
				i.ProductlocationID = Convert.ToInt32(col["location"]);
				pph.ProductName = col["ProductName"];
				pph.CostPerUnit = Convert.ToDecimal(col["CostPerUnit"]);
				pph.PurchaseAmt = Convert.ToInt32(col["InvCount"]);
				pph.UserID = Convert.ToInt32(col["user"]);
				pph.SupplierID = Convert.ToInt32(col["supplier"]);



				if (i.ProductName.Length == 0 || i.InvCount == 0 || i.Status.Length == 0 || i.CategoryID == 0 || i.ProductlocationID == 0 || pph.CostPerUnit == 0 || pph.PurchaseAmt == 0 || pph.UserID == 0 || pph.SupplierID == 0)
				{
					i.ActionType = Models.Inventory.ActionTypes.RequiredFieldsMissing;
					pph.ActionType = Models.ProductPriceHistory.ActionTypes.RequiredFieldsMissing;
					return View(i);
				}
				else
				{
					if (col["btnSubmit"] == "addproduct")
					{ //sign up button pressed
					// create if/else statement to determine if they are a new business signing up or a manager/employee signingup
					// if owner, prompt to initialize stock
					// if manager/employee, allow signup via
						pph.SaveProductPriceSession();
						pph.Save();
						i.SaveInventorySession();
						i.Save();
						return RedirectToAction("Inventory");
					}
				}
				return View();
			}
			catch (Exception)
			{
				Models.Inventory i = new Models.Inventory();
				return View();
			}
		}
		public ActionResult PurchaseLog()
		{
			return View(GetAllPurchaseLog());
		}
		IEnumerable<TProductPriceHistory> GetAllPurchaseLog()
		{
			using (DBModel db = new DBModel())
			{
				return db.TProductPriceHistories.ToList<TProductPriceHistory>();
			}
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
			//Create db context object here 
			DBModel dbContext = new DBModel();
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
				u.Role_ID = Convert.ToInt32(col["rolename"]);

				if (u.First_Name.Length == 0 || u.Last_Name.Length == 0 || u.Phone_Number.Length == 0 || u.Email.Length == 0 || u.User_Name.Length == 0 || u.Password.Length == 0 || u.Role_ID == 0)
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
						return RedirectToAction("Users");
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
						return RedirectToAction("Suppliers");
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
