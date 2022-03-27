using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Generic;

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

				SetParameter(ref cm, "@User_ID", u.User_ID, SqlDbType.Int, Direction: ParameterDirection.Output);
				SetParameter(ref cm, "@First_Name", u.First_Name, SqlDbType.VarChar);
				SetParameter(ref cm, "@Last_Name", u.Last_Name, SqlDbType.VarChar);
				SetParameter(ref cm, "@Phone_Number", u.Phone_Number, SqlDbType.VarChar);
				SetParameter(ref cm, "@Email", u.Email, SqlDbType.VarChar);
				SetParameter(ref cm, "@Address_1", u.Address_1, SqlDbType.VarChar);
				SetParameter(ref cm, "@Address_2", u.Address_2, SqlDbType.VarChar);
				SetParameter(ref cm, "@Zip", u.Zip, SqlDbType.VarChar);
				SetParameter(ref cm, "@User_Name", u.User_Name, SqlDbType.VarChar);
				SetParameter(ref cm, "@Password", u.Password, SqlDbType.VarChar);
				SetParameter(ref cm, "@State_ID", u.State_ID, SqlDbType.Int);
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
						newUser.User_ID = (long)dr["User_ID"];
						newUser.User_Name = u.User_Name;
						newUser.Password = u.Password;
						newUser.First_Name = (string)dr["First_Name"];
						newUser.Last_Name = (string)dr["Last_Name"];
						newUser.Phone_Number = (string)dr["Phone_Number"];
						newUser.Address_1 = (string)dr["Address_1"];
						newUser.Address_2 = (string)dr["Address_2"];
						newUser.Zip = (string)dr["Zip"];
						newUser.State_ID = (string)dr["State_ID"];
						newUser.Role_ID = (string)dr["Role_ID"];
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

				SetParameter(ref cm, "@User_ID", u.User_ID, SqlDbType.Int);
				SetParameter(ref cm, "@First_Name", u.First_Name, SqlDbType.VarChar);
				SetParameter(ref cm, "@Last_Name", u.Last_Name, SqlDbType.VarChar);
				SetParameter(ref cm, "@Phone_Number", u.Phone_Number, SqlDbType.VarChar);
				SetParameter(ref cm, "@Email", u.Email, SqlDbType.VarChar);
				SetParameter(ref cm, "@Address_1", u.Address_1, SqlDbType.VarChar);
				SetParameter(ref cm, "@Address_2", u.Address_2, SqlDbType.VarChar);
				SetParameter(ref cm, "@Zip", u.Zip, SqlDbType.VarChar);
				SetParameter(ref cm, "@User_Name", u.User_Name, SqlDbType.VarChar);
				SetParameter(ref cm, "@Password", u.Password, SqlDbType.VarChar);
				SetParameter(ref cm, "@State_ID", u.State_ID, SqlDbType.Int);
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

		public Supplier.ActionTypes InsertSupplier(Supplier s)
		{
			try
			{
                SqlConnection cn = null;
                if (!GetDBConnection(ref cn)) throw new Exception("Database did not connect");
                SqlCommand cm = new SqlCommand("INSERT_SUPPLIER", cn);
                int intReturnValue = -1;

                SetParameter(ref cm, "@Supplier_ID", s.Supplier_ID, SqlDbType.Int, Direction: ParameterDirection.Output);
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
						s.Supplier_ID = (long)cm.Parameters["@Supplier"].Value;
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

				SetParameter(ref cm, "@Supplier_ID", s.Supplier_ID, SqlDbType.Int, Direction: ParameterDirection.Output);
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