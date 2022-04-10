using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartStock.Models
{

    public class Inventory
    {
        public string Product_Name { get; set; }
        public string Product_Desc { get; set; }

        //public int catagoryName { get; set; }
        public int UnitsPerCase { get; set; }
        public int Cases { get; set; }
        public int StatusID { get; set; }

        public long InventoryID = 0;
        public int ProductID = 0;
       // public int UnitsPerCase = 0;
        public int intCategoryID = 0;
      //  public int Cases = 0;
       // public int StatusID = 0;
        public int ProductlocationID = 0;
        public ActionTypes ActionType = ActionTypes.NoType;

        public bool IsAuthenticated
        {
            get
            {
                if (InventoryID > 0) return true;
                return false;
            }
        }

        public Inventory.ActionTypes Save()
        {
            try
            {
                Database db = new Database();
                if (InventoryID == 0)
                { //insert new product
                    this.ActionType = db.InsertInventory(this);
                }
                else
                {
                    this.ActionType = db.UpdateInventory(this);
                }
                return this.ActionType;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public bool RemoveInventorySession()
        {
            try
            {
                HttpContext.Current.Session["CurrentInventory"] = null;
                return true;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public Product GetInventorySession()
        {
            try
            {
                Product p = new Product();
                if (HttpContext.Current.Session["CurrentInventory"] == null)
                {
                    return p;
                }
                p = (Product)HttpContext.Current.Session["CurrentInventory"];
                return p;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public bool SaveInventorySession()
        {
            try
            {
                HttpContext.Current.Session["CurrentInventory"] = this;
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