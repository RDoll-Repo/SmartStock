using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
        public CardType Type = CardType.Null;

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

        public enum CardType
        {
            Null = 0,
            StockAlert = 1,
            PriceIncrease = 2,
            Announcement = 3
        }
    }
}