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
    public class CompanyJobDescriptionRepository : ADOBaseRepository, IDataRepository<CompanyJobDescriptionPoco>
    {
        public void Add(params CompanyJobDescriptionPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand();

                command.Connection = conn;

                foreach (CompanyJobDescriptionPoco item in items)
                {
                    command.CommandText = @"INSERT INTO [dbo].[Company_Jobs_Descriptions]
                                                   ([Id]
                                                   ,[Job]
                                                   ,[Job_Name]
                                                   ,[Job_Descriptions])
                                             VALUES
                                                   (@Id
                                                   ,@Job
                                                   ,@JobName
                                                   ,@JobDescriptions)";

                    command.Parameters.AddWithValue("@Id", item.Id);
                    command.Parameters.AddWithValue("@Job", item.Job);
                    command.Parameters.AddWithValue("@JobName", item.JobName);
                    command.Parameters.AddWithValue("@JobDescriptions", item.JobDescriptions);
                    
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

        public IList<CompanyJobDescriptionPoco> GetAll(params Expression<Func<CompanyJobDescriptionPoco, object>>[] navigationProperties)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;

            cmd.CommandText = @"SELECT [Id]
                                      ,[Job]
                                      ,[Job_Name]
                                      ,[Job_Descriptions]
                                      ,[Time_Stamp]
                                  FROM [dbo].[Company_Jobs_Descriptions]";

            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            CompanyJobDescriptionPoco[] compJobDescPoco = new CompanyJobDescriptionPoco[1010];
            int counter = 0;

            while (reader.Read())
            {
                CompanyJobDescriptionPoco jobDescPoco  = new CompanyJobDescriptionPoco();
                jobDescPoco.Id = (Guid)reader["Id"];
                jobDescPoco.Job = (Guid)reader["Job"];
                jobDescPoco.JobName = Convert.ToString(reader["Job_Name"]);
                jobDescPoco.JobDescriptions = Convert.ToString(reader["Job_Descriptions"]);
                jobDescPoco.TimeStamp = (byte[])reader["Time_Stamp"];

                compJobDescPoco[counter++] = jobDescPoco;
            }

            conn.Close();

            return compJobDescPoco.Where(p => p != null).ToList();
        }

        public IList<CompanyJobDescriptionPoco> GetList(Expression<Func<CompanyJobDescriptionPoco, bool>> where, params Expression<Func<CompanyJobDescriptionPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyJobDescriptionPoco GetSingle(Expression<Func<CompanyJobDescriptionPoco, bool>> where, params Expression<Func<CompanyJobDescriptionPoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyJobDescriptionPoco> educationPoco = GetAll().AsQueryable();
            return educationPoco.Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyJobDescriptionPoco[] items)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);

            SqlCommand command = new SqlCommand();
            command.Connection = conn;

            foreach (CompanyJobDescriptionPoco item in items)
            {
                command.CommandText = @"DELETE FROM [dbo].[Company_Jobs_Descriptions]
                                            WHERE [Id] = @Id";

                command.Parameters.AddWithValue("@Id", item.Id);

                conn.Open();
                command.ExecuteNonQuery();
                conn.Close();
            }
        }

        public void Update(params CompanyJobDescriptionPoco[] items)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);

            SqlCommand command = new SqlCommand();
            command.Connection = conn;

            foreach (CompanyJobDescriptionPoco item in items)
            {
                command.CommandText = @"UPDATE [dbo].[Company_Jobs_Descriptions]
                                         SET  [Job] = @Job
                                              ,[Job_Name] = @JobName
                                              ,[Job_Descriptions] = @JobDescriptions
                                         WHERE [Id] = @Id";

                command.Parameters.AddWithValue("@Id", item.Id);
                command.Parameters.AddWithValue("@Job", item.Job);
                command.Parameters.AddWithValue("@JobName", item.JobName);
                command.Parameters.AddWithValue("@JobDescriptions", item.JobDescriptions);

                conn.Open();
                command.ExecuteNonQuery();
                conn.Close();
            }
        }
    }
}
