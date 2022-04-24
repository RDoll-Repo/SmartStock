using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;

namespace SmartStock.Models
{

    public class Supplier
    {
        public long Supplier_ID { get; set; }
        public string Company_Name = string.Empty;
        public string Contact_FirstName = string.Empty;
        public string Contact_LastName = string.Empty;
        public string Contact_PhoneNumber = string.Empty;
        public string Contact_Email = string.Empty;
        public string Contact_Address1 = string.Empty;
        public string Contact_Zip = string.Empty;
        public string URL = string.Empty;
        public string Notes = string.Empty;
        public string Contact_State = string.Empty;
        public ActionTypes ActionType = ActionTypes.NoType;

        public bool IsAuthenticated
        {
            get
            {
                if (Supplier_ID > 0) return true;
                return false;
            }
        }

        public Supplier.ActionTypes Save()
        {
            try
            {
                Database db = new Database();
                if (Supplier_ID == 0)
                { //insert new supplier
                    this.ActionType = db.InsertSupplier(this);
                }
                else
                {
                    this.ActionType = db.UpdateSupplier(this);
                }
                return this.ActionType;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public Supplier.ActionTypes Delete()
        {
            try
            {
                Database db = new Database();
                this.ActionType = db.DeleteSupplier(this);
                return this.ActionType;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public bool RemoveSupplierSession()
        {
            try
            {
                HttpContext.Current.Session["CurrentSupplier"] = null;
                return true;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public Supplier GetSupplierSession()
        {
            try
            {
                Supplier s = new Supplier();
                if (HttpContext.Current.Session["CurrentSupplier"] == null)
                {
                    return s;
                }
                s = (Supplier)HttpContext.Current.Session["CurrentSupplier"];
                return s;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public bool SaveSupplierSession()
        {
            try
            {
                HttpContext.Current.Session["CurrentSupplier"] = this;
                return true;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public Supplier Validation()
        {
            /**
             * Decided against regex for Address. There's just too many edge cases to make a reliable pattern.  
             * In addition, doing states would require an entire extra table that I don't think is worth the overhead.
             * Treat URL as optional since some businesses possibly don't have websites
             */
            Regex phone = new Regex(@"\(?\d{3}\)?-? *\d{3}-? *-?\d{4}");
            Regex zip = new Regex(@"^[0-9]{5}(?:-[0-9]{4})?$");

            if (this.Company_Name == string.Empty || this.Contact_FirstName == string.Empty || this.Contact_State == string.Empty || this.Contact_Address1 == string.Empty)
            {
                this.ActionType = ActionTypes.RequiredFieldsMissing;
                return this;
            }

            if (!phone.IsMatch(this.Contact_PhoneNumber))
            {
                this.ActionType = ActionTypes.InvalidPhoneNumber;
                return this;
            }

            if (!zip.IsMatch(this.Contact_Zip))
            {
                this.ActionType = ActionTypes.InvalidZip;
                return this;
            }

            try
            {
                MailAddress m = new MailAddress(this.Contact_Email);
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
            InvalidZip = 11,
        }
    }
}