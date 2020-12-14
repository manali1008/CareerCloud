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
    public class CompanyJobSkillRepository : ADOBaseRepository, IDataRepository<CompanyJobSkillPoco>
    {
        public void Add(params CompanyJobSkillPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand();

                command.Connection = conn;

                foreach (CompanyJobSkillPoco item in items)
                {
                    command.CommandText = @"INSERT INTO [dbo].[Company_Job_Skills]
                                                   ([Id]
                                                   ,[Job]
                                                   ,[Skill]
                                                   ,[Skill_Level]
                                                   ,[Importance])
                                             VALUES
                                                   (@Id
                                                   ,@Job
                                                   ,@Skill
                                                   ,@SkillLevel
                                                   ,@Importance)";

                    command.Parameters.AddWithValue("@Id", item.Id);
                    command.Parameters.AddWithValue("@Job", item.Job);
                    command.Parameters.AddWithValue("@Skill", item.Skill);
                    command.Parameters.AddWithValue("@SkillLevel", item.SkillLevel);
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

        public IList<CompanyJobSkillPoco> GetAll(params Expression<Func<CompanyJobSkillPoco, object>>[] navigationProperties)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;

            cmd.CommandText = @"SELECT [Id]
                                      ,[Job]
                                      ,[Skill]
                                      ,[Skill_Level]
                                      ,[Importance]
                                      ,[Time_Stamp]
                                  FROM [dbo].[Company_Job_Skills]";

            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            CompanyJobSkillPoco[] compJobSkillPoco = new CompanyJobSkillPoco[5010];
            int counter = 0;

            while (reader.Read())
            {
                CompanyJobSkillPoco jobSkillPoco = new CompanyJobSkillPoco();
                jobSkillPoco.Id = (Guid)reader["Id"];
                jobSkillPoco.Job = (Guid)reader["Job"];
                jobSkillPoco.Skill = Convert.ToString(reader["Skill"]);
                jobSkillPoco.SkillLevel = Convert.ToString(reader["Skill_Level"]);
                jobSkillPoco.Importance = Convert.ToInt32(reader["Importance"]);
                jobSkillPoco.TimeStamp = (byte[])reader["Time_Stamp"];

                compJobSkillPoco[counter++] = jobSkillPoco;
            }

            conn.Close();

            return compJobSkillPoco.Where(p => p != null).ToList();
        }

        public IList<CompanyJobSkillPoco> GetList(Expression<Func<CompanyJobSkillPoco, bool>> where, params Expression<Func<CompanyJobSkillPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyJobSkillPoco GetSingle(Expression<Func<CompanyJobSkillPoco, bool>> where, params Expression<Func<CompanyJobSkillPoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyJobSkillPoco> educationPoco = GetAll().AsQueryable();
            return educationPoco.Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyJobSkillPoco[] items)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);

            SqlCommand command = new SqlCommand();
            command.Connection = conn;

            foreach (CompanyJobSkillPoco item in items)
            {
                command.CommandText = @"DELETE FROM [dbo].[Company_Job_Skills]
                                            WHERE [Id] = @Id";

                command.Parameters.AddWithValue("@Id", item.Id);

                conn.Open();
                command.ExecuteNonQuery();
                conn.Close();
            }
        }

        public void Update(params CompanyJobSkillPoco[] items)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);

            SqlCommand command = new SqlCommand();
            command.Connection = conn;

            foreach (CompanyJobSkillPoco item in items)
            {
                command.CommandText = @"UPDATE [dbo].[Company_Job_Skills]
                                           SET [Job] = @Job
                                              ,[Skill] = @Skill
                                              ,[Skill_Level] = @SkillLevel
                                              ,[Importance] = @Importance
                                           WHERE [Id] = @Id";

                command.Parameters.AddWithValue("@Id", item.Id);
                command.Parameters.AddWithValue("@Job", item.Job);
                command.Parameters.AddWithValue("@Skill", item.Skill);
                command.Parameters.AddWithValue("@SkillLevel", item.SkillLevel);
                command.Parameters.AddWithValue("@Importance", item.Importance);

                conn.Open();
                command.ExecuteNonQuery();
                conn.Close();
            }
        }
    }
}
