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
    public class ApplicantSkillRepository : ADOBaseRepository, IDataRepository<ApplicantSkillPoco>
    {
        public void Add(params ApplicantSkillPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand();

                command.Connection = conn;

                foreach (ApplicantSkillPoco item in items)
                {
                    command.CommandText = @"INSERT INTO [dbo].[Applicant_Skills]
                                               ([Id]
                                               ,[Applicant]
                                               ,[Skill]
                                               ,[Skill_Level]
                                               ,[Start_Month]
                                               ,[Start_Year]
                                               ,[End_Month]
                                               ,[End_Year])
                                            VALUES
                                               (@Id
                                               ,@Applicant
                                               ,@Skill
                                               ,@SkillLevel
                                               ,@StartMonth
                                               ,@StartYear
                                               ,@EndMonth
                                               ,@EndYear)";

                    command.Parameters.AddWithValue("@Id", item.Id);
                    command.Parameters.AddWithValue("@Applicant", item.Applicant);
                    command.Parameters.AddWithValue("@Skill", item.Skill);
                    command.Parameters.AddWithValue("@SkillLevel", item.SkillLevel);
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

        public IList<ApplicantSkillPoco> GetAll(params Expression<Func<ApplicantSkillPoco, object>>[] navigationProperties)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;

            cmd.CommandText = @"SELECT [Id]
                                      ,[Applicant]
                                      ,[Skill]
                                      ,[Skill_Level]
                                      ,[Start_Month]
                                      ,[Start_Year]
                                      ,[End_Month]
                                      ,[End_Year]
                                      ,[Time_Stamp]
                                 FROM [dbo].[Applicant_Skills]";

            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            ApplicantSkillPoco[] applSkillPoco = new ApplicantSkillPoco[1000];
            int counter = 0;

            while (reader.Read())
            {
                ApplicantSkillPoco skillPoco = new ApplicantSkillPoco();
                skillPoco.Id = (Guid)reader["Id"];
                skillPoco.Applicant = (Guid)reader["Applicant"];
                skillPoco.Skill = Convert.ToString(reader["Skill"]);
                skillPoco.SkillLevel = Convert.ToString(reader["Skill_Level"]);
                skillPoco.StartMonth = (byte)reader["Start_Month"];
                skillPoco.StartYear = Convert.ToInt32(reader["Start_Year"]);
                skillPoco.EndMonth = (byte)reader["End_Month"];
                skillPoco.EndYear = Convert.ToInt32(reader["End_Year"]);
                skillPoco.TimeStamp = (byte[])reader["Time_Stamp"];

                applSkillPoco[counter++] = skillPoco;
            }

            conn.Close();

            return applSkillPoco.Where(p => p != null).ToList();
        }

        public IList<ApplicantSkillPoco> GetList(Expression<Func<ApplicantSkillPoco, bool>> where, params Expression<Func<ApplicantSkillPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantSkillPoco GetSingle(Expression<Func<ApplicantSkillPoco, bool>> where, params Expression<Func<ApplicantSkillPoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantSkillPoco> applSkillPoco = GetAll().AsQueryable();
            return applSkillPoco.Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantSkillPoco[] items)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);

            SqlCommand command = new SqlCommand();
            command.Connection = conn;

            foreach (ApplicantSkillPoco item in items)
            {
                command.CommandText = @"DELETE FROM [dbo].[Applicant_Skills]
                                            WHERE [Id] = @Id";

                command.Parameters.AddWithValue("@Id", item.Id);
                conn.Open();
                command.ExecuteNonQuery();
                conn.Close();
            }
        }

        public void Update(params ApplicantSkillPoco[] items)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);

            SqlCommand command = new SqlCommand();
            command.Connection = conn;

            foreach (ApplicantSkillPoco item in items)
            {
                command.CommandText = @"UPDATE [dbo].[Applicant_Skills]
                                           SET [Id] = @Id
                                              ,[Applicant] = @Applicant
                                              ,[Skill] = @Skill
                                              ,[Skill_Level] = @SkillLevel
                                              ,[Start_Month] = @StartMonth
                                              ,[Start_Year] = @StartYear
                                              ,[End_Month] = @EndMonth
                                              ,[End_Year] = @EndYear
                                            WHERE Id = @Id";

                command.Parameters.AddWithValue("@Id", item.Id);
                command.Parameters.AddWithValue("@Applicant", item.Applicant);
                command.Parameters.AddWithValue("@Skill", item.Skill);
                command.Parameters.AddWithValue("@SkillLevel", item.SkillLevel);
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
