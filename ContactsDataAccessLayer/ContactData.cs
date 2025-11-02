using System;
using System.Data;
using System.Data.SqlClient;
using System.Net.Sockets;
namespace ContactsDataAccessLayer
{
    public class clsContactDataAccess

    {
        public static bool GetContactInfoByID(int ID, ref String FirstName, ref String LastName,
            ref string Email, ref string Phone
            , ref string Address, ref int CountryID , ref DateTime DateofBirth, ref string Imegpath)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT * FROM Contacts where ContactID=@ContactID";
            
            SqlCommand command=new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ContactID", ID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound   = true;


                    FirstName = (string)reader["FirstName"];
                    LastName  = (string)reader["LastName"];
                    Email = (string)reader["Email"];
                    Phone = (string)reader["Phone"];
                    Address = (string)reader["Address"];
                    CountryID =(int)reader["CountryID"];
                    DateofBirth = (DateTime)reader["DateOfBirth"];

                    if (reader["Imegpath"] != DBNull.Value)
                    {
                        Imegpath = (string)reader["ImagePath"];

                    }
                    else
                        Imegpath = "";
                }
                else { isFound = false; }   
                    reader.Close();
            }
            catch 
            { 
            isFound=false;
            }
            finally
            {
            connection.Close(); 
            }


            return isFound;
        }

        public static int AddNewContact(string FirstName, string LastName,
            string Email, string Phone, string Address,
            DateTime DateOfBirth, int CountryID, string ImagePath)
        {
            SqlConnection conecction = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"insert into Contacts (FirstName, LastName, Email, Phone, Address,DateOfBirth, CountryID,ImagePath)
                             VALUES (@FirstName, @LastName, @Email, @Phone, @Address,@DateOfBirth, @CountryID,@ImagePath);
                             SELECT SCOPE_IDENTITY(); ";
            SqlCommand command=new SqlCommand(query, conecction);

            command.Parameters.AddWithValue("@FirstName", FirstName);
            command.Parameters.AddWithValue("@LastName", LastName);
            command.Parameters.AddWithValue("@Email", Email);
            command.Parameters.AddWithValue("@Phone", Phone);
            command.Parameters.AddWithValue("@Address", Address);
            command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            command.Parameters.AddWithValue("@CountryID", CountryID);
            command.Parameters.AddWithValue("@ImagePath", ImagePath);
            try
            {
                conecction.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    return insertedID;
                }
                else

                {
                    return -1;
                }
            }
            catch 
            {
               //Console.WriteLine("Error: " + ex.Message);

            }
            finally { conecction.Close(); }
            return -1;
        }
        public static bool UpdateContact(int ID, string FirstName, string LastName,
            string Email, string Phone, string Address, DateTime DateOfBirth, int CountryID, string ImagePath)
        {

            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Update  Contacts  
                            set FirstName = @FirstName, 
                                LastName = @LastName, 
                                Email = @Email, 
                                Phone = @Phone, 
                                Address = @Address, 
                                DateOfBirth = @DateOfBirth,
                                CountryID = @CountryID,
                                ImagePath =@ImagePath
                                where ContactID = @ContactID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ContactID", ID);
            command.Parameters.AddWithValue("@FirstName", FirstName);
            command.Parameters.AddWithValue("@LastName", LastName);
            command.Parameters.AddWithValue("@Email", Email);
            command.Parameters.AddWithValue("@Phone", Phone);
            command.Parameters.AddWithValue("@Address", Address);
            command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            command.Parameters.AddWithValue("@CountryID", CountryID);
            command.Parameters.AddWithValue("@ImagePath", ImagePath);

            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
                return false;
            }

            finally
            {
                connection.Close();
            }

            return (rowsAffected > 0);
        }


    }
}
