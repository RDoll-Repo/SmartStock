﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartStock.Models
{

    public class Inventory
    {
        public string Product_Name { get; set; }
        public string PurchaseDate { get; set; }

        public decimal CostPerUnit { get; set; }
        public int PurchaseAmt { get; set; }
        public int UserID { get; set; }
        public int SupplierID { get; set; }


        public long InventoryID = 0;
        public string ProductName = string.Empty;
        public int InvCount = 0;
        public string Status = string.Empty;
        public int CategoryID = 0;
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

        public Inventory GetInventorySession()
        {
            try
            {
                Inventory i = new Inventory();
                if (HttpContext.Current.Session["CurrentInventory"] == null)
                {
                    return i;
                }
                i = (Inventory)HttpContext.Current.Session["CurrentInventory"];
                return i;
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