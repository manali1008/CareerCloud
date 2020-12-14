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
    public class SecurityRoleRepository : ADOBaseRepository, IDataRepository<SecurityRolePoco>
    {
        public void Add(params SecurityRolePoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand();

                command.Connection = conn;

                foreach (SecurityRolePoco item in items)
                {
                    command.CommandText = @"INSERT INTO [dbo].[Security_Roles]
                                                   ([Id]
                                                   ,[Role]
                                                   ,[Is_Inactive])
                                             VALUES
                                                   (@Id
                                                   ,@Role
                                                   ,@IsInactive)";

                    command.Parameters.AddWithValue("@Id", item.Id);
                    command.Parameters.AddWithValue("@Role", item.Role);
                    command.Parameters.AddWithValue("@IsInactive", item.IsInactive);
                    
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

        public IList<SecurityRolePoco> GetAll(params Expression<Func<SecurityRolePoco, object>>[] navigationProperties)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;

            cmd.CommandText = @"SELECT [Id]
                                      ,[Role]
                                      ,[Is_Inactive]
                                  FROM [dbo].[Security_Roles]";

            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            SecurityRolePoco[] secRolePoco = new SecurityRolePoco[1000];
            int counter = 0;

            while (reader.Read())
            {
                SecurityRolePoco rolePoco = new SecurityRolePoco();
                rolePoco.Id = (Guid)reader["Id"];
                rolePoco.Role = Convert.ToString(reader["Role"]);
                rolePoco.IsInactive = Convert.ToBoolean(reader["Is_Inactive"]);
                
                secRolePoco[counter++] = rolePoco;
            }

            conn.Close();

            return secRolePoco.Where(p => p != null).ToList();
        }

        public IList<SecurityRolePoco> GetList(Expression<Func<SecurityRolePoco, bool>> where, params Expression<Func<SecurityRolePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public SecurityRolePoco GetSingle(Expression<Func<SecurityRolePoco, bool>> where, params Expression<Func<SecurityRolePoco, object>>[] navigationProperties)
        {
            IQueryable<SecurityRolePoco> educationPoco = GetAll().AsQueryable();
            return educationPoco.Where(where).FirstOrDefault();
        }

        public void Remove(params SecurityRolePoco[] items)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);

            SqlCommand command = new SqlCommand();
            command.Connection = conn;

            foreach (SecurityRolePoco item in items)
            {
                command.CommandText = @"DELETE FROM [dbo].[Security_Roles]
                                            WHERE [Id] = @Id";

                command.Parameters.AddWithValue("@Id", item.Id);
                conn.Open();
                command.ExecuteNonQuery();
                conn.Close();
            }
        }

        public void Update(params SecurityRolePoco[] items)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);

            SqlCommand command = new SqlCommand();
            command.Connection = conn;

            foreach (SecurityRolePoco item in items)
            {
                command.CommandText = @"UPDATE [dbo].[Security_Roles]
                                           SET [Role] = @Role
                                              ,[Is_Inactive] = @IsInactive
                                         WHERE [Id] = @Id";

                command.Parameters.AddWithValue("@Id", item.Id);
                command.Parameters.AddWithValue("@Role", item.Role);
                command.Parameters.AddWithValue("@IsInactive", item.IsInactive);

                conn.Open();
                command.ExecuteNonQuery();
                conn.Close();
            }

        }
    }
}
