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
    public class CompanyJobEducationRepository : ADOBaseRepository, IDataRepository<CompanyJobEducationPoco>
    {
        public void Add(params CompanyJobEducationPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand();

                command.Connection = conn;

                foreach (CompanyJobEducationPoco item in items)
                {
                    command.CommandText = @"INSERT INTO [dbo].[Company_Job_Educations]
                                                   ([Id]
                                                   ,[Job]
                                                   ,[Major]
                                                   ,[Importance])
                                             VALUES
                                                   (@Id
                                                   ,@Job
                                                   ,@Major
                                                   ,@Importance)";

                    command.Parameters.AddWithValue("@Id", item.Id);
                    command.Parameters.AddWithValue("@Job", item.Job);
                    command.Parameters.AddWithValue("@Major", item.Major);
                    command.Parameters.AddWithValue("@Importance", item.Importance);
                    
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

        public IList<CompanyJobEducationPoco> GetAll(params Expression<Func<CompanyJobEducationPoco, object>>[] navigationProperties)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;

            cmd.CommandText = @"SELECT [Id]
                                      ,[Job]
                                      ,[Major]
                                      ,[Importance]
                                      ,[Time_Stamp]
                                  FROM [dbo].[Company_Job_Educations]";

            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            CompanyJobEducationPoco[] compJobEduPoco = new CompanyJobEducationPoco[1010];
            int counter = 0;

            while (reader.Read())
            {
                CompanyJobEducationPoco jobEduPoco = new CompanyJobEducationPoco();
                jobEduPoco.Id = (Guid)reader["Id"];
                jobEduPoco.Job = (Guid)reader["Job"];
                jobEduPoco.Major = Convert.ToString(reader["Major"]);
                jobEduPoco.Importance = (short)reader["Importance"];
                jobEduPoco.TimeStamp = (byte[])reader["Time_Stamp"];

                compJobEduPoco[counter++] = jobEduPoco;
            }

            conn.Close();

            return compJobEduPoco.Where(p => p != null).ToList();
        }

        public IList<CompanyJobEducationPoco> GetList(Expression<Func<CompanyJobEducationPoco, bool>> where, params Expression<Func<CompanyJobEducationPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyJobEducationPoco GetSingle(Expression<Func<CompanyJobEducationPoco, bool>> where, params Expression<Func<CompanyJobEducationPoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyJobEducationPoco> educationPoco = GetAll().AsQueryable();
            return educationPoco.Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyJobEducationPoco[] items)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);

            SqlCommand command = new SqlCommand();
            command.Connection = conn;

            foreach (CompanyJobEducationPoco item in items)
            {
                command.CommandText = @"DELETE FROM [dbo].[Company_Job_Educations]
                                            WHERE [Id] = @Id";

                command.Parameters.AddWithValue("@Id", item.Id);
                conn.Open();
                command.ExecuteNonQuery();
                conn.Close();
            }
        }

        public void Update(params CompanyJobEducationPoco[] items)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);

            SqlCommand command = new SqlCommand();
            command.Connection = conn;

            foreach (CompanyJobEducationPoco item in items)
            {
                command.CommandText = @"UPDATE [dbo].[Company_Job_Educations]
                                           SET [Job] = @Job
                                              ,[Major] = @Major
                                              ,[Importance] = @Importance
                                         WHERE Id = @Id";

                command.Parameters.AddWithValue("@Id", item.Id);
                command.Parameters.AddWithValue("@Job", item.Job);
                command.Parameters.AddWithValue("@Major", item.Major);
                command.Parameters.AddWithValue("@Importance", item.Importance);

                conn.Open();
                command.ExecuteNonQuery();
                conn.Close();
            }
        }
    }
}
