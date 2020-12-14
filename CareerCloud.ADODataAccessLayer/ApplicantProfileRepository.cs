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
    public class ApplicantProfileRepository : ADOBaseRepository, IDataRepository<ApplicantProfilePoco>
    {
        public void Add(params ApplicantProfilePoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand();

                command.Connection = conn;

                foreach (ApplicantProfilePoco item in items)
                {
                    command.CommandText = @"INSERT INTO [dbo].[Applicant_Profiles]
                                                   ([Id]
                                                   ,[Login]
                                                   ,[Current_Salary]
                                                   ,[Current_Rate]
                                                   ,[Currency]
                                                   ,[Country_Code]
                                                   ,[State_Province_Code]
                                                   ,[Street_Address]
                                                   ,[City_Town]
                                                   ,[Zip_Postal_Code])
                                             VALUES
                                                   (@Id
                                                   ,@Login
                                                   ,@Current_Salary
                                                   ,@Current_Rate
                                                   ,@Currency
                                                   ,@Country_Code
                                                   ,@State_Province_Code
                                                   ,@Street_Address
                                                   ,@City_Town
                                                   ,@Zip_Postal_Code)";

                    command.Parameters.AddWithValue("@Id", item.Id);
                    command.Parameters.AddWithValue("@Login", item.Login);
                    command.Parameters.AddWithValue("@Current_Salary", item.CurrentSalary);
                    command.Parameters.AddWithValue("@Current_Rate", item.CurrentRate);
                    command.Parameters.AddWithValue("@Currency", item.Currency);
                    command.Parameters.AddWithValue("@Country_Code", item.Country);
                    command.Parameters.AddWithValue("@State_Province_Code", item.Province);
                    command.Parameters.AddWithValue("@Street_Address", item.Street);
                    command.Parameters.AddWithValue("@City_Town", item.City);
                    command.Parameters.AddWithValue("@Zip_Postal_Code", item.PostalCode);

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

        public IList<ApplicantProfilePoco> GetAll(params Expression<Func<ApplicantProfilePoco, object>>[] navigationProperties)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;

            cmd.CommandText = @"SELECT [Id]
                                      ,[Login]
                                      ,[Current_Salary]
                                      ,[Current_Rate]
                                      ,[Currency]
                                      ,[Country_Code]
                                      ,[State_Province_Code]
                                      ,[Street_Address]
                                      ,[City_Town]
                                      ,[Zip_Postal_Code]
                                      ,[Time_Stamp]
                                  FROM [dbo].[Applicant_Profiles]";

            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            ApplicantProfilePoco[] profilePoco = new ApplicantProfilePoco[1000];
            int counter = 0;

            while (reader.Read())
            {
                ApplicantProfilePoco applProfilePoco = new ApplicantProfilePoco();
                applProfilePoco.Id = (Guid)reader["Id"];
                applProfilePoco.Login = (Guid)reader["Login"];
                applProfilePoco.CurrentSalary = (decimal?)reader["Current_Salary"];
                applProfilePoco.CurrentRate = (decimal?)reader["Current_Rate"];
                applProfilePoco.Currency = Convert.ToString(reader["Currency"]);
                applProfilePoco.Country = Convert.ToString(reader["Country_Code"]);
                applProfilePoco.Province = Convert.ToString(reader["State_Province_Code"]);
                applProfilePoco.City = Convert.ToString(reader["City_Town"]);
                applProfilePoco.Street = Convert.ToString(reader["Street_Address"]);
                applProfilePoco.PostalCode = Convert.ToString(reader["Zip_Postal_Code"]);
                applProfilePoco.TimeStamp = (byte[])reader["Time_Stamp"];

                profilePoco[counter++] = applProfilePoco;
            }

            conn.Close();

            return profilePoco.Where(p => p != null).ToList();
        }

        public IList<ApplicantProfilePoco> GetList(Expression<Func<ApplicantProfilePoco, bool>> where, params Expression<Func<ApplicantProfilePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantProfilePoco GetSingle(Expression<Func<ApplicantProfilePoco, bool>> where, params Expression<Func<ApplicantProfilePoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantProfilePoco> educationPoco = GetAll().AsQueryable();
            return educationPoco.Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantProfilePoco[] items)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);

            SqlCommand command = new SqlCommand();
            command.Connection = conn;

            foreach (ApplicantProfilePoco item in items)
            {
                command.CommandText = @"DELETE FROM [dbo].[Applicant_Profiles]
                                            WHERE [Id] = @Id";

                command.Parameters.AddWithValue("@Id", item.Id);
                conn.Open();
                command.ExecuteNonQuery();
                conn.Close();
            }
        }

        public void Update(params ApplicantProfilePoco[] items)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);

            SqlCommand command = new SqlCommand();
            command.Connection = conn;

            foreach (ApplicantProfilePoco item in items)
            {
                command.CommandText = @"UPDATE [dbo].[Applicant_Profiles]
                                           SET 
                                               [Login] = @Login
                                              ,[Current_Salary] = @CurrentSalary
                                              ,[Current_Rate] = @CurrentRate
                                              ,[Currency] = @Currency
                                              ,[Country_Code] = @CountryCode
                                              ,[State_Province_Code] = @Province
                                              ,[Street_Address] = @Street
                                              ,[City_Town] = @City
                                              ,[Zip_Postal_Code] = @PostalCode
                                           WHERE [Id] = @Id";

                command.Parameters.AddWithValue("@Id", item.Id);
                command.Parameters.AddWithValue("@Login", item.Login);
                command.Parameters.AddWithValue("@CurrentSalary", item.CurrentSalary);
                command.Parameters.AddWithValue("@CurrentRate", item.CurrentRate);
                command.Parameters.AddWithValue("@Currency", item.Currency);
                command.Parameters.AddWithValue("@CountryCode", item.Country);
                command.Parameters.AddWithValue("@Province", item.Province);
                command.Parameters.AddWithValue("@Street", item.Street);
                command.Parameters.AddWithValue("@City", item.City);
                command.Parameters.AddWithValue("@PostalCode", item.PostalCode);

                conn.Open();
                command.ExecuteNonQuery();
                conn.Close();
            }
        }
    }
}
