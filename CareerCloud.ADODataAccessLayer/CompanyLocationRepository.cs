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
    public class CompanyLocationRepository : ADOBaseRepository, IDataRepository<CompanyLocationPoco>
    {
        public void Add(params CompanyLocationPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand();

                command.Connection = conn;

                foreach (CompanyLocationPoco item in items)
                {
                    command.CommandText = @"INSERT INTO [dbo].[Company_Locations]
                                                   ([Id]
                                                   ,[Company]
                                                   ,[Country_Code]
                                                   ,[State_Province_Code]
                                                   ,[Street_Address]
                                                   ,[City_Town]
                                                   ,[Zip_Postal_Code])
                                             VALUES
                                                   (@Id
                                                   ,@Company
                                                   ,@CountryCode
                                                   ,@Province 
                                                   ,@Street
                                                   ,@City
                                                   ,@PostalCode)";

                    command.Parameters.AddWithValue("@Id", item.Id);
                    command.Parameters.AddWithValue("@Company", item.Company);
                    command.Parameters.AddWithValue("@CountryCode", item.CountryCode);
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

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<CompanyLocationPoco> GetAll(params Expression<Func<CompanyLocationPoco, object>>[] navigationProperties)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;

            cmd.CommandText = @"SELECT [Id]
                                      ,[Company]
                                      ,[Country_Code]
                                      ,[State_Province_Code]
                                      ,[Street_Address]
                                      ,[City_Town]
                                      ,[Zip_Postal_Code]
                                      ,[Time_Stamp]
                                  FROM [dbo].[Company_Locations]";

            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            CompanyLocationPoco[] compLocationPoco = new CompanyLocationPoco[1000];
            int counter = 0;

            while (reader.Read())
            {
                CompanyLocationPoco locationPoco = new CompanyLocationPoco();
                locationPoco.Id = (Guid)reader["Id"];
                locationPoco.Company = (Guid)reader["Company"];
                locationPoco.CountryCode = Convert.ToString(reader["Country_Code"]);
                locationPoco.Province = Convert.ToString(reader["State_Province_Code"]);
                locationPoco.Street = Convert.ToString(reader["Street_Address"]);
                locationPoco.City = Convert.ToString(reader["City_Town"]);
                locationPoco.PostalCode = Convert.ToString(reader["Zip_Postal_Code"]);
                locationPoco.TimeStamp = (byte[])reader["Time_Stamp"];

                compLocationPoco[counter++] = locationPoco;
            }

            conn.Close();

            return compLocationPoco.Where(p => p != null).ToList();
        }

        public IList<CompanyLocationPoco> GetList(Expression<Func<CompanyLocationPoco, bool>> where, params Expression<Func<CompanyLocationPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyLocationPoco GetSingle(Expression<Func<CompanyLocationPoco, bool>> where, params Expression<Func<CompanyLocationPoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyLocationPoco> educationPoco = GetAll().AsQueryable();
            return educationPoco.Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyLocationPoco[] items)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);

            SqlCommand command = new SqlCommand();
            command.Connection = conn;

            foreach (CompanyLocationPoco item in items)
            {
                command.CommandText = @"DELETE FROM [dbo].[Company_Locations]
                                            WHERE [Id] = @Id";

                command.Parameters.AddWithValue("@Id", item.Id);
                conn.Open();
                command.ExecuteNonQuery();
                conn.Close();
            }
        }

        public void Update(params CompanyLocationPoco[] items)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);

            SqlCommand command = new SqlCommand();
            command.Connection = conn;

            foreach (CompanyLocationPoco item in items)
            {
                command.CommandText = @"UPDATE [dbo].[Company_Locations]
                                           SET [Company] = @Company
                                              ,[Country_Code] = @CountryCode
                                              ,[State_Province_Code] = @Province
                                              ,[Street_Address] = @Street
                                              ,[City_Town] = @City
                                              ,[Zip_Postal_Code] = @PostalCode
                                          WHERE [Id] = @Id";

                command.Parameters.AddWithValue("@Id", item.Id);
                command.Parameters.AddWithValue("@Company", item.Company);
                command.Parameters.AddWithValue("@CountryCode", item.CountryCode);
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
