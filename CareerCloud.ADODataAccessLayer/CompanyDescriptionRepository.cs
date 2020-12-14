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
    public class CompanyDescriptionRepository : ADOBaseRepository, IDataRepository<CompanyDescriptionPoco>
    {
        public void Add(params CompanyDescriptionPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand();

                command.Connection = conn;

                foreach (CompanyDescriptionPoco item in items)
                {
                    command.CommandText = @"INSERT INTO [dbo].[Company_Descriptions]
                                                   ([Id]
                                                   ,[Company]
                                                   ,[LanguageID]
                                                   ,[Company_Name]
                                                   ,[Company_Description])
                                             VALUES
                                                   (@Id
                                                   ,@Company
                                                   ,@LanguageID
                                                   ,@CompanyName
                                                   ,@CompanyDescription)";

                    command.Parameters.AddWithValue("@Id", item.Id);
                    command.Parameters.AddWithValue("@Company", item.Company);
                    command.Parameters.AddWithValue("@LanguageID", item.LanguageId);
                    command.Parameters.AddWithValue("@CompanyName", item.CompanyName);
                    command.Parameters.AddWithValue("@CompanyDescription", item.CompanyDescription);
                    
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

        public IList<CompanyDescriptionPoco> GetAll(params Expression<Func<CompanyDescriptionPoco, object>>[] navigationProperties)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;

            cmd.CommandText = @"SELECT [Id]
                                      ,[Company]
                                      ,[LanguageID]
                                      ,[Company_Name]
                                      ,[Company_Description]
                                      ,[Time_Stamp]
                                  FROM [dbo].[Company_Descriptions]";

            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            CompanyDescriptionPoco[] compDescPoco = new CompanyDescriptionPoco[1000];
            int counter = 0;

            while (reader.Read())
            {
                CompanyDescriptionPoco descPoco = new CompanyDescriptionPoco();
                descPoco.Id = (Guid)reader["Id"];
                descPoco.Company = (Guid)reader["Company"];
                descPoco.LanguageId = Convert.ToString(reader["LanguageID"]);
                descPoco.CompanyName = Convert.ToString(reader["Company_Name"]);
                descPoco.CompanyDescription = Convert.ToString(reader["Company_Description"]);                
                descPoco.TimeStamp = (byte[])reader["Time_Stamp"];

                compDescPoco[counter++] = descPoco;
            }

            conn.Close();

            return compDescPoco.Where(p => p != null).ToList();
        }

        public IList<CompanyDescriptionPoco> GetList(Expression<Func<CompanyDescriptionPoco, bool>> where, params Expression<Func<CompanyDescriptionPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyDescriptionPoco GetSingle(Expression<Func<CompanyDescriptionPoco, bool>> where, params Expression<Func<CompanyDescriptionPoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyDescriptionPoco> educationPoco = GetAll().AsQueryable();
            return educationPoco.Where(where).FirstOrDefault();
        }
        public void Remove(params CompanyDescriptionPoco[] items)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);

            SqlCommand command = new SqlCommand();
            command.Connection = conn;

            foreach (CompanyDescriptionPoco item in items)
            {
                command.CommandText = @"DELETE FROM [dbo].[Company_Descriptions]
                                            WHERE [Id] = @Id";

                command.Parameters.AddWithValue("@Id", item.Id);

                conn.Open();
                command.ExecuteNonQuery();
                conn.Close();
            }
        }
        public void Update(params CompanyDescriptionPoco[] items)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);

            SqlCommand command = new SqlCommand();
            command.Connection = conn;

            foreach (CompanyDescriptionPoco item in items)
            {
                command.CommandText = @"UPDATE [dbo].[Company_Descriptions]
                                           SET [Company] = @Company
                                              ,[LanguageID] = @LanguageID
                                              ,[Company_Name] = @CompanyName
                                              ,[Company_Description] = @CompanyDescription
                                         WHERE [Id] = @Id";

                command.Parameters.AddWithValue("@Id", item.Id);
                command.Parameters.AddWithValue("@Company", item.Company);
                command.Parameters.AddWithValue("@LanguageID", item.LanguageId);
                command.Parameters.AddWithValue("@CompanyName", item.CompanyName);
                command.Parameters.AddWithValue("@CompanyDescription", item.CompanyDescription);

                conn.Open();
                command.ExecuteNonQuery();
                conn.Close();
            }
        }
    }
}
