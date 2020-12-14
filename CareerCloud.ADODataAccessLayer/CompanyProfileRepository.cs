using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace CareerCloud.ADODataAccessLayer
{
    public class CompanyProfileRepository : ADOBaseRepository, IDataRepository<CompanyProfilePoco>
    {
        public void Add(params CompanyProfilePoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand();

                command.Connection = conn;

                foreach (CompanyProfilePoco item in items)
                {
                    command.CommandText = @"INSERT INTO [dbo].[Company_Profiles]
                                                   ([Id]
                                                   ,[Registration_Date]
                                                   ,[Company_Website]
                                                   ,[Contact_Phone]
                                                   ,[Contact_Name]
                                                   ,[Company_Logo])
                                             VALUES
                                                   (@Id
                                                   ,@RegistrationDate
                                                   ,@CompanyWebsite
                                                   ,@ContactPhone
                                                   ,@ContactName
                                                   ,@CompanyLogo)";

                    command.Parameters.AddWithValue("@Id", item.Id);
                    command.Parameters.AddWithValue("@RegistrationDate", item.RegistrationDate);
                    command.Parameters.AddWithValue("@CompanyWebsite", item.CompanyWebsite);
                    command.Parameters.AddWithValue("@ContactPhone", item.ContactPhone);
                    command.Parameters.AddWithValue("@ContactName", item.ContactName);
                    command.Parameters.AddWithValue("@CompanyLogo", item.CompanyLogo);
                    
                    conn.Open();
                    command.ExecuteNonQuery();
                    conn.Close();

                }
            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<CompanyProfilePoco> GetAll(params Expression<Func<CompanyProfilePoco, object>>[] navigationProperties)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;

            cmd.CommandText = @"SELECT [Id]
                                    ,[Registration_Date]
                                    ,[Company_Website]
                                    ,[Contact_Phone]
                                    ,[Contact_Name]
                                    ,[Company_Logo]
                                    ,[Time_Stamp]
                                FROM [dbo].[Company_Profiles]";

            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            CompanyProfilePoco[] compProfilePoco = new CompanyProfilePoco[1000];
            int counter = 0;

            while (reader.Read())
            {
                CompanyProfilePoco profilePoco = new CompanyProfilePoco();
                profilePoco.Id = (Guid)reader["Id"];
                profilePoco.RegistrationDate = Convert.ToDateTime(reader["Registration_Date"]);
                profilePoco.CompanyWebsite = Convert.ToString(reader["Company_Website"]);
                profilePoco.ContactPhone = Convert.ToString(reader["Contact_Phone"]);
                profilePoco.ContactName = Convert.ToString(reader["Contact_Name"]);
                profilePoco.CompanyLogo = (reader["Company_Logo"] as byte[]) ?? null;
                profilePoco.TimeStamp = (byte[])reader["Time_Stamp"];

                compProfilePoco[counter++] = profilePoco;
            }

            conn.Close();

            return compProfilePoco.Where(p => p != null).ToList();
        }

        public IList<CompanyProfilePoco> GetList(Expression<Func<CompanyProfilePoco, bool>> where, params Expression<Func<CompanyProfilePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyProfilePoco GetSingle(Expression<Func<CompanyProfilePoco, bool>> where, params Expression<Func<CompanyProfilePoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyProfilePoco> educationPoco = GetAll().AsQueryable();
            return educationPoco.Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyProfilePoco[] items)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);

            SqlCommand command = new SqlCommand();
            command.Connection = conn;

            foreach (CompanyProfilePoco item in items)
            {
                command.CommandText = @"DELETE FROM [dbo].[Company_Profiles]
                                            WHERE [Id] = @Id";

                command.Parameters.AddWithValue("@Id", item.Id);
                conn.Open();
                command.ExecuteNonQuery();
                conn.Close();
            }
        }

        public void Update(params CompanyProfilePoco[] items)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);

            SqlCommand command = new SqlCommand();
            command.Connection = conn;

            foreach (CompanyProfilePoco item in items)
            {
                command.CommandText = @"UPDATE [dbo].[Company_Profiles]
                                           SET [Registration_Date] = @RegistrationDate
                                              ,[Company_Website] = @CompanyWebsite
                                              ,[Contact_Phone] = @ContactPhone
                                              ,[Contact_Name] = @ContactName
                                              ,[Company_Logo] = @CompanyLogo
                                         WHERE [Id] = @Id";

                command.Parameters.AddWithValue("@Id", item.Id);
                command.Parameters.AddWithValue("@RegistrationDate", item.RegistrationDate);
                command.Parameters.AddWithValue("@CompanyWebsite", item.CompanyWebsite);
                command.Parameters.AddWithValue("@ContactPhone", item.ContactPhone);
                command.Parameters.AddWithValue("@ContactName", item.ContactName);
                command.Parameters.AddWithValue("@CompanyLogo", item.CompanyLogo);

                conn.Open();
                command.ExecuteNonQuery();
                conn.Close();
            }
        }
    }
}
