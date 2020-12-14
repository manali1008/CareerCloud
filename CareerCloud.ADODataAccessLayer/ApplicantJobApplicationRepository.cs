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
    public class ApplicantJobApplicationRepository : ADOBaseRepository, IDataRepository<ApplicantJobApplicationPoco>
    {
        public void Add(params ApplicantJobApplicationPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand();

                command.Connection = conn;

                foreach (ApplicantJobApplicationPoco item in items)
                {
                    command.CommandText = @"INSERT INTO [dbo].[Applicant_Job_Applications]
                                                       ([Id]
                                                       ,[Applicant]
                                                       ,[Job]
                                                       ,[Application_Date])
                                                 VALUES
                                                       (@Id
                                                       ,@Applicant
                                                       ,@Job
                                                       ,@Application_Date)";

                    command.Parameters.AddWithValue("@Id", item.Id);
                    command.Parameters.AddWithValue("@Applicant", item.Applicant);
                    command.Parameters.AddWithValue("@Job", item.Job);
                    command.Parameters.AddWithValue("@Application_Date", item.ApplicationDate);

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

        public IList<ApplicantJobApplicationPoco> GetAll(params Expression<Func<ApplicantJobApplicationPoco, object>>[] navigationProperties)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;

            cmd.CommandText = @"SELECT [Id]
                                  ,[Applicant]
                                  ,[Job]
                                  ,[Application_Date]
                                  ,[Time_Stamp]
                              FROM [dbo].[Applicant_Job_Applications]";

            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            ApplicantJobApplicationPoco[] applJobPoco = new ApplicantJobApplicationPoco[1000];
            int counter = 0;

            while (reader.Read())
            {
                ApplicantJobApplicationPoco jobPoco = new ApplicantJobApplicationPoco();
                jobPoco.Id = (Guid)reader["Id"];
                jobPoco.Applicant = (Guid)reader["Applicant"];
                jobPoco.Job = (Guid)reader["Job"];
                jobPoco.ApplicationDate = Convert.ToDateTime(reader["Application_Date"]);
                jobPoco.TimeStamp = (byte[])reader["Time_Stamp"];

                applJobPoco[counter++] = jobPoco;
            }

            conn.Close();

            return applJobPoco.Where(p => p != null).ToList();
        }

        public IList<ApplicantJobApplicationPoco> GetList(Expression<Func<ApplicantJobApplicationPoco, bool>> where, params Expression<Func<ApplicantJobApplicationPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantJobApplicationPoco GetSingle(Expression<Func<ApplicantJobApplicationPoco, bool>> where, params Expression<Func<ApplicantJobApplicationPoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantJobApplicationPoco> applJobPoco = GetAll().AsQueryable();
            return applJobPoco.Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantJobApplicationPoco[] items)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);


            SqlCommand command = new SqlCommand();
            command.Connection = conn;

            foreach (ApplicantJobApplicationPoco item in items)
            {
                command.CommandText = @"DELETE FROM [dbo].[Applicant_Job_Applications]
                                            WHERE [Id] = @Id";

                command.Parameters.AddWithValue("@Id", item.Id);

                conn.Open();
                command.ExecuteNonQuery();
                conn.Close();
            }
        }

        public void Update(params ApplicantJobApplicationPoco[] items)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);

            SqlCommand command = new SqlCommand();
            command.Connection = conn;

            foreach (ApplicantJobApplicationPoco item in items)
            {
                command.CommandText = @"UPDATE [dbo].[Applicant_Job_Applications]
                                           SET 
                                               [Applicant] = @Applicant
                                              ,[Job] = @Job
                                              ,[Application_Date] = @Application_Date
                                         WHERE Id = @Id";

                command.Parameters.AddWithValue("@Id", item.Id);
                command.Parameters.AddWithValue("@Applicant", item.Applicant);
                command.Parameters.AddWithValue("@Job", item.Job);
                command.Parameters.AddWithValue("@Application_Date", item.ApplicationDate);
                
                conn.Open();
                command.ExecuteNonQuery();
                conn.Close();
            }
        }
    }
}
