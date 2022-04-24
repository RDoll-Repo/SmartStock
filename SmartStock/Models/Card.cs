using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartStock.Models
{

    // This is a class for the card system used on the dashboard.
    // There will be different categories and their appearance on the dashboard will be
    // based upon algorithms in the background during initialization of the dashboard. 
    // As stated from the outset, this system should be the last thing we do. 

    public class Card
    {
        public long Card_ID = 0;
        public string Message = string.Empty;
        //public ActionTypes Type = ActionTypes.Null;
        public DateTime AlertDateTime = DateTime.Now;
        public CardTypes CardType = CardTypes.NoType;

        public void Dismiss()
        {
            // TODO: Remove this card from the dashboard if the recipient dismisses it. 
        }


        /** Not where this is going to in production, but some sample scenarios:
        
        Low Stock:
        Take the average of stock per item. If the current stock is < 20% of
        the average, we will pop a card

                Sample Output: You are running low on {item}, be sure to order more soon. 

        Price Increase:
        If the cost of an item per unit increases by more than 15% in the span of two months,
        pop a card. 
        
                Sample Output: The price of {item} has increased from {former cost}
                               to {current cost} since {two months before now}. 
                               Consider switching suppliers

        Announcement: 
        Allows the owner to send annoucnements to the dashboards of employees.
                
                Sample Output: There is an extra shift available on April 29th, 2022.

        */
        public bool IsAuthenticated
        {
            get
            {
                if (Card_ID > 0) return true;
                return false;
            }
        }

        public Card.CardTypes Save()
        {
            try
            {
                Database db = new Database();
                if (Card_ID == 0)
                { //insert new product
                    this.CardType = db.InsertCard(this);
                }
                else
                {
                    this.CardType = db.UpdateCard(this);
                }
                return this.CardType;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public Card.CardTypes Delete()
        {
            try
            {
                Database db = new Database();
                this.CardType = db.DeleteCard(this);
                return this.CardType;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public bool RemoveCardSession()
        {
            try
            {
                HttpContext.Current.Session["CurrentInventory"] = null;
                return true;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public Card GetCardSession()
        {
            try
            {
                Card c = new Card();
                if (HttpContext.Current.Session["CurrentCard"] == null)
                {
                    return c;
                }
                c = (Card)HttpContext.Current.Session["CurrentCard"];
                return c;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public bool SaveCardSession()
        {
            try
            {
                HttpContext.Current.Session["CurrentCard"] = this;
                return true;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }


        public enum CardTypes
        {
            NoType = 0,
            InsertSuccessful = 1,
            UpdateSuccessful = 2,
            Unknown = 3,
            RequiredFieldsMissing = 4,
            DeleteSuccessful = 5,
            Null = 6,
            StockAlert = 7,
            PriceIncrease = 8,
            Announcement = 9
        }
    }
}