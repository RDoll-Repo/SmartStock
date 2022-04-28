using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartStock.Models
{

    public class ProductPriceHistory
    {
        public decimal priceChangeComp;
        public long ProductPriceHistoryID = 0;
        public string ProductName = string.Empty;
        public DateTime PurchaseDate = DateTime.Now;
        public decimal CostPerUnit = 0;
        public int PurchaseAmt = 0;
        public string UnitType = string.Empty;
        public int UserID = 0;
        public int SupplierID = 0;
        public ActionTypes ActionType = ActionTypes.NoType;

        public bool IsAuthenticated
        {
            get
            {
                if (ProductPriceHistoryID > 0) return true;
                return false;
            }
        }

        public ProductPriceHistory.ActionTypes pphDeliverySave()
        {
            try
            {
                Database db = new Database();
                if (ProductPriceHistoryID == 0)
                { //insert new product
                    this.ActionType = db.DeliveryProductPrice(this);
                }
                else
                {
                    return this.ActionType;
                }
                return this.ActionType;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public ProductPriceHistory.ActionTypes Save()
        {
            try
            {
                Database db = new Database();
                if (ProductPriceHistoryID == 0)
                { //insert new product
                    this.ActionType = db.InsertProductPrice(this);
                }
                else
                {
                    this.ActionType = db.UpdateProductPrice(this);
                }
                return this.ActionType;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public bool RemoveProductPriceSession()
        {
            try
            {
                HttpContext.Current.Session["CurrentProductPrice"] = null;
                return true;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public ProductPriceHistory GetProductPriceSession()
        {
            try
            {
                ProductPriceHistory pph = new ProductPriceHistory();
                if (HttpContext.Current.Session["CurrentProductPrice"] == null)
                {
                    return pph;
                }
                pph = (ProductPriceHistory)HttpContext.Current.Session["CurrentProductPrice"];
                return pph;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public bool SaveProductPriceSession()
        {
            try
            {
                HttpContext.Current.Session["CurrentProductPrice"] = this;
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
            LoginFailed = 7,
            DeleteSuccessful = 8
        }
    }
}