using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace AddressBookSQL
{
    public class addressbooksql
    {
        string connectionString = @"Data Source=(LocalDb)\localdb;Initial Catalog=AddressBookSystem;Integrated Security=True";

        public int GetPersonDetailsfromDatabase()
        {
            int Count = 0;
            try
            {
                AddressBookModel addressBookModel = new AddressBookModel();
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string retrieveQuery = @"select p.person_id,p.firstname,p.lastname,p.phone,p.email,ab.book_name,ab.book_id,ab.book_type,p.zip ,a.city,a.state from person p inner join address a on p.zip=a.zip inner join Person_addressbook pa on pa.person_id=p.person_id inner join addressbook ab on ab.book_id=pa.book_id";
                    ;
                    SqlCommand sqlCommand = new SqlCommand(retrieveQuery, connection);
                    connection.Open();
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    if (sqlDataReader.HasRows)
                    {
                        Console.WriteLine("Address Book Services Database has following Contact details right now");
                        while (sqlDataReader.Read())
                        {
                            addressBookModel.personid = sqlDataReader.GetInt32(0);
                            addressBookModel.firstname = sqlDataReader.GetString(1);
                            addressBookModel.lastname = sqlDataReader.GetString(2);
                            addressBookModel.phone = sqlDataReader.GetString(3);
                            addressBookModel.emailid = sqlDataReader.GetString(4);
                            addressBookModel.bookid = sqlDataReader.GetInt32(6);
                            addressBookModel.city = sqlDataReader.GetString(9);
                            addressBookModel.zip = sqlDataReader.GetInt32(8);
                            addressBookModel.state = sqlDataReader.GetString(10);
                            addressBookModel.bookname = sqlDataReader.GetString(5);
                            addressBookModel.booktype = sqlDataReader.GetString(7);
                            Count++;
                            Console.WriteLine("{0}, {1}, {2}, {4}, {5}, {6}, {7}, {8}, {9}, {10}", addressBookModel.personid, addressBookModel.firstname, addressBookModel.lastname,
                                addressBookModel.phone, addressBookModel.emailid, addressBookModel.bookid, addressBookModel.city, addressBookModel.zip, addressBookModel.state, addressBookModel.bookname, addressBookModel.booktype);
                        }
                        Console.WriteLine("New Contact Added Successfully");
                        sqlDataReader.Close();
                    }
                    connection.Close();
                }
                return Count;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public bool UpdateContact(AddressBookModel model)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand("SpUpdateContact", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@first_name", model.firstname);
                    command.Parameters.AddWithValue("@last_name", model.lastname);
                    command.Parameters.AddWithValue("@phone_no", model.phone);
                    command.Parameters.AddWithValue("@email1", model.emailid);
                    command.Parameters.AddWithValue("@zip1", model.zip);


                    connection.Open();
                    var result = command.ExecuteNonQuery();
                    Console.WriteLine("Contact Updated Successfully !");
                    connection.Close();
                    if (result == 0)
                    {
                        return false;
                    }
                    return true;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public bool RetriveContactInParticularPeriod()
        {

            try
            {
                AddressBookModel addressBookModel = new AddressBookModel();
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string retrieveQuery = @"select p.personid,p.firstname,p.lastname,p.date_added,p.phone,p.email,ab.bookname,ab.bookid,ab.booktype,p.zip ,a.city,a.state from person p inner join address a on p.zip=a.zip inner join
                                            Person_addressbook pa on pa.personid=p.personid inner join addressbook ab on ab.bookid=pa.bookid where p.dateadded between '2016-01-01' and getdate();";

                    SqlCommand sqlCommand = new SqlCommand(retrieveQuery, connection);
                    connection.Open();
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    if (sqlDataReader.HasRows)
                    {
                        while (sqlDataReader.Read())
                        {

                            addressBookModel.personid = sqlDataReader.GetInt32(0);
                            addressBookModel.firstname = sqlDataReader.GetString(1);
                            addressBookModel.lastname = sqlDataReader.GetString(2);
                            addressBookModel.phone = sqlDataReader.GetString(4);
                            addressBookModel.emailid = sqlDataReader.GetString(5);
                            addressBookModel.bookid = sqlDataReader.GetInt32(7);
                            addressBookModel.city = sqlDataReader.GetString(10);
                            addressBookModel.zip = sqlDataReader.GetInt32(9);
                            addressBookModel.state = sqlDataReader.GetString(11);
                            addressBookModel.bookname = sqlDataReader.GetString(6);
                            addressBookModel.booktype = sqlDataReader.GetString(8);
                            addressBookModel.dateadded = sqlDataReader.GetDateTime(3);

                            Console.WriteLine("{0}, {1}, {2},{3}, {4}, {5}, {6}, {7}, {8}, {9}, {10} {11}", addressBookModel.personid, addressBookModel.firstname, addressBookModel.lastname, addressBookModel.dateadded,
                                addressBookModel.phone, addressBookModel.emailid, addressBookModel.bookname, addressBookModel.bookid, addressBookModel.booktype, addressBookModel.zip, addressBookModel.city, addressBookModel.state);
                        }
                        Console.WriteLine("New Contact Added Successfully");
                        sqlDataReader.Close();
                        return true;
                    }
                    connection.Close();
                    return false;
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public int RetriveContactByCityOrState(AddressBookModel addressBookModel)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("SpRetriveContactByCityOrState", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@state_name", addressBookModel.state);
                connection.Open();
                var Count = (int)command.ExecuteScalar();
                SqlDataReader sqlDataReader = command.ExecuteReader();
                if (sqlDataReader.HasRows)
                {
                    while (sqlDataReader.Read())
                    {
                        addressBookModel.personid = sqlDataReader.GetInt32(0);
                        Console.WriteLine("Number of Conctacts beloning to entered City Or State {0}", addressBookModel.personid);
                    }
                }
                return Count;
            }
        }
        public bool AddNewContactWithoutThread(List<AddressBookDetail> model1)
        {
            var result = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    foreach (AddressBookDetail model in model1)
                    {

                        SqlCommand command = new SqlCommand("SpAddNewRecord", connection);
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@first_name", model.firstname);
                        command.Parameters.AddWithValue("@last_name", model.lastname);
                        command.Parameters.AddWithValue("@email", model.email);
                        command.Parameters.AddWithValue("@phone_number", model.phone);
                        command.Parameters.AddWithValue("@zip", model.zip);
                        command.Parameters.AddWithValue("@date_added", model.dateadded);
                        command.Parameters.AddWithValue("@person_id", model.personid);
                        command.Parameters.AddWithValue("@book_id", model.bookid);
                        connection.Open();
                        result = command.ExecuteNonQuery();
                        Console.WriteLine("New Contact Added Successfully");
                        connection.Close();
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            if (result != 0)
                return true;
            return false;
        }
        public int AddNewContactWithThread(List<AddressBookDetail> model1)
        {
            var result = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    foreach (AddressBookDetail model in model1)
                    {
                        Task thread = new Task(() =>
                        {
                            SqlCommand command = new SqlCommand("SpAddNewRecord", connection);
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.AddWithValue("@first_name", model.firstname);
                            command.Parameters.AddWithValue("@last_name", model.lastname);
                            command.Parameters.AddWithValue("@email", model.email);
                            command.Parameters.AddWithValue("@phone_number", model.phone);
                            command.Parameters.AddWithValue("@zip", model.zip);
                            command.Parameters.AddWithValue("@date_added", model.dateadded);
                            command.Parameters.AddWithValue("@person_id", model.personid);
                            command.Parameters.AddWithValue("@book_id", model.bookid);
                            connection.Open();
                            result = command.ExecuteNonQuery();
                            Console.WriteLine("New Contact Added Successfully");
                            connection.Close();
                        });
                        thread.Start();
                    }
                    Console.WriteLine(model1.Count);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return model1.Count;
        }
    }
}
