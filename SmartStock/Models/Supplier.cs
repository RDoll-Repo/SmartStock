using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartStock.Models
{

    public class Supplier
    {
        public long Supplier_ID = 0;
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