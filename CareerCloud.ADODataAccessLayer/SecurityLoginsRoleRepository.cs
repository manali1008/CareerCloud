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
    public class SecurityLoginsRoleRepository : ADOBaseRepository, IDataRepository<SecurityLoginsRolePoco>
    {
        public void Add(params SecurityLoginsRolePoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand();

                command.Connection = conn;

                foreach (SecurityLoginsRolePoco item in items)
                {
                    command.CommandText = @"INSERT INTO [dbo].[Security_Logins_Roles]
                                                   ([Id]
                                                   ,[Login]
                                                   ,[Role])
                                             VALUES
                                                   (@Id
                                                   ,@Login
                                                   ,@Role)";

                    command.Parameters.AddWithValue("@Id", item.Id);
                    command.Parameters.AddWithValue("@Login", item.Login);
                    command.Parameters.AddWithValue("@Role", item.Role);
                    
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

        public IList<SecurityLoginsRolePoco> GetAll(params Expression<Func<SecurityLoginsRolePoco, object>>[] navigationProperties)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;

            cmd.CommandText = @"SELECT [Id]
                                      ,[Login]
                                      ,[Role]
                                      ,[Time_Stamp]
                                  FROM [dbo].[Security_Logins_Roles]";

            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            SecurityLoginsRolePoco[] secLoginRolePoco = new SecurityLoginsRolePoco[1000];
            int counter = 0;

            while (reader.Read())
            {
                SecurityLoginsRolePoco loginRolePoco = new SecurityLoginsRolePoco();
                loginRolePoco.Id = (Guid)reader["Id"];
                loginRolePoco.Login = (Guid)reader["Login"];
                loginRolePoco.Role = (Guid)reader["Role"];
                loginRolePoco.TimeStamp = (byte[])reader["Time_Stamp"];

                secLoginRolePoco[counter++] = loginRolePoco;
            }

            conn.Close();

            return secLoginRolePoco.Where(p => p != null).ToList();
        }

        public IList<SecurityLoginsRolePoco> GetList(Expression<Func<SecurityLoginsRolePoco, bool>> where, params Expression<Func<SecurityLoginsRolePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public SecurityLoginsRolePoco GetSingle(Expression<Func<SecurityLoginsRolePoco, bool>> where, params Expression<Func<SecurityLoginsRolePoco, object>>[] navigationProperties)
        {
            IQueryable<SecurityLoginsRolePoco> educationPoco = GetAll().AsQueryable();
            return educationPoco.Where(where).FirstOrDefault();
        }

        public void Remove(params SecurityLoginsRolePoco[] items)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);

            SqlCommand command = new SqlCommand();
            command.Connection = conn;

            foreach (SecurityLoginsRolePoco item in items)
            {
                command.CommandText = @"DELETE FROM [dbo].[Security_Logins_Roles]
                                            WHERE [Id] = @Id";

                command.Parameters.AddWithValue("@Id", item.Id);
                conn.Open();
                command.ExecuteNonQuery();
                conn.Close();
            }
        }

        public void Update(params SecurityLoginsRolePoco[] items)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);

            SqlCommand command = new SqlCommand();
            command.Connection = conn;

            foreach (SecurityLoginsRolePoco item in items)
            {
                command.CommandText = @"UPDATE [dbo].[Security_Logins_Roles]
                                        SET [Login] = @Login
                                            ,[Role] = @Role
                                         WHERE [Id] = @Id";

                command.Parameters.AddWithValue("@Login", item.Login);
                command.Parameters.AddWithValue("@Role", item.Role);

                conn.Open();
                command.ExecuteNonQuery();
                conn.Close();
            }

        }
    }
}
