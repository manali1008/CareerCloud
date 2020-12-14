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
    public class ApplicantWorkHistoryRepository : ADOBaseRepository, IDataRepository<ApplicantWorkHistoryPoco>
    {
        public void Add(params ApplicantWorkHistoryPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand();

                command.Connection = conn;

                foreach (ApplicantWorkHistoryPoco item in items)
                {
                    command.CommandText = @"INSERT INTO [dbo].[Applicant_Work_History]
                                                   ([Id]
                                                   ,[Applicant]
                                                   ,[Company_Name]
                                                   ,[Country_Code]
                                                   ,[Location]
                                                   ,[Job_Title]
                                                   ,[Job_Description]
                                                   ,[Start_Month]
                                                   ,[Start_Year]
                                                   ,[End_Month]
                                                   ,[End_Year])
                                             VALUES
                                                   (@Id
                                                   ,@Applicant
                                                   ,@CompanyName
                                                   ,@CountryCode
                                                   ,@Location
                                                   ,@JobTitle
                                                   ,@JobDescription
                                                   ,@StartMonth
                                                   ,@StartYear
                                                   ,@EndMonth
                                                   ,@EndYear)";

                    command.Parameters.AddWithValue("@Id", item.Id);
                    command.Parameters.AddWithValue("@Applicant", item.Applicant);
                    command.Parameters.AddWithValue("@CompanyName", item.CompanyName);
                    command.Parameters.AddWithValue("@CountryCode", item.CountryCode);
                    command.Parameters.AddWithValue("@Location", item.Location);
                    command.Parameters.AddWithValue("@JobTitle", item.JobTitle);
                    command.Parameters.AddWithValue("@JobDescription", item.JobDescription);
                    command.Parameters.AddWithValue("@StartMonth", item.StartMonth);
                    command.Parameters.AddWithValue("@StartYear", item.StartYear);
                    command.Parameters.AddWithValue("@EndMonth", item.EndMonth);
                    command.Parameters.AddWithValue("@EndYear", item.EndYear);

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

        public IList<ApplicantWorkHistoryPoco> GetAll(params Expression<Func<ApplicantWorkHistoryPoco, object>>[] navigationProperties)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;

            cmd.CommandText = @"SELECT [Id]
                                      ,[Applicant]
                                      ,[Company_Name]
                                      ,[Country_Code]
                                      ,[Location]
                                      ,[Job_Title]
                                      ,[Job_Description]
                                      ,[Start_Month]
                                      ,[Start_Year]
                                      ,[End_Month]
                                      ,[End_Year]
                                      ,[Time_Stamp]
                                  FROM [dbo].[Applicant_Work_History]";

            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            ApplicantWorkHistoryPoco[] applWorkHistoryPoco = new ApplicantWorkHistoryPoco[1000];
            int counter = 0;

            while (reader.Read())
            {
                ApplicantWorkHistoryPoco workHistoryPoco = new ApplicantWorkHistoryPoco();
                workHistoryPoco.Id = (Guid)reader["Id"];
                workHistoryPoco.Applicant = (Guid)reader["Applicant"];
                workHistoryPoco.CompanyName = Convert.ToString(reader["Company_Name"]);
                workHistoryPoco.CountryCode = Convert.ToString(reader["Country_Code"]);
                workHistoryPoco.Location = Convert.ToString(reader["Location"]);
                workHistoryPoco.JobTitle = Convert.ToString(reader["Job_Title"]);
                workHistoryPoco.JobDescription = Convert.ToString(reader["Job_Description"]);
                workHistoryPoco.StartMonth = (short)reader["Start_Month"];
                workHistoryPoco.StartYear = Convert.ToInt32(reader["Start_Year"]);
                workHistoryPoco.EndMonth = (short)reader["End_Month"];
                workHistoryPoco.EndYear = Convert.ToInt32(reader["End_Year"]);
                workHistoryPoco.TimeStamp = (byte[])reader["Time_Stamp"];

                applWorkHistoryPoco[counter++] = workHistoryPoco;
            }

            conn.Close();

            return applWorkHistoryPoco.Where(p => p != null).ToList();
        }

        public IList<ApplicantWorkHistoryPoco> GetList(Expression<Func<ApplicantWorkHistoryPoco, bool>> where, params Expression<Func<ApplicantWorkHistoryPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantWorkHistoryPoco GetSingle(Expression<Func<ApplicantWorkHistoryPoco, bool>> where, params Expression<Func<ApplicantWorkHistoryPoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantWorkHistoryPoco> educationPoco = GetAll().AsQueryable();
            return educationPoco.Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantWorkHistoryPoco[] items)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);

            SqlCommand command = new SqlCommand();
            command.Connection = conn;

            foreach (ApplicantWorkHistoryPoco item in items)
            {
                command.CommandText = @"DELETE FROM [dbo].[Applicant_Work_History]
                                            WHERE [Id] = @Id";

                command.Parameters.AddWithValue("@Id", item.Id);
                conn.Open();
                command.ExecuteNonQuery();
                conn.Close();
            }
        }

        public void Update(params ApplicantWorkHistoryPoco[] items)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);

            SqlCommand command = new SqlCommand();
            command.Connection = conn;

            foreach (ApplicantWorkHistoryPoco item in items)
            {
                command.CommandText = @"UPDATE [dbo].[Applicant_Work_History]
                                           SET [Applicant] = @Applicant
                                              ,[Company_Name] = @CompanyName
                                              ,[Country_Code] = @CountryCode
                                              ,[Location] = @Location
                                              ,[Job_Title] = @JobTitle
                                              ,[Job_Description] = @JobDescription
                                              ,[Start_Month] = @StartMonth
                                              ,[Start_Year] = @StartYear
                                              ,[End_Month] = @EndMonth
                                              ,[End_Year] = @EndYear
                                         WHERE Id = @Id";

                command.Parameters.AddWithValue("@Id", item.Id);
                command.Parameters.AddWithValue("@Applicant", item.Applicant);
                command.Parameters.AddWithValue("@CompanyName", item.CompanyName);
                command.Parameters.AddWithValue("@CountryCode", item.CountryCode);
                command.Parameters.AddWithValue("@Location", item.Location);
                command.Parameters.AddWithValue("@JobTitle", item.JobTitle);
                command.Parameters.AddWithValue("@JobDescription", item.JobDescription);
                command.Parameters.AddWithValue("@StartMonth", item.StartMonth);
                command.Parameters.AddWithValue("@StartYear", item.StartYear);
                command.Parameters.AddWithValue("@EndMonth", item.EndMonth);
                command.Parameters.AddWithValue("@EndYear", item.EndYear);

                conn.Open();
                command.ExecuteNonQuery();
                conn.Close();
            }
        }
    }
}
