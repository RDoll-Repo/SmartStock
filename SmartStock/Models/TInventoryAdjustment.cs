//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SmartStock.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class TInventoryAdjustment
    {
        public int intInventoryAdjustmentID { get; set; }
        public int intInventoryID { get; set; }
        public int intAdjustmentID { get; set; }
        public int intUserID { get; set; }
        public int intProductID { get; set; }
    
        public virtual TAdjustment TAdjustment { get; set; }
        public virtual TInventory TInventory { get; set; }
        public virtual TProduct TProduct { get; set; }
        public virtual TUser TUser { get; set; }
    }
}