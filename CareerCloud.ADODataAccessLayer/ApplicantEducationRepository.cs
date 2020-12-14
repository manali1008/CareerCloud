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
    public class ApplicantEducationRepository : ADOBaseRepository, IDataRepository<ApplicantEducationPoco>
    {
        public void Add(params ApplicantEducationPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand();
                
                command.Connection = conn;

                foreach (ApplicantEducationPoco item in items)
                {
                    command.CommandText = @"INSERT INTO [dbo].[Applicant_Educations]
                                           ([Id]
                                           ,[Applicant]
                                           ,[Major]
                                           ,[Certificate_Diploma]
                                           ,[Start_Date]
                                           ,[Completion_Date]
                                           ,[Completion_Percent])
                                        VALUES
                                           (@Id
                                           ,@Applicant
                                           ,@Major
                                           ,@CertificateDiploma
                                           ,@StartDate
                                           ,@CompletionDate
                                           ,@CompletionPercent)";

                    command.Parameters.AddWithValue("@Id", item.Id);
                    command.Parameters.AddWithValue("@Applicant", item.Applicant);
                    command.Parameters.AddWithValue("@Major", item.Major);
                    command.Parameters.AddWithValue("@CertificateDiploma", item.CertificateDiploma);
                    command.Parameters.AddWithValue("@StartDate", item.StartDate);
                    command.Parameters.AddWithValue("@CompletionDate", item.CompletionDate);
                    command.Parameters.AddWithValue("@CompletionPercent", item.CompletionPercent);

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

        public IList<ApplicantEducationPoco> GetAll(params Expression<Func<ApplicantEducationPoco, object>>[] navigationProperties)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;

            cmd.CommandText = @"SELECT [Id]
                                  ,[Applicant]
                                  ,[Major]
                                  ,[Certificate_Diploma]
                                  ,[Start_Date]
                                  ,[Completion_Date]
                                  ,[Completion_Percent]
                                  ,[Time_Stamp]
                              FROM [dbo].[Applicant_Educations]";

            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            ApplicantEducationPoco[] educationPoco = new ApplicantEducationPoco[1000];
            int counter = 0;

            while(reader.Read())
            {
                ApplicantEducationPoco eduPoco = new ApplicantEducationPoco();
                eduPoco.Id = (Guid) reader["Id"];
                eduPoco.Applicant =(Guid) reader["Applicant"];
                eduPoco.Major = Convert.ToString( reader["Major"]);
                eduPoco.CertificateDiploma = Convert.ToString( reader["Certificate_Diploma"]);
                eduPoco.StartDate = (DateTime?)reader["Start_Date"];
                eduPoco.CompletionDate = (DateTime?)reader["Completion_Date"];
                eduPoco.CompletionPercent = (byte?) reader["Completion_Percent"];
                eduPoco.TimeStamp = (byte[])reader["Time_Stamp"];

                educationPoco[counter++] = eduPoco;
            }

            conn.Close();

            return educationPoco.Where(p => p != null).ToList();
        }

        public IList<ApplicantEducationPoco> GetList(Expression<Func<ApplicantEducationPoco, bool>> where, params Expression<Func<ApplicantEducationPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantEducationPoco GetSingle(Expression<Func<ApplicantEducationPoco, bool>> where, params Expression<Func<ApplicantEducationPoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantEducationPoco> educationPoco = GetAll().AsQueryable();
            return educationPoco.Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantEducationPoco[] items)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            
            SqlCommand command = new SqlCommand();
            command.Connection = conn;

            foreach (ApplicantEducationPoco item in items)
            {
                command.CommandText = @"DELETE FROM [dbo].[Applicant_Educations]
                                            WHERE [Id] = @Id";


                command.Parameters.AddWithValue("@Id", item.Id);
                conn.Open();
                command.ExecuteNonQuery();
                conn.Close();
            }
        }

        public void Update(params ApplicantEducationPoco[] items)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            
            SqlCommand command = new SqlCommand();
            command.Connection = conn;

            foreach (ApplicantEducationPoco item in items)
            {
                command.CommandText = @"UPDATE [dbo].[Applicant_Educations]
                                           SET [Applicant] = @Applicant
                                              ,[Major] = @Major
                                              ,[Certificate_Diploma] = @CertificateDiploma
                                              ,[Start_Date] = @StartDate
                                              ,[Completion_Date] = @CompletionDate
                                              ,[Completion_Percent] = @CompletionPercent
                                         WHERE [Id] = @Id";

                command.Parameters.AddWithValue("@Id", item.Id);
                command.Parameters.AddWithValue("@Applicant", item.Applicant);
                command.Parameters.AddWithValue("@Major", item.Major);
                command.Parameters.AddWithValue("@CertificateDiploma", item.CertificateDiploma);
                command.Parameters.AddWithValue("@StartDate", item.StartDate);
                command.Parameters.AddWithValue("@CompletionDate", item.CompletionDate);
                command.Parameters.AddWithValue("@CompletionPercent", item.CompletionPercent);

                conn.Open();
                command.ExecuteNonQuery();
                conn.Close();
            }
            
        }
    }
}
