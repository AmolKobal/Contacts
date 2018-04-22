using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ContactDemo.Models
{
    public class ContactRepository
    {
        public IEnumerable<Contact> GetAllContacts()
        {
            IList<Contact> contacts = new List<Contact>();

            string connectionString = Common.GetConnectionString();
            using (SqlDataAdapter sqlAdapter = new SqlDataAdapter("", connectionString))
            {
                sqlAdapter.SelectCommand.CommandText = "usp_GetAllContact";
                sqlAdapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;

                DataTable dtContacts = new DataTable();
                sqlAdapter.Fill(dtContacts);

                foreach (DataRow row in dtContacts.Rows)
                {
                    Contact contact = new Contact
                    {
                        ID = Convert.ToInt32(row["ID"]),
                        FirstName = row["FirstName"].ToString(),
                        LastName = row["LastName"].ToString(),
                        EMail = row["EMail"].ToString(),
                        PhoneNumber = row["PhoneNumber"].ToString(),
                        Status = Convert.ToBoolean(row["Status"]),
                        DateCreated = Convert.ToDateTime(row["DateCreated"]),
                    };

                    contacts.Add(contact);
                }
            }

            return contacts;
        }

        internal void Create(Contact contact)
        {
            using (SqlConnection sqlcon = Common.GetConnection())
            {
                SqlCommand sqlCmd = sqlcon.CreateCommand();
                sqlCmd.CommandText = "usp_AddContact";
                sqlCmd.CommandType = System.Data.CommandType.StoredProcedure;

                sqlCmd.Parameters.Add(new SqlParameter("@FirstName", contact.FirstName));
                sqlCmd.Parameters.Add(new SqlParameter("@LastName", contact.LastName));
                sqlCmd.Parameters.Add(new SqlParameter("@EMail", contact.EMail));
                sqlCmd.Parameters.Add(new SqlParameter("@PhoneNumber", contact.PhoneNumber));

                sqlCmd.ExecuteNonQuery();
                sqlCmd.Dispose();
            }
        }

        internal void Edit(Contact contact)
        {
            using (SqlConnection sqlcon = Common.GetConnection())
            {
                SqlCommand sqlCmd = sqlcon.CreateCommand();
                sqlCmd.CommandText = "usp_EditContact";
                sqlCmd.CommandType = System.Data.CommandType.StoredProcedure;

                sqlCmd.Parameters.Add(new SqlParameter("@ID", contact.ID));
                sqlCmd.Parameters.Add(new SqlParameter("@FirstName", contact.FirstName));
                sqlCmd.Parameters.Add(new SqlParameter("@LastName", contact.LastName));
                sqlCmd.Parameters.Add(new SqlParameter("@EMail", contact.EMail));
                sqlCmd.Parameters.Add(new SqlParameter("@PhoneNumber", contact.PhoneNumber));
                sqlCmd.Parameters.Add(new SqlParameter("@Status", contact.Status));

                sqlCmd.ExecuteNonQuery();
                sqlCmd.Dispose();
            }
        }

        internal void Delete(Contact contact)
        {
            using (SqlConnection sqlcon = Common.GetConnection())
            {
                SqlCommand sqlCmd = sqlcon.CreateCommand();
                sqlCmd.CommandText = "usp_DeleteContact";
                sqlCmd.CommandType = System.Data.CommandType.StoredProcedure;

                sqlCmd.Parameters.Add(new SqlParameter("@ID", contact.ID));

                sqlCmd.ExecuteNonQuery();
                sqlCmd.Dispose();
            }

        }

    }
}