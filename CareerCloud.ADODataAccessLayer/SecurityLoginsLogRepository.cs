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
    public class SecurityLoginsLogRepository : ADOBaseRepository, IDataRepository<SecurityLoginsLogPoco>
    {
        public void Add(params SecurityLoginsLogPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand();

                command.Connection = conn;

                foreach (SecurityLoginsLogPoco item in items)
                {
                    command.CommandText = @"INSERT INTO [dbo].[Security_Logins_Log]
                                                   ([Id]
                                                   ,[Login]
                                                   ,[Source_IP]
                                                   ,[Logon_Date]
                                                   ,[Is_Succesful])
                                             VALUES
                                                   (@Id
                                                   ,@Login
                                                   ,@SourceIP
                                                   ,@LogonDate
                                                   ,@IsSuccesful)";

                    command.Parameters.AddWithValue("@Id", item.Id);
                    command.Parameters.AddWithValue("@Login", item.Login);
                    command.Parameters.AddWithValue("@SourceIP", item.SourceIP);
                    command.Parameters.AddWithValue("@LogonDate", item.LogonDate);
                    command.Parameters.AddWithValue("@IsSuccesful", item.IsSuccesful);
                    
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

        public IList<SecurityLoginsLogPoco> GetAll(params Expression<Func<SecurityLoginsLogPoco, object>>[] navigationProperties)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;

            cmd.CommandText = @"SELECT [Id]
                                      ,[Login]
                                      ,[Source_IP]
                                      ,[Logon_Date]
                                      ,[Is_Succesful]
                                  FROM [dbo].[Security_Logins_Log]";

            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            SecurityLoginsLogPoco[] secLoginLogPoco = new SecurityLoginsLogPoco[2000];
            int counter = 0;

            while (reader.Read())
            {
                SecurityLoginsLogPoco loginLogPoco = new SecurityLoginsLogPoco();
                loginLogPoco.Id = (Guid)reader["Id"];
                loginLogPoco.Login = (Guid)reader["Login"];
                loginLogPoco.SourceIP = Convert.ToString(reader["Source_IP"]);
                loginLogPoco.LogonDate = Convert.ToDateTime(reader["Logon_Date"]);
                loginLogPoco.IsSuccesful = Convert.ToBoolean(reader["Is_Succesful"]);
                
                secLoginLogPoco[counter++] = loginLogPoco;
            }

            conn.Close();

            return secLoginLogPoco.Where(p => p != null).ToList();
        }

        public IList<SecurityLoginsLogPoco> GetList(Expression<Func<SecurityLoginsLogPoco, bool>> where, params Expression<Func<SecurityLoginsLogPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public SecurityLoginsLogPoco GetSingle(Expression<Func<SecurityLoginsLogPoco, bool>> where, params Expression<Func<SecurityLoginsLogPoco, object>>[] navigationProperties)
        {
            IQueryable<SecurityLoginsLogPoco> educationPoco = GetAll().AsQueryable();
            return educationPoco.Where(where).FirstOrDefault();
        }

        public void Remove(params SecurityLoginsLogPoco[] items)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);

            SqlCommand command = new SqlCommand();
            command.Connection = conn;

            foreach (SecurityLoginsLogPoco item in items)
            {
                command.CommandText = @"DELETE FROM [dbo].[Security_Logins_Log]
                                            WHERE [Id] = @Id";

                command.Parameters.AddWithValue("@Id", item.Id);
                conn.Open();
                command.ExecuteNonQuery();
                conn.Close();
            }
        }

        public void Update(params SecurityLoginsLogPoco[] items)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);

            SqlCommand command = new SqlCommand();
            command.Connection = conn;

            foreach (SecurityLoginsLogPoco item in items)
            {
                command.CommandText = @"UPDATE [dbo].[Security_Logins_Log]
                                           SET [Login] = @Login
                                              ,[Source_IP] = @SourceIP
                                              ,[Logon_Date] = @LogonDate
                                              ,[Is_Succesful] = @IsSuccesful
                                         WHERE [Id] = @Id";

                command.Parameters.AddWithValue("@Id", item.Id);
                command.Parameters.AddWithValue("@Login", item.Login);
                command.Parameters.AddWithValue("@SourceIP", item.SourceIP);
                command.Parameters.AddWithValue("@LogonDate", item.LogonDate);
                command.Parameters.AddWithValue("@IsSuccesful", item.IsSuccesful);

                conn.Open();
                command.ExecuteNonQuery();
                conn.Close();
            }

        }
    }
}
