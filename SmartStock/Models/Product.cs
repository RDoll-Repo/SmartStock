using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartStock.Models
{

    public class Product
    {
        public long Product_ID = 0;
        public string Product_Name = string.Empty;
        public string Product_Desc = string.Empty;
        public int intCategoryID = 0;
        public ActionTypes ActionType = ActionTypes.NoType;

        public bool IsAuthenticated
        {
            get
            {
                if (Product_ID > 0) return true;
                return false;
            }
        }

        public Product.ActionTypes Save()
        {
            try
            {
                Database db = new Database();
                if (Product_ID == 0)
                { //insert new product
                    this.ActionType = db.InsertProduct(this);
                }
                else
                {
                    this.ActionType = db.UpdateProduct(this);
                }
                return this.ActionType;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public bool RemoveProductSession()
        {
            try
            {
                HttpContext.Current.Session["CurrentProduct"] = null;
                return true;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public Product GetProductSession()
        {
            try
            {
                Product p = new Product();
                if (HttpContext.Current.Session["CurrentProduct"] == null)
                {
                    return p;
                }
                p = (Product)HttpContext.Current.Session["CurrentProduct"];
                return p;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public bool SaveProductSession()
        {
            try
            {
                HttpContext.Current.Session["CurrentProduct"] = this;
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