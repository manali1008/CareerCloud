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
    public class ApplicantResumeRepository : ADOBaseRepository, IDataRepository<ApplicantResumePoco>
    {
        public void Add(params ApplicantResumePoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand();

                command.Connection = conn;

                foreach (ApplicantResumePoco item in items)
                {
                    command.CommandText = @"INSERT INTO [dbo].[Applicant_Resumes]
                                               ([Id]
                                               ,[Applicant]
                                               ,[Resume]
                                               ,[Last_Updated])
                                            VALUES
                                                (@Id
                                               ,@Applicant
                                               ,@Resume
                                               ,@LastUpdated)";

                    command.Parameters.AddWithValue("@Id", item.Id);
                    command.Parameters.AddWithValue("@Applicant", item.Applicant);
                    command.Parameters.AddWithValue("@Resume", item.Resume);
                    command.Parameters.AddWithValue("@LastUpdated", item.LastUpdated);
                    
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

        public IList<ApplicantResumePoco> GetAll(params Expression<Func<ApplicantResumePoco, object>>[] navigationProperties)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;

            cmd.CommandText = @"SELECT [Id]
                                      ,[Applicant]
                                      ,[Resume]
                                      ,[Last_Updated]
                                  FROM [dbo].[Applicant_Resumes]";

            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            ApplicantResumePoco[] applResumePoco = new ApplicantResumePoco[1000];
            int counter = 0;

            while (reader.Read())
            {
                ApplicantResumePoco resumePoco = new ApplicantResumePoco();
                resumePoco.Id = (Guid)reader["Id"];
                resumePoco.Applicant = (Guid)reader["Applicant"];
                resumePoco.Resume = Convert.ToString(reader["Resume"]);
                //resumePoco.LastUpdated = (DateTime?)reader["Last_Updated"];
                resumePoco.LastUpdated = (reader["Last_Updated"] as DateTime?) ?? null;

                applResumePoco[counter++] = resumePoco;
            }

            conn.Close();

            return applResumePoco.Where(p => p != null).ToList();
        }

        public IList<ApplicantResumePoco> GetList(Expression<Func<ApplicantResumePoco, bool>> where, params Expression<Func<ApplicantResumePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantResumePoco GetSingle(Expression<Func<ApplicantResumePoco, bool>> where, params Expression<Func<ApplicantResumePoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantResumePoco> resumePoco = GetAll().AsQueryable();
            return resumePoco.Where(where).FirstOrDefault();            
        }

        public void Remove(params ApplicantResumePoco[] items)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);

            SqlCommand command = new SqlCommand();
            command.Connection = conn;

            foreach (ApplicantResumePoco item in items)
            {
                command.CommandText = @"DELETE FROM [dbo].[Applicant_Resumes]
                                            WHERE [Id] = @Id";

                command.Parameters.AddWithValue("@Id", item.Id);
                conn.Open();
                command.ExecuteNonQuery();
                conn.Close();
            }
        }

        public void Update(params ApplicantResumePoco[] items)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);

            SqlCommand command = new SqlCommand();
            command.Connection = conn;

            foreach (ApplicantResumePoco item in items)
            {
                command.CommandText = @"UPDATE [dbo].[Applicant_Resumes]
                                           SET [Applicant] = @Applicant
                                              ,[Resume] = @Resume
                                              ,[Last_Updated] = @LastUpdated
                                           WHERE Id=@Id";

                command.Parameters.AddWithValue("@Id", item.Id);
                command.Parameters.AddWithValue("@Applicant", item.Applicant);
                command.Parameters.AddWithValue("@Resume", item.Resume);
                command.Parameters.AddWithValue("@LastUpdated", item.LastUpdated);
                
                conn.Open();
                command.ExecuteNonQuery();
                conn.Close();
            }
        }
    }
}
