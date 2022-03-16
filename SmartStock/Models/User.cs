using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CapstoneBackEnd.Models
{

	public class User
	{
		public long User_ID = 0;
		public string First_Name = string.Empty;
		public string Last_Name = string.Empty;
		public string Phone_Number = string.Empty;
		public string Email = string.Empty;
		public string Address_1 = string.Empty;
		public string Address_2 = string.Empty;
		public string Zip = string.Empty;
		public string User_Name = string.Empty;
		public string Password = string.Empty;
		public string State_ID = string.Empty;
		public string Role_ID = string.Empty;
		public ActionTypes ActionType = ActionTypes.NoType;

		public bool IsAuthenticated
		{
			get
			{
				if (User_ID > 0) return true;
				return false;
			}
		}

		public User Login()
		{
			try
			{
				Database db = new Database();
				return db.Login(this);
			}
			catch (Exception ex) { throw new Exception(ex.Message); }
		}

		public User.ActionTypes Save()
		{
			try
			{
				Database db = new Database();
				if (User_ID == 0)
				{ //insert new user
					this.ActionType = db.InsertUser(this);
				}
				else
				{
					this.ActionType = db.UpdateUser(this);
				}
				return this.ActionType;
			}
			catch (Exception ex) { throw new Exception(ex.Message); }
		}

		public bool RemoveUserSession()
		{
			try
			{
				HttpContext.Current.Session["CurrentUser"] = null;
				return true;
			}
			catch (Exception ex) { throw new Exception(ex.Message); }
		}

		public User GetUserSession()
		{
			try
			{
				User u = new User();
				if (HttpContext.Current.Session["CurrentUser"] == null)
				{
					return u;
				}
				u = (User)HttpContext.Current.Session["CurrentUser"];
				return u;
			}
			catch (Exception ex) { throw new Exception(ex.Message); }
		}

		public bool SaveUserSession()
		{
			try
			{
				HttpContext.Current.Session["CurrentUser"] = this;
				return true;
			}
			catch (Exception ex) { throw new Exception(ex.Message); }
		}

		public enum ActionTypes
		{
			NoType = 0,
			InsertSuccessful = 1,
			UpdateSuccessful = 2,
			DuplicateEmail = 3,
			DuplicateUserID = 4,
			Unknown = 5,
			RequiredFieldsMissing = 6,
			LoginFailed = 7
		}
	}
}