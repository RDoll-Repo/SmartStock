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
    
    public partial class TInventory
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TInventory()
        {
            this.TInventoryAdjustments = new HashSet<TInventoryAdjustment>();
        }
    
        public int intInventoryID { get; set; }
        public int intProductID { get; set; }
        public int intStatusID { get; set; }
        public int intCurrentInventory { get; set; }
    
        public virtual TProduct TProduct { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TInventoryAdjustment> TInventoryAdjustments { get; set; }
    }
}