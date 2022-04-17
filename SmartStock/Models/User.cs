using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;

namespace SmartStock.Models
{

	public class User
	{
		public long User_ID = 0;
		public string First_Name = string.Empty;
		public string Last_Name = string.Empty;
		public string Phone_Number = string.Empty;
		public string Email = string.Empty;
		public string User_Name = string.Empty;
		public string Password = string.Empty;
		public int Role_ID = 0;
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

		public User Validation()
		{
			Regex phone = new Regex(@"\(?\d{3}\)?-? *\d{3}-? *-?\d{4}");

			if (this.First_Name == string.Empty || this.Last_Name == string.Empty || this.User_Name == string.Empty || this.Password == string.Empty )
            {
				this.ActionType = ActionTypes.RequiredFieldsMissing;
				//return this;
            }


			if (!phone.IsMatch(this.Phone_Number))
            {
				this.ActionType = ActionTypes.InvalidPhoneNumber;
				return this;
            }

			try
            {
				MailAddress m = new MailAddress(this.Email);
            }
			catch (Exception)
            {
				this.ActionType = ActionTypes.InvalidEmail;
				return this;
            }

			return this;
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
			LoginFailed = 7,
			DeleteSuccessful = 8,
			InvalidPhoneNumber = 9,
			InvalidEmail = 10,
			NoRole = 11
		}
	}
}