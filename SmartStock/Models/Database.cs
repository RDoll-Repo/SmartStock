using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SmartStock.Models;
using System.Data.Entity;
using System.Dynamic;

namespace SmartStock.Models
{
	public class Database
	{

		public User.ActionTypes InsertUser(User u)
		{
			try
			{
				SqlConnection cn = null;
				if (!GetDBConnection(ref cn)) throw new Exception("Database did not connect");
				SqlCommand cm = new SqlCommand("INSERT_USER", cn);
				int intReturnValue = -1;

				SetParameter(ref cm, "@User_ID", u.User_ID, SqlDbType.BigInt, Direction: ParameterDirection.Output);
				SetParameter(ref cm, "@First_Name", u.First_Name, SqlDbType.VarChar);
				SetParameter(ref cm, "@Last_Name", u.Last_Name, SqlDbType.VarChar);
				SetParameter(ref cm, "@Phone_Number", u.Phone_Number, SqlDbType.VarChar);
				SetParameter(ref cm, "@Email", u.Email, SqlDbType.VarChar);
				SetParameter(ref cm, "@User_Name", u.User_Name, SqlDbType.VarChar);
				SetParameter(ref cm, "@Password", u.Password, SqlDbType.VarChar);
				SetParameter(ref cm, "@Role_ID", u.Role_ID, SqlDbType.Int);


				SetParameter(ref cm, "ReturnValue", 0, SqlDbType.TinyInt, Direction: ParameterDirection.ReturnValue);

				cm.ExecuteReader();

				intReturnValue = (int)cm.Parameters["ReturnValue"].Value;
				CloseDBConnection(ref cn);

				switch (intReturnValue)
				{
					case 1: // new user created
						u.User_ID = (long)cm.Parameters["@User_ID"].Value;
						return User.ActionTypes.InsertSuccessful;
					case -1:
						return User.ActionTypes.DuplicateEmail;
					case -2:
						return User.ActionTypes.DuplicateUserID;
					default:
						return User.ActionTypes.Unknown;
				}
			}
			catch (Exception ex) { throw new Exception(ex.Message); }
		}



		public User Login(User u)
		{
			try
			{
				SqlConnection cn = new SqlConnection();
				if (!GetDBConnection(ref cn)) throw new Exception("Database did not connect");
				SqlDataAdapter da = new SqlDataAdapter("LOGIN", cn);
				DataSet ds;
				User newUser = null;

				da.SelectCommand.CommandType = CommandType.StoredProcedure;

				SetParameter(ref da, "@User_Name", u.User_Name, SqlDbType.VarChar);
				SetParameter(ref da, "@Password", u.Password, SqlDbType.VarChar);

				try
				{
					ds = new DataSet();
					da.Fill(ds);
					if (ds.Tables[0].Rows.Count > 0)
					{
						newUser = new User();
						DataRow dr = ds.Tables[0].Rows[0];
						newUser.User_ID = (int)dr["intUserID"];
						newUser.User_Name = u.User_Name;
						newUser.Password = u.Password;
						newUser.First_Name = (string)dr["strFirstName"];
						newUser.Last_Name = (string)dr["strLastName"];
						newUser.Phone_Number = (string)dr["strPhoneNumber"];
						newUser.Role_ID = (int)dr["intRoleID"];
					}
				}


				catch (Exception ex) { throw new Exception(ex.Message); }
				finally
				{
					CloseDBConnection(ref cn);
				}
				return newUser;
			}
			catch (Exception ex) { throw new Exception(ex.Message); }
		}

		public User.ActionTypes UpdateUser(User u)
		{
			try
			{
				SqlConnection cn = null;
				if (!GetDBConnection(ref cn)) throw new Exception("Database did not connect");
				SqlCommand cm = new SqlCommand("UPDATE_USER", cn);
				int intReturnValue = -1;

				SetParameter(ref cm, "@User_ID", u.User_ID, SqlDbType.BigInt);
				SetParameter(ref cm, "@First_Name", u.First_Name, SqlDbType.VarChar);
				SetParameter(ref cm, "@Last_Name", u.Last_Name, SqlDbType.VarChar);
				SetParameter(ref cm, "@Phone_Number", u.Phone_Number, SqlDbType.VarChar);
				SetParameter(ref cm, "@Email", u.Email, SqlDbType.VarChar);
				SetParameter(ref cm, "@User_Name", u.User_Name, SqlDbType.VarChar);
				SetParameter(ref cm, "@Password", u.Password, SqlDbType.VarChar);
				SetParameter(ref cm, "@Role_ID", u.Role_ID, SqlDbType.Int);

				SetParameter(ref cm, "ReturnValue", 0, SqlDbType.Int, Direction: ParameterDirection.ReturnValue);

				cm.ExecuteReader();

				intReturnValue = (int)cm.Parameters["ReturnValue"].Value;
				CloseDBConnection(ref cn);

				switch (intReturnValue)
				{
					case 1: //new updated
						return User.ActionTypes.UpdateSuccessful;
					default:
						return User.ActionTypes.Unknown;
				}
			}
			catch (Exception ex) { throw new Exception(ex.Message); }
		}

		public User.ActionTypes DeleteUser(User u)
		{
			try
			{
				SqlConnection cn = null;
				if (!GetDBConnection(ref cn)) throw new Exception("Database did not connect");
				SqlCommand cm = new SqlCommand("DELETE_USER", cn);
				int intReturnValue = -1;

				SetParameter(ref cm, "@User_ID", u.User_ID, SqlDbType.BigInt);

				SetParameter(ref cm, "ReturnValue", 0, SqlDbType.Int, Direction: ParameterDirection.ReturnValue);

				cm.ExecuteReader();

				intReturnValue = (int)cm.Parameters["ReturnValue"].Value;
				CloseDBConnection(ref cn);

				switch (intReturnValue)
				{
					case 1: //new updated
						return User.ActionTypes.DeleteSuccessful;
					default:
						return User.ActionTypes.Unknown;
				}
			}
			catch (Exception ex) { throw new Exception(ex.Message); }
		}

		public Supplier.ActionTypes InsertSupplier(Supplier s)
		{
			try
			{
				SqlConnection cn = null;
				if (!GetDBConnection(ref cn)) throw new Exception("Database did not connect");
				SqlCommand cm = new SqlCommand("INSERT_SUPPLIER", cn);
				int intReturnValue = -1;

				SetParameter(ref cm, "@Supplier_ID", s.Supplier_ID, SqlDbType.BigInt, Direction: ParameterDirection.Output);
				SetParameter(ref cm, "@Company_Name", s.Company_Name, SqlDbType.VarChar);
				SetParameter(ref cm, "@Contact_FirstName", s.Contact_FirstName, SqlDbType.VarChar);
				SetParameter(ref cm, "@Contact_LastName", s.Contact_LastName, SqlDbType.VarChar);
				SetParameter(ref cm, "@Contact_PhoneNumber", s.Contact_PhoneNumber, SqlDbType.VarChar);
				SetParameter(ref cm, "@Contact_Email", s.Contact_Email, SqlDbType.VarChar);
				SetParameter(ref cm, "@Contact_Address1", s.Contact_Address1, SqlDbType.VarChar);
				SetParameter(ref cm, "@Contact_Zip", s.Contact_Zip, SqlDbType.VarChar);
				SetParameter(ref cm, "@URL", s.URL, SqlDbType.VarChar);
				SetParameter(ref cm, "@Notes", s.Notes, SqlDbType.VarChar);
				SetParameter(ref cm, "@Contact_State", s.Contact_State, SqlDbType.VarChar);


				SetParameter(ref cm, "ReturnValue", 0, SqlDbType.TinyInt, Direction: ParameterDirection.ReturnValue);

				cm.ExecuteReader();

				intReturnValue = (int)cm.Parameters["ReturnValue"].Value;
				CloseDBConnection(ref cn);

				switch (intReturnValue)
				{
					case 1: // new Supplier created
						s.Supplier_ID = (long)cm.Parameters["@Supplier_ID"].Value;
						return Supplier.ActionTypes.InsertSuccessful;
					case -1:
						return Supplier.ActionTypes.DuplicateEmail;
					case -2:
						return Supplier.ActionTypes.DuplicateUserID;
					default:
						return Supplier.ActionTypes.Unknown;
				}
			}
			catch (Exception ex) { throw new Exception(ex.Message); }
		}



		public Supplier.ActionTypes UpdateSupplier(Supplier s)
		{
			try
			{
				SqlConnection cn = null;
				if (!GetDBConnection(ref cn)) throw new Exception("Database did not connect");
				SqlCommand cm = new SqlCommand("UPDATE_SUPPLIER", cn);
				int intReturnValue = -1;

				SetParameter(ref cm, "@Supplier_ID", s.Supplier_ID, SqlDbType.BigInt);
				SetParameter(ref cm, "@Company_Name", s.Company_Name, SqlDbType.VarChar);
				SetParameter(ref cm, "@Contact_FirstName", s.Contact_FirstName, SqlDbType.VarChar);
				SetParameter(ref cm, "@Contact_LastName", s.Contact_LastName, SqlDbType.VarChar);
				SetParameter(ref cm, "@Contact_PhoneNumber", s.Contact_PhoneNumber, SqlDbType.VarChar);
				SetParameter(ref cm, "@Contact_Email", s.Contact_Email, SqlDbType.VarChar);
				SetParameter(ref cm, "@Contact_Address1", s.Contact_Address1, SqlDbType.VarChar);
				SetParameter(ref cm, "@Contact_Zip", s.Contact_Zip, SqlDbType.VarChar);
				SetParameter(ref cm, "@URL", s.URL, SqlDbType.VarChar);
				SetParameter(ref cm, "@Notes", s.Notes, SqlDbType.VarChar);
				SetParameter(ref cm, "@Contact_State", s.Contact_State, SqlDbType.VarChar);

				SetParameter(ref cm, "ReturnValue", 0, SqlDbType.Int, Direction: ParameterDirection.ReturnValue);

				cm.ExecuteReader();

				intReturnValue = (int)cm.Parameters["ReturnValue"].Value;
				CloseDBConnection(ref cn);

				switch (intReturnValue)
				{
					case 1: //new updated
						return Supplier.ActionTypes.UpdateSuccessful;
					default:
						return Supplier.ActionTypes.Unknown;
				}
			}
			catch (Exception ex) { throw new Exception(ex.Message); }
		}

		public Supplier.ActionTypes DeleteSupplier(Supplier s)
		{
			try
			{
				SqlConnection cn = null;
				if (!GetDBConnection(ref cn)) throw new Exception("Database did not connect");
				SqlCommand cm = new SqlCommand("DELETE_SUPPLIER", cn);
				int intReturnValue = -1;

				SetParameter(ref cm, "@Supplier_ID", s.Supplier_ID, SqlDbType.BigInt);

				SetParameter(ref cm, "ReturnValue", 0, SqlDbType.Int, Direction: ParameterDirection.ReturnValue);

				cm.ExecuteReader();

				intReturnValue = (int)cm.Parameters["ReturnValue"].Value;
				CloseDBConnection(ref cn);

				switch (intReturnValue)
				{
					case 1: //new updated
						return Supplier.ActionTypes.DeleteSuccessful;
					default:
						return Supplier.ActionTypes.Unknown;
				}
			}
			catch (Exception ex) { throw new Exception(ex.Message); }
		}

		public ProductPriceHistory.ActionTypes InsertProductPrice(ProductPriceHistory pph)
		{
			try
			{
				SqlConnection cn = null;
				if (!GetDBConnection(ref cn)) throw new Exception("Database did not connect");
				SqlCommand cm = new SqlCommand("INSERT_PRODUCTPRICEHISTORY", cn);
				int intReturnValue = -1;

				SetParameter(ref cm, "@PPHID", pph.ProductPriceHistoryID, SqlDbType.BigInt, Direction: ParameterDirection.Output);
				SetParameter(ref cm, "@ProductName", pph.ProductName, SqlDbType.VarChar);
				SetParameter(ref cm, "@PurchaseDate", pph.PurchaseDate, SqlDbType.DateTime);
				SetParameter(ref cm, "@CostPerUnit", pph.CostPerUnit, SqlDbType.Money);
				SetParameter(ref cm, "@UnitType", pph.UnitType, SqlDbType.VarChar);
				SetParameter(ref cm, "@PurchaseAmt", pph.PurchaseAmt, SqlDbType.Int);
				SetParameter(ref cm, "@UserID", pph.UserID, SqlDbType.Int);
				SetParameter(ref cm, "@SupplierID", pph.SupplierID, SqlDbType.Int);

				SetParameter(ref cm, "ReturnValue", 0, SqlDbType.TinyInt, Direction: ParameterDirection.ReturnValue);

				cm.ExecuteReader();

				intReturnValue = (int)cm.Parameters["ReturnValue"].Value;
				CloseDBConnection(ref cn);

				switch (intReturnValue)
				{
					case 1: // new Supplier created
						pph.ProductPriceHistoryID = (long)cm.Parameters["@PPHID"].Value;
						return ProductPriceHistory.ActionTypes.InsertSuccessful;
					case -1:
						return ProductPriceHistory.ActionTypes.DuplicateEmail;
					case -2:
						return ProductPriceHistory.ActionTypes.DuplicateUserID;
					default:
						return ProductPriceHistory.ActionTypes.Unknown;
				}
			}
			catch (Exception ex) { throw new Exception(ex.Message); }
		}



		public ProductPriceHistory.ActionTypes UpdateProductPrice(ProductPriceHistory pph)
		{
			try
			{
				SqlConnection cn = null;
				if (!GetDBConnection(ref cn)) throw new Exception("Database did not connect");
				SqlCommand cm = new SqlCommand("UPDATE_PRODUCTPRICEHISTORY", cn);
				int intReturnValue = -1;

				SetParameter(ref cm, "@PPHID", pph.ProductPriceHistoryID, SqlDbType.BigInt, Direction: ParameterDirection.Output);
				SetParameter(ref cm, "@Product_Name", pph.ProductName, SqlDbType.VarChar);
				SetParameter(ref cm, "@PurchaseDate", pph.PurchaseDate, SqlDbType.DateTime);
				SetParameter(ref cm, "@CostPerUnit", pph.CostPerUnit, SqlDbType.Money);
				SetParameter(ref cm, "@PurchaseAmt", pph.PurchaseAmt, SqlDbType.Int);
				SetParameter(ref cm, "@UnitType", pph.UnitType, SqlDbType.VarChar);
				SetParameter(ref cm, "@UserID", pph.UserID, SqlDbType.Int);
				SetParameter(ref cm, "@SupplierID", pph.SupplierID, SqlDbType.Int);

				SetParameter(ref cm, "ReturnValue", 0, SqlDbType.Int, Direction: ParameterDirection.ReturnValue);

				cm.ExecuteReader();

				intReturnValue = (int)cm.Parameters["ReturnValue"].Value;
				CloseDBConnection(ref cn);

				switch (intReturnValue)
				{
					case 1: //new updated
						return ProductPriceHistory.ActionTypes.UpdateSuccessful;
					default:
						return ProductPriceHistory.ActionTypes.Unknown;
				}
			}
			catch (Exception ex) { throw new Exception(ex.Message); }
		}

		public ProductPriceHistory.ActionTypes DeleteProductPrice(ProductPriceHistory pph)
		{
			try
			{
				SqlConnection cn = null;
				if (!GetDBConnection(ref cn)) throw new Exception("Database did not connect");
				SqlCommand cm = new SqlCommand("DELETE_PRODUCTPRICEHISTORY", cn);
				int intReturnValue = -1;

				SetParameter(ref cm, "@PPHID", pph.ProductPriceHistoryID, SqlDbType.BigInt);

				SetParameter(ref cm, "ReturnValue", 0, SqlDbType.Int, Direction: ParameterDirection.ReturnValue);

				cm.ExecuteReader();

				intReturnValue = (int)cm.Parameters["ReturnValue"].Value;
				CloseDBConnection(ref cn);

				switch (intReturnValue)
				{
					case 1: //new updated
						return ProductPriceHistory.ActionTypes.DeleteSuccessful;
					default:
						return ProductPriceHistory.ActionTypes.Unknown;
				}
			}
			catch (Exception ex) { throw new Exception(ex.Message); }
		}

		public Inventory.ActionTypes InsertInventory(Inventory i)
		{
			try
			{
				SqlConnection cn = null;
				if (!GetDBConnection(ref cn)) throw new Exception("Database did not connect");
				SqlCommand cm = new SqlCommand("INSERT_INVENTORY", cn);
				int intReturnValue = -1;

				SetParameter(ref cm, "@InventoryID", i.InventoryID, SqlDbType.BigInt, Direction: ParameterDirection.Output);
				SetParameter(ref cm, "@ProductName", i.ProductName, SqlDbType.VarChar);
				SetParameter(ref cm, "@InvCount", i.InvCount, SqlDbType.Int);
				SetParameter(ref cm, "@blnIsLow", i.blnIsLow, SqlDbType.VarChar);
				SetParameter(ref cm, "@UnitType", i.UnitType, SqlDbType.VarChar);
				SetParameter(ref cm, "@CategoryID", i.CategoryID, SqlDbType.Int);
				SetParameter(ref cm, "@ProductlocationID", i.ProductlocationID, SqlDbType.Int);

				SetParameter(ref cm, "ReturnValue", 0, SqlDbType.TinyInt, Direction: ParameterDirection.ReturnValue);

				cm.ExecuteReader();

				intReturnValue = (int)cm.Parameters["ReturnValue"].Value;
				CloseDBConnection(ref cn);

				switch (intReturnValue)
				{
					case 1: // new Supplier created
						i.InventoryID = (long)cm.Parameters["@InventoryID"].Value;
						return Inventory.ActionTypes.InsertSuccessful;
					case -1:
						return Inventory.ActionTypes.DuplicateEmail;
					case -2:
						return Inventory.ActionTypes.DuplicateUserID;
					default:
						return Inventory.ActionTypes.Unknown;
				}
			}
			catch (Exception ex) { throw new Exception(ex.Message); }
		}



		public Inventory.ActionTypes UpdateInventory(Inventory i)
		{
			try
			{
				SqlConnection cn = null;
				if (!GetDBConnection(ref cn)) throw new Exception("Database did not connect");
				SqlCommand cm = new SqlCommand("UPDATE_INVENTORY", cn);
				int intReturnValue = -1;

				SetParameter(ref cm, "@InventoryID", i.InventoryID, SqlDbType.BigInt);
				SetParameter(ref cm, "@ProductName", i.ProductName, SqlDbType.VarChar);
				SetParameter(ref cm, "@InvCount", i.InvCount, SqlDbType.Int);
				SetParameter(ref cm, "@blnIsLow", i.blnIsLow, SqlDbType.VarChar);
				SetParameter(ref cm, "@UnitType", i.UnitType, SqlDbType.VarChar);
				SetParameter(ref cm, "@CategoryID", i.CategoryID, SqlDbType.Int);
				SetParameter(ref cm, "@ProductlocationID", i.ProductlocationID, SqlDbType.Int);

				SetParameter(ref cm, "ReturnValue", 0, SqlDbType.Int, Direction: ParameterDirection.ReturnValue);

				cm.ExecuteReader();

				intReturnValue = (int)cm.Parameters["ReturnValue"].Value;
				CloseDBConnection(ref cn);

				switch (intReturnValue)
				{
					case 1: //new updated
						return Inventory.ActionTypes.UpdateSuccessful;
					default:
						return Inventory.ActionTypes.Unknown;
				}
			}
			catch (Exception ex) { throw new Exception(ex.Message); }
		}

		public Inventory.ActionTypes AuditInventory(Inventory i)
		{
			try
			{
				SqlConnection cn = null;
				if (!GetDBConnection(ref cn)) throw new Exception("Database did not connect");
				SqlCommand cm = new SqlCommand("AUDIT_INVENTORY", cn);
				int intReturnValue = -1;

				SetParameter(ref cm, "@InventoryID", i.InventoryID, SqlDbType.BigInt);
				SetParameter(ref cm, "@InvCount", i.InvCount, SqlDbType.Int);

				SetParameter(ref cm, "ReturnValue", 0, SqlDbType.Int, Direction: ParameterDirection.ReturnValue);

				cm.ExecuteReader();

				intReturnValue = (int)cm.Parameters["ReturnValue"].Value;
				CloseDBConnection(ref cn);

				switch (intReturnValue)
				{
					case 1: //new updated
						return Inventory.ActionTypes.UpdateSuccessful;
					default:
						return Inventory.ActionTypes.Unknown;
				}
			}
			catch (Exception ex) { throw new Exception(ex.Message); }
		}

		public Inventory.ActionTypes DeliveryInventory(Inventory i)
		{
			try
			{
				SqlConnection cn = null;
				if (!GetDBConnection(ref cn)) throw new Exception("Database did not connect");
				SqlCommand cm = new SqlCommand("DELIVERY_INVENTORY", cn);
				int intReturnValue = -1;

				SetParameter(ref cm, "@InventoryID", i.InventoryID, SqlDbType.BigInt);
				SetParameter(ref cm, "@InvCount", i.InvCount, SqlDbType.Int);

				SetParameter(ref cm, "ReturnValue", 0, SqlDbType.Int, Direction: ParameterDirection.ReturnValue);

				cm.ExecuteReader();

				intReturnValue = (int)cm.Parameters["ReturnValue"].Value;
				CloseDBConnection(ref cn);

				switch (intReturnValue)
				{
					case 1: //new updated
						return Inventory.ActionTypes.UpdateSuccessful;
					default:
						return Inventory.ActionTypes.Unknown;
				}
			}
			catch (Exception ex) { throw new Exception(ex.Message); }
		}

		public ProductPriceHistory.ActionTypes DeliveryProductPrice(ProductPriceHistory pph)
		{
			try
			{
				SqlConnection cn = null;
				if (!GetDBConnection(ref cn)) throw new Exception("Database did not connect");
				SqlCommand cm = new SqlCommand("DELIVERY_PRODUCTPRICE", cn);
				int intReturnValue = -1;

				SetParameter(ref cm, "@PPHID", pph.ProductPriceHistoryID, SqlDbType.BigInt, Direction: ParameterDirection.Output);
				SetParameter(ref cm, "@ProductName", pph.ProductName, SqlDbType.VarChar);
				SetParameter(ref cm, "@CostPerUnit", pph.CostPerUnit, SqlDbType.Money);
				SetParameter(ref cm, "@PurchaseAmt", pph.PurchaseAmt, SqlDbType.Int);
				SetParameter(ref cm, "@PurchaseDate", pph.PurchaseDate, SqlDbType.DateTime);


				SetParameter(ref cm, "ReturnValue", 0, SqlDbType.Int, Direction: ParameterDirection.ReturnValue);

				cm.ExecuteReader();

				intReturnValue = (int)cm.Parameters["ReturnValue"].Value;
				CloseDBConnection(ref cn);

				switch (intReturnValue)
				{
					case 1: //new updated
						return ProductPriceHistory.ActionTypes.UpdateSuccessful;
					default:
						return ProductPriceHistory.ActionTypes.Unknown;
				}
			}
			catch (Exception ex) { throw new Exception(ex.Message); }
		}

		public Inventory.ActionTypes DeleteInventory(Inventory i)
		{
			try
			{
				SqlConnection cn = null;
				if (!GetDBConnection(ref cn)) throw new Exception("Database did not connect");
				SqlCommand cm = new SqlCommand("DELETE_INVENTORY", cn);
				int intReturnValue = -1;

				SetParameter(ref cm, "@InventoryID", i.InventoryID, SqlDbType.BigInt);

				SetParameter(ref cm, "ReturnValue", 0, SqlDbType.Int, Direction: ParameterDirection.ReturnValue);

				cm.ExecuteReader();

				intReturnValue = (int)cm.Parameters["ReturnValue"].Value;
				CloseDBConnection(ref cn);

				switch (intReturnValue)
				{
					case 1: //new updated
						return Inventory.ActionTypes.DeleteSuccessful;
					default:
						return Inventory.ActionTypes.Unknown;
				}
			}
			catch (Exception ex) { throw new Exception(ex.Message); }
		}

		public Card.CardTypes InsertCard(Card c)
		{
			try
			{
				SqlConnection cn = null;
				if (!GetDBConnection(ref cn)) throw new Exception("Database did not connect");
				SqlCommand cm = new SqlCommand("INSERT_ALERT", cn);
				int intReturnValue = -1;

				SetParameter(ref cm, "@AlertID", c.Card_ID, SqlDbType.BigInt, Direction: ParameterDirection.Output);
				SetParameter(ref cm, "@Alert", c.Message, SqlDbType.VarChar);
				SetParameter(ref cm, "@AlertDate", c.AlertDateTime, SqlDbType.DateTime);

				SetParameter(ref cm, "ReturnValue", 0, SqlDbType.TinyInt, Direction: ParameterDirection.ReturnValue);

				cm.ExecuteReader();

				intReturnValue = (int)cm.Parameters["ReturnValue"].Value;
				CloseDBConnection(ref cn);

				switch (intReturnValue)
				{
					case 1: // new Supplier created
						c.Card_ID = (long)cm.Parameters["@AlertID"].Value;
						return Card.CardTypes.InsertSuccessful;
					default:
						return Card.CardTypes.Unknown;
				}
			}
			catch (Exception ex) { throw new Exception(ex.Message); }
		}



		public Card.CardTypes UpdateCard(Card c)
		{
			try
			{
				SqlConnection cn = null;
				if (!GetDBConnection(ref cn)) throw new Exception("Database did not connect");
				SqlCommand cm = new SqlCommand("UPDATE_ALERT", cn);
				int intReturnValue = -1;

				SetParameter(ref cm, "@AlertID", c.Card_ID, SqlDbType.BigInt);
				SetParameter(ref cm, "@Alert", c.Message, SqlDbType.VarChar);
				SetParameter(ref cm, "@AlertDate", c.AlertDateTime, SqlDbType.DateTime);

				SetParameter(ref cm, "ReturnValue", 0, SqlDbType.Int, Direction: ParameterDirection.ReturnValue);

				cm.ExecuteReader();

				intReturnValue = (int)cm.Parameters["ReturnValue"].Value;
				CloseDBConnection(ref cn);

				switch (intReturnValue)
				{
					case 1: //new updated
						return Card.CardTypes.UpdateSuccessful;
					default:
						return Card.CardTypes.Unknown;
				}
			}
			catch (Exception ex) { throw new Exception(ex.Message); }
		}

		public Card.CardTypes DeleteCard(Card c)
		{
			try
			{
				SqlConnection cn = null;
				if (!GetDBConnection(ref cn)) throw new Exception("Database did not connect");
				SqlCommand cm = new SqlCommand("DELETE_ALERT", cn);
				int intReturnValue = -1;

				SetParameter(ref cm, "@AlertID", c.Card_ID, SqlDbType.BigInt);

				SetParameter(ref cm, "ReturnValue", 0, SqlDbType.Int, Direction: ParameterDirection.ReturnValue);

				cm.ExecuteReader();

				intReturnValue = (int)cm.Parameters["ReturnValue"].Value;
				CloseDBConnection(ref cn);

				switch (intReturnValue)
				{
					case 1: //new updated
						return Card.CardTypes.DeleteSuccessful;
					default:
						return Card.CardTypes.Unknown;
				}
			}
			catch (Exception ex) { throw new Exception(ex.Message); }
		}

		public Inventory.ActionTypes GetAvgValue(Inventory i)
		{
			if(i.ProductName != null)
			{ 
				SqlConnection cn = null;
				if (!GetDBConnection(ref cn)) throw new Exception("Database did not connect");
				SqlCommand cm = new SqlCommand("AVG_INV", cn);

				SetParameter(ref cm, "@ProductName", i.ProductName, SqlDbType.VarChar);

				var priceChangeComp = cm.Parameters.Add("@priceChangeComp", SqlDbType.Money);
				priceChangeComp.Direction = ParameterDirection.ReturnValue;

				cm.ExecuteReader();

				var average = Convert.ToDouble(priceChangeComp.Value);

				CloseDBConnection(ref cn);

				DBModel dbContext = new DBModel();

				var Inv = dbContext.TInventories.ToList();
				var log = dbContext.TProductPriceHistories.ToList();
				foreach (var item in Inv)
				{
					if (item.strProductName != null)
					{
						i.InvCount = item.intInvCount;
					}
					i.invCheck();

				}
				double Invcount = Convert.ToDouble(i.InvCount);
				double lowCheck = (Invcount * .2);
				if (average < lowCheck)
				{
					i.blnIsLow = true;
					Card y = new Card();
					y.CardType = Card.CardTypes.StockAlert;
					y.AlertDateTime = DateTime.Now;
					y.Message = ($"You are running low on {i.ProductName} , be sure to order more soon. ");
				}
				else
				{
					i.blnIsLow = false;
				}

				Models.Card c = new Models.Card();
				SqlConnection cnn = null;
				if (!GetDBConnection(ref cnn)) throw new Exception("Database did not connect");
				SqlCommand cmm = new SqlCommand("INSERT_ALERT", cnn);

				SetParameter(ref cm, "@Alert", c.Message, SqlDbType.VarChar);

				cm.ExecuteReader();
				CloseDBConnection(ref cn);
			}
			return Inventory.ActionTypes.Unknown;
		}

		private bool GetDBConnection(ref SqlConnection SQLConn)
		{
			try
			{
				if (SQLConn == null) SQLConn = new SqlConnection();
				if (SQLConn.State != ConnectionState.Open)
				{
					SQLConn.ConnectionString = ConfigurationManager.AppSettings["AppDBConnect"];
					SQLConn.Open();
				}
				return true;
			}
			catch (Exception ex) { throw new Exception(ex.Message); }
		}

		private bool CloseDBConnection(ref SqlConnection SQLConn)
		{
			try
			{
				if (SQLConn.State != ConnectionState.Closed)
				{
					SQLConn.Close();
					SQLConn.Dispose();
					SQLConn = null;
				}
				return true;
			}
			catch (Exception ex) { throw new Exception(ex.Message); }
		}

		private int SetParameter(ref SqlCommand cm, string ParameterName, Object Value
			, SqlDbType ParameterType, int FieldSize = -1
			, ParameterDirection Direction = ParameterDirection.Input
			, Byte Precision = 0, Byte Scale = 0)
		{
			try
			{
				cm.CommandType = CommandType.StoredProcedure;
				if (FieldSize == -1)
					cm.Parameters.Add(ParameterName, ParameterType);
				else
					cm.Parameters.Add(ParameterName, ParameterType, FieldSize);

				if (Precision > 0) cm.Parameters[cm.Parameters.Count - 1].Precision = Precision;
				if (Scale > 0) cm.Parameters[cm.Parameters.Count - 1].Scale = Scale;

				cm.Parameters[cm.Parameters.Count - 1].Value = Value;
				cm.Parameters[cm.Parameters.Count - 1].Direction = Direction;

				return 0;
			}
			catch (Exception ex) { throw new Exception(ex.Message); }
		}

		private int SetParameter(ref SqlDataAdapter cm, string ParameterName, Object Value
			, SqlDbType ParameterType, int FieldSize = -1
			, ParameterDirection Direction = ParameterDirection.Input
			, Byte Precision = 0, Byte Scale = 0)
		{
			try
			{
				cm.SelectCommand.CommandType = CommandType.StoredProcedure;
				if (FieldSize == -1)
					cm.SelectCommand.Parameters.Add(ParameterName, ParameterType);
				else
					cm.SelectCommand.Parameters.Add(ParameterName, ParameterType, FieldSize);

				if (Precision > 0) cm.SelectCommand.Parameters[cm.SelectCommand.Parameters.Count - 1].Precision = Precision;
				if (Scale > 0) cm.SelectCommand.Parameters[cm.SelectCommand.Parameters.Count - 1].Scale = Scale;

				cm.SelectCommand.Parameters[cm.SelectCommand.Parameters.Count - 1].Value = Value;
				cm.SelectCommand.Parameters[cm.SelectCommand.Parameters.Count - 1].Direction = Direction;

				return 0;
			}
			catch (Exception ex) { throw new Exception(ex.Message); }
		}
	}
}