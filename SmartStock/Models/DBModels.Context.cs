﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class DBModel : DbContext
    {
        public DBModel()
            : base("name=DBModel")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<TCategory> TCategories { get; set; }
        public virtual DbSet<TInventory> TInventories { get; set; }
        public virtual DbSet<TProductLocation> TProductLocations { get; set; }
        public virtual DbSet<TProductPriceHistory> TProductPriceHistories { get; set; }
        public virtual DbSet<TRole> TRoles { get; set; }
        public virtual DbSet<TSupplier> TSuppliers { get; set; }
        public virtual DbSet<TUser> TUsers { get; set; }
    
        public virtual int DELETE_INVENTORY(ObjectParameter inventoryID)
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("DELETE_INVENTORY", inventoryID);
        }
    
        public virtual int DELETE_PRODUCTPRICEHISTORY(ObjectParameter pPHID)
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("DELETE_PRODUCTPRICEHISTORY", pPHID);
        }
    
        public virtual int DELETE_SUPPLIER(ObjectParameter supplier_ID)
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("DELETE_SUPPLIER", supplier_ID);
        }
    
        public virtual int DELETE_USER(ObjectParameter user_ID)
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("DELETE_USER", user_ID);
        }
    
        public virtual int INSERT_INVENTORY(ObjectParameter inventoryID, string productName, Nullable<int> invCount, string status, Nullable<int> categoryID, Nullable<int> productlocationID)
        {
            var productNameParameter = productName != null ?
                new ObjectParameter("ProductName", productName) :
                new ObjectParameter("ProductName", typeof(string));
    
            var invCountParameter = invCount.HasValue ?
                new ObjectParameter("InvCount", invCount) :
                new ObjectParameter("InvCount", typeof(int));
    
            var statusParameter = status != null ?
                new ObjectParameter("Status", status) :
                new ObjectParameter("Status", typeof(string));
    
            var categoryIDParameter = categoryID.HasValue ?
                new ObjectParameter("CategoryID", categoryID) :
                new ObjectParameter("CategoryID", typeof(int));
    
            var productlocationIDParameter = productlocationID.HasValue ?
                new ObjectParameter("ProductlocationID", productlocationID) :
                new ObjectParameter("ProductlocationID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("INSERT_INVENTORY", inventoryID, productNameParameter, invCountParameter, statusParameter, categoryIDParameter, productlocationIDParameter);
        }
    
        public virtual int INSERT_PRODUCTPRICEHISTORY(ObjectParameter pPHID, string productName, Nullable<System.DateTime> purchaseDate, Nullable<decimal> costPerUnit, Nullable<int> purchaseAmt, Nullable<int> userID, Nullable<int> supplierID)
        {
            var productNameParameter = productName != null ?
                new ObjectParameter("ProductName", productName) :
                new ObjectParameter("ProductName", typeof(string));
    
            var purchaseDateParameter = purchaseDate.HasValue ?
                new ObjectParameter("PurchaseDate", purchaseDate) :
                new ObjectParameter("PurchaseDate", typeof(System.DateTime));
    
            var costPerUnitParameter = costPerUnit.HasValue ?
                new ObjectParameter("CostPerUnit", costPerUnit) :
                new ObjectParameter("CostPerUnit", typeof(decimal));
    
            var purchaseAmtParameter = purchaseAmt.HasValue ?
                new ObjectParameter("PurchaseAmt", purchaseAmt) :
                new ObjectParameter("PurchaseAmt", typeof(int));
    
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(int));
    
            var supplierIDParameter = supplierID.HasValue ?
                new ObjectParameter("SupplierID", supplierID) :
                new ObjectParameter("SupplierID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("INSERT_PRODUCTPRICEHISTORY", pPHID, productNameParameter, purchaseDateParameter, costPerUnitParameter, purchaseAmtParameter, userIDParameter, supplierIDParameter);
        }
    
        public virtual int INSERT_SUPPLIER(ObjectParameter supplier_ID, string company_Name, string contact_FirstName, string contact_LastName, string contact_PhoneNumber, string contact_Email, string contact_Address1, string contact_Zip, string uRL, string notes, string contact_State)
        {
            var company_NameParameter = company_Name != null ?
                new ObjectParameter("Company_Name", company_Name) :
                new ObjectParameter("Company_Name", typeof(string));
    
            var contact_FirstNameParameter = contact_FirstName != null ?
                new ObjectParameter("Contact_FirstName", contact_FirstName) :
                new ObjectParameter("Contact_FirstName", typeof(string));
    
            var contact_LastNameParameter = contact_LastName != null ?
                new ObjectParameter("Contact_LastName", contact_LastName) :
                new ObjectParameter("Contact_LastName", typeof(string));
    
            var contact_PhoneNumberParameter = contact_PhoneNumber != null ?
                new ObjectParameter("Contact_PhoneNumber", contact_PhoneNumber) :
                new ObjectParameter("Contact_PhoneNumber", typeof(string));
    
            var contact_EmailParameter = contact_Email != null ?
                new ObjectParameter("Contact_Email", contact_Email) :
                new ObjectParameter("Contact_Email", typeof(string));
    
            var contact_Address1Parameter = contact_Address1 != null ?
                new ObjectParameter("Contact_Address1", contact_Address1) :
                new ObjectParameter("Contact_Address1", typeof(string));
    
            var contact_ZipParameter = contact_Zip != null ?
                new ObjectParameter("Contact_Zip", contact_Zip) :
                new ObjectParameter("Contact_Zip", typeof(string));
    
            var uRLParameter = uRL != null ?
                new ObjectParameter("URL", uRL) :
                new ObjectParameter("URL", typeof(string));
    
            var notesParameter = notes != null ?
                new ObjectParameter("Notes", notes) :
                new ObjectParameter("Notes", typeof(string));
    
            var contact_StateParameter = contact_State != null ?
                new ObjectParameter("Contact_State", contact_State) :
                new ObjectParameter("Contact_State", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("INSERT_SUPPLIER", supplier_ID, company_NameParameter, contact_FirstNameParameter, contact_LastNameParameter, contact_PhoneNumberParameter, contact_EmailParameter, contact_Address1Parameter, contact_ZipParameter, uRLParameter, notesParameter, contact_StateParameter);
        }
    
        public virtual int INSERT_USER(ObjectParameter user_ID, string first_Name, string last_Name, string phone_Number, string email, string user_Name, string password, Nullable<int> role_ID)
        {
            var first_NameParameter = first_Name != null ?
                new ObjectParameter("First_Name", first_Name) :
                new ObjectParameter("First_Name", typeof(string));
    
            var last_NameParameter = last_Name != null ?
                new ObjectParameter("Last_Name", last_Name) :
                new ObjectParameter("Last_Name", typeof(string));
    
            var phone_NumberParameter = phone_Number != null ?
                new ObjectParameter("Phone_Number", phone_Number) :
                new ObjectParameter("Phone_Number", typeof(string));
    
            var emailParameter = email != null ?
                new ObjectParameter("Email", email) :
                new ObjectParameter("Email", typeof(string));
    
            var user_NameParameter = user_Name != null ?
                new ObjectParameter("User_Name", user_Name) :
                new ObjectParameter("User_Name", typeof(string));
    
            var passwordParameter = password != null ?
                new ObjectParameter("Password", password) :
                new ObjectParameter("Password", typeof(string));
    
            var role_IDParameter = role_ID.HasValue ?
                new ObjectParameter("Role_ID", role_ID) :
                new ObjectParameter("Role_ID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("INSERT_USER", user_ID, first_NameParameter, last_NameParameter, phone_NumberParameter, emailParameter, user_NameParameter, passwordParameter, role_IDParameter);
        }
    
        public virtual ObjectResult<LOGIN_Result> LOGIN(string user_Name, string password)
        {
            var user_NameParameter = user_Name != null ?
                new ObjectParameter("User_Name", user_Name) :
                new ObjectParameter("User_Name", typeof(string));
    
            var passwordParameter = password != null ?
                new ObjectParameter("Password", password) :
                new ObjectParameter("Password", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<LOGIN_Result>("LOGIN", user_NameParameter, passwordParameter);
        }
    
        public virtual ObjectResult<SELECT_INVENTORY_Result> SELECT_INVENTORY(Nullable<long> inventoryID)
        {
            var inventoryIDParameter = inventoryID.HasValue ?
                new ObjectParameter("InventoryID", inventoryID) :
                new ObjectParameter("InventoryID", typeof(long));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SELECT_INVENTORY_Result>("SELECT_INVENTORY", inventoryIDParameter);
        }
    
        public virtual ObjectResult<SELECT_PRODUCTPRICEHISTORY_Result> SELECT_PRODUCTPRICEHISTORY(Nullable<long> pPHID)
        {
            var pPHIDParameter = pPHID.HasValue ?
                new ObjectParameter("PPHID", pPHID) :
                new ObjectParameter("PPHID", typeof(long));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SELECT_PRODUCTPRICEHISTORY_Result>("SELECT_PRODUCTPRICEHISTORY", pPHIDParameter);
        }
    
        public virtual ObjectResult<SELECT_SUPPLIER_Result> SELECT_SUPPLIER(Nullable<long> supplier_ID)
        {
            var supplier_IDParameter = supplier_ID.HasValue ?
                new ObjectParameter("Supplier_ID", supplier_ID) :
                new ObjectParameter("Supplier_ID", typeof(long));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SELECT_SUPPLIER_Result>("SELECT_SUPPLIER", supplier_IDParameter);
        }
    
        public virtual ObjectResult<SELECT_USER_Result> SELECT_USER(Nullable<long> user_ID)
        {
            var user_IDParameter = user_ID.HasValue ?
                new ObjectParameter("User_ID", user_ID) :
                new ObjectParameter("User_ID", typeof(long));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SELECT_USER_Result>("SELECT_USER", user_IDParameter);
        }
    
        public virtual int UPDATE_INVENTORY(ObjectParameter inventoryID, string productName, Nullable<int> invCount, string status, Nullable<int> categoryID, Nullable<int> productlocationID)
        {
            var productNameParameter = productName != null ?
                new ObjectParameter("ProductName", productName) :
                new ObjectParameter("ProductName", typeof(string));
    
            var invCountParameter = invCount.HasValue ?
                new ObjectParameter("InvCount", invCount) :
                new ObjectParameter("InvCount", typeof(int));
    
            var statusParameter = status != null ?
                new ObjectParameter("Status", status) :
                new ObjectParameter("Status", typeof(string));
    
            var categoryIDParameter = categoryID.HasValue ?
                new ObjectParameter("CategoryID", categoryID) :
                new ObjectParameter("CategoryID", typeof(int));
    
            var productlocationIDParameter = productlocationID.HasValue ?
                new ObjectParameter("ProductlocationID", productlocationID) :
                new ObjectParameter("ProductlocationID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("UPDATE_INVENTORY", inventoryID, productNameParameter, invCountParameter, statusParameter, categoryIDParameter, productlocationIDParameter);
        }
    
        public virtual int UPDATE_PRODUCTPRICEHISTORY(ObjectParameter pPHID, string productName, Nullable<System.DateTime> purchaseDate, Nullable<decimal> costPerUnit, Nullable<int> purchaseAmt, Nullable<int> userID, Nullable<int> supplierID)
        {
            var productNameParameter = productName != null ?
                new ObjectParameter("ProductName", productName) :
                new ObjectParameter("ProductName", typeof(string));
    
            var purchaseDateParameter = purchaseDate.HasValue ?
                new ObjectParameter("PurchaseDate", purchaseDate) :
                new ObjectParameter("PurchaseDate", typeof(System.DateTime));
    
            var costPerUnitParameter = costPerUnit.HasValue ?
                new ObjectParameter("CostPerUnit", costPerUnit) :
                new ObjectParameter("CostPerUnit", typeof(decimal));
    
            var purchaseAmtParameter = purchaseAmt.HasValue ?
                new ObjectParameter("PurchaseAmt", purchaseAmt) :
                new ObjectParameter("PurchaseAmt", typeof(int));
    
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(int));
    
            var supplierIDParameter = supplierID.HasValue ?
                new ObjectParameter("SupplierID", supplierID) :
                new ObjectParameter("SupplierID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("UPDATE_PRODUCTPRICEHISTORY", pPHID, productNameParameter, purchaseDateParameter, costPerUnitParameter, purchaseAmtParameter, userIDParameter, supplierIDParameter);
        }
    
        public virtual int UPDATE_SUPPLIER(ObjectParameter supplier_ID, string company_Name, string contact_FirstName, string contact_LastName, string contact_PhoneNumber, string contact_Email, string contact_Address1, string contact_Zip, string uRL, string notes, string contact_State)
        {
            var company_NameParameter = company_Name != null ?
                new ObjectParameter("Company_Name", company_Name) :
                new ObjectParameter("Company_Name", typeof(string));
    
            var contact_FirstNameParameter = contact_FirstName != null ?
                new ObjectParameter("Contact_FirstName", contact_FirstName) :
                new ObjectParameter("Contact_FirstName", typeof(string));
    
            var contact_LastNameParameter = contact_LastName != null ?
                new ObjectParameter("Contact_LastName", contact_LastName) :
                new ObjectParameter("Contact_LastName", typeof(string));
    
            var contact_PhoneNumberParameter = contact_PhoneNumber != null ?
                new ObjectParameter("Contact_PhoneNumber", contact_PhoneNumber) :
                new ObjectParameter("Contact_PhoneNumber", typeof(string));
    
            var contact_EmailParameter = contact_Email != null ?
                new ObjectParameter("Contact_Email", contact_Email) :
                new ObjectParameter("Contact_Email", typeof(string));
    
            var contact_Address1Parameter = contact_Address1 != null ?
                new ObjectParameter("Contact_Address1", contact_Address1) :
                new ObjectParameter("Contact_Address1", typeof(string));
    
            var contact_ZipParameter = contact_Zip != null ?
                new ObjectParameter("Contact_Zip", contact_Zip) :
                new ObjectParameter("Contact_Zip", typeof(string));
    
            var uRLParameter = uRL != null ?
                new ObjectParameter("URL", uRL) :
                new ObjectParameter("URL", typeof(string));
    
            var notesParameter = notes != null ?
                new ObjectParameter("Notes", notes) :
                new ObjectParameter("Notes", typeof(string));
    
            var contact_StateParameter = contact_State != null ?
                new ObjectParameter("Contact_State", contact_State) :
                new ObjectParameter("Contact_State", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("UPDATE_SUPPLIER", supplier_ID, company_NameParameter, contact_FirstNameParameter, contact_LastNameParameter, contact_PhoneNumberParameter, contact_EmailParameter, contact_Address1Parameter, contact_ZipParameter, uRLParameter, notesParameter, contact_StateParameter);
        }
    
        public virtual int UPDATE_USER(Nullable<long> user_ID, string first_Name, string last_Name, string phone_Number, string email, string user_Name, string password, Nullable<int> role_ID)
        {
            var user_IDParameter = user_ID.HasValue ?
                new ObjectParameter("User_ID", user_ID) :
                new ObjectParameter("User_ID", typeof(long));
    
            var first_NameParameter = first_Name != null ?
                new ObjectParameter("First_Name", first_Name) :
                new ObjectParameter("First_Name", typeof(string));
    
            var last_NameParameter = last_Name != null ?
                new ObjectParameter("Last_Name", last_Name) :
                new ObjectParameter("Last_Name", typeof(string));
    
            var phone_NumberParameter = phone_Number != null ?
                new ObjectParameter("Phone_Number", phone_Number) :
                new ObjectParameter("Phone_Number", typeof(string));
    
            var emailParameter = email != null ?
                new ObjectParameter("Email", email) :
                new ObjectParameter("Email", typeof(string));
    
            var user_NameParameter = user_Name != null ?
                new ObjectParameter("User_Name", user_Name) :
                new ObjectParameter("User_Name", typeof(string));
    
            var passwordParameter = password != null ?
                new ObjectParameter("Password", password) :
                new ObjectParameter("Password", typeof(string));
    
            var role_IDParameter = role_ID.HasValue ?
                new ObjectParameter("Role_ID", role_ID) :
                new ObjectParameter("Role_ID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("UPDATE_USER", user_IDParameter, first_NameParameter, last_NameParameter, phone_NumberParameter, emailParameter, user_NameParameter, passwordParameter, role_IDParameter);
        }
    }
}
