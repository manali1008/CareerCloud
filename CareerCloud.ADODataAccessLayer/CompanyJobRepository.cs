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
    public class CompanyJobRepository : ADOBaseRepository, IDataRepository<CompanyJobPoco>
    {
        public void Add(params CompanyJobPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand();

                command.Connection = conn;

                foreach (CompanyJobPoco item in items)
                {
                    command.CommandText = @"INSERT INTO [dbo].[Company_Jobs]
                                                   ([Id]
                                                   ,[Company]
                                                   ,[Profile_Created]
                                                   ,[Is_Inactive]
                                                   ,[Is_Company_Hidden])
                                             VALUES
                                                   (@Id
                                                   ,@Company
                                                   ,@ProfileCreated
                                                   ,@IsInactive
                                                   ,@IsCompanyHidden)";

                    command.Parameters.AddWithValue("@Id", item.Id);
                    command.Parameters.AddWithValue("@Company", item.Company);
                    command.Parameters.AddWithValue("@ProfileCreated", item.ProfileCreated);
                    command.Parameters.AddWithValue("@IsInactive", item.IsInactive);
                    command.Parameters.AddWithValue("@IsCompanyHidden", item.IsCompanyHidden);
                    
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

        public IList<CompanyJobPoco> GetAll(params Expression<Func<CompanyJobPoco, object>>[] navigationProperties)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;

            cmd.CommandText = @"SELECT [Id]
                                      ,[Company]
                                      ,[Profile_Created]
                                      ,[Is_Inactive]
                                      ,[Is_Company_Hidden]
                                      ,[Time_Stamp]
                                  FROM [dbo].[Company_Jobs]";

            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            CompanyJobPoco[] compJobPoco = new CompanyJobPoco[1010];
            int counter = 0;

            while (reader.Read())
            {
                CompanyJobPoco jobPoco = new CompanyJobPoco();
                jobPoco.Id = (Guid)reader["Id"];
                jobPoco.Company = (Guid)reader["Company"];
                jobPoco.ProfileCreated = Convert.ToDateTime(reader["Profile_Created"]);
                jobPoco.IsInactive = Convert.ToBoolean(reader["Is_Inactive"]);
                jobPoco.IsCompanyHidden = Convert.ToBoolean(reader["Is_Company_Hidden"]);
                jobPoco.TimeStamp = (byte[])reader["Time_Stamp"];

                compJobPoco[counter++] = jobPoco;
            }

            conn.Close();

            return compJobPoco.Where(p => p != null).ToList();
        }

        public IList<CompanyJobPoco> GetList(Expression<Func<CompanyJobPoco, bool>> where, params Expression<Func<CompanyJobPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyJobPoco GetSingle(Expression<Func<CompanyJobPoco, bool>> where, params Expression<Func<CompanyJobPoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyJobPoco> educationPoco = GetAll().AsQueryable();
            return educationPoco.Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyJobPoco[] items)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);

            SqlCommand command = new SqlCommand();
            command.Connection = conn;

            foreach (CompanyJobPoco item in items)
            {
                command.CommandText = @"DELETE FROM [dbo].[Company_Jobs]
                                            WHERE [Id] = @Id";

                command.Parameters.AddWithValue("@Id", item.Id);
                conn.Open();
                command.ExecuteNonQuery();
                conn.Close();
            }
        }

        public void Update(params CompanyJobPoco[] items)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);

            SqlCommand command = new SqlCommand();
            command.Connection = conn;

            foreach (CompanyJobPoco item in items)
            {
                command.CommandText = @"UPDATE [dbo].[Company_Jobs]
                                           SET [Company] = @Company
                                              ,[Profile_Created] = @ProfileCreated
                                              ,[Is_Inactive] = @IsInactive
                                              ,[Is_Company_Hidden] = @IsCompanyHidden
                                           WHERE [Id] = @Id";

                command.Parameters.AddWithValue("@Id", item.Id);
                command.Parameters.AddWithValue("@Company", item.Company);
                command.Parameters.AddWithValue("@ProfileCreated", item.ProfileCreated);
                command.Parameters.AddWithValue("@IsInactive", item.IsInactive);
                command.Parameters.AddWithValue("@IsCompanyHidden", item.IsCompanyHidden);

                conn.Open();
                command.ExecuteNonQuery();
                conn.Close();
            }
        }
    }
}
