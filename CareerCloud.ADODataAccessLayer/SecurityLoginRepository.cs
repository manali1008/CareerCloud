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
    public class SecurityLoginRepository : ADOBaseRepository, IDataRepository<SecurityLoginPoco>
    {
        public void Add(params SecurityLoginPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand();

                command.Connection = conn;

                foreach (SecurityLoginPoco item in items)
                {
                    command.CommandText = @"INSERT INTO [dbo].[Security_Logins]
                                                   ([Id]
                                                   ,[Login]
                                                   ,[Password]
                                                   ,[Created_Date]
                                                   ,[Password_Update_Date]
                                                   ,[Agreement_Accepted_Date]
                                                   ,[Is_Locked]
                                                   ,[Is_Inactive]
                                                   ,[Email_Address]
                                                   ,[Phone_Number]
                                                   ,[Full_Name]
                                                   ,[Force_Change_Password]
                                                   ,[Prefferred_Language])
                                             VALUES
                                                   (@Id
                                                   ,@Login
                                                   ,@Password
                                                   ,@CreatedDate
                                                   ,@PasswordUpdateDate
                                                   ,@AgreementAcceptedDate
                                                   ,@IsLocked
                                                   ,@IsInactive
                                                   ,@Email
                                                   ,@Phone
                                                   ,@FullName
                                                   ,@ForceChangePassword
                                                   ,@PrefferredLanguage)";

                    command.Parameters.AddWithValue("@Id", item.Id);
                    command.Parameters.AddWithValue("@Login", item.Login);
                    command.Parameters.AddWithValue("@Password", item.Password);
                    command.Parameters.AddWithValue("@CreatedDate", item.Created);
                    command.Parameters.AddWithValue("@PasswordUpdateDate", item.PasswordUpdate);
                    command.Parameters.AddWithValue("@AgreementAcceptedDate", item.AgreementAccepted);
                    command.Parameters.AddWithValue("@IsLocked", item.IsLocked);
                    command.Parameters.AddWithValue("@IsInactive", item.IsInactive);
                    command.Parameters.AddWithValue("@Email", item.EmailAddress);
                    command.Parameters.AddWithValue("@Phone", item.PhoneNumber);
                    command.Parameters.AddWithValue("@FullName", item.FullName);
                    command.Parameters.AddWithValue("@ForceChangePassword", item.ForceChangePassword);
                    command.Parameters.AddWithValue("@PrefferredLanguage", item.PrefferredLanguage);

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

        public IList<SecurityLoginPoco> GetAll(params Expression<Func<SecurityLoginPoco, object>>[] navigationProperties)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;

            cmd.CommandText = @"SELECT [Id]
                                    ,[Login]
                                    ,[Password]
                                    ,[Created_Date]
                                    ,[Password_Update_Date]
                                    ,[Agreement_Accepted_Date]
                                    ,[Is_Locked]
                                    ,[Is_Inactive]
                                    ,[Email_Address]
                                    ,[Phone_Number]
                                    ,[Full_Name]
                                    ,[Force_Change_Password]
                                    ,[Prefferred_Language]
                                    ,[Time_Stamp]
                                FROM [dbo].[Security_Logins]";

            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            SecurityLoginPoco[] securityLoginPoco = new SecurityLoginPoco[1000];
            int counter = 0;

            while (reader.Read())
            {
                SecurityLoginPoco loginPoco = new SecurityLoginPoco();
                loginPoco.Id = (Guid)reader["Id"];
                loginPoco.Login = Convert.ToString(reader["Login"]);
                loginPoco.Password = Convert.ToString(reader["Password"]);
                loginPoco.Created = Convert.ToDateTime(reader["Created_Date"]);
                loginPoco.PasswordUpdate = (reader["Password_Update_Date"] as DateTime?) ?? null;
                loginPoco.AgreementAccepted = (reader["Agreement_Accepted_Date"] as DateTime?) ?? null;
                loginPoco.IsLocked = Convert.ToBoolean(reader["Is_Locked"]);
                loginPoco.IsInactive = Convert.ToBoolean(reader["Is_Inactive"]);
                loginPoco.EmailAddress = Convert.ToString(reader["Email_Address"]);
                loginPoco.PhoneNumber = Convert.ToString(reader["Phone_Number"]);
                loginPoco.FullName = Convert.ToString(reader["Full_Name"]);
                loginPoco.ForceChangePassword = Convert.ToBoolean(reader["Force_Change_Password"]);
                loginPoco.PrefferredLanguage = Convert.ToString(reader["Prefferred_Language"]);
                loginPoco.TimeStamp = (byte[])reader["Time_Stamp"];
                
                securityLoginPoco[counter++] = loginPoco;
            }

            conn.Close();

            return securityLoginPoco.Where(p => p != null).ToList();
        }

        public IList<SecurityLoginPoco> GetList(Expression<Func<SecurityLoginPoco, bool>> where, params Expression<Func<SecurityLoginPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public SecurityLoginPoco GetSingle(Expression<Func<SecurityLoginPoco, bool>> where, params Expression<Func<SecurityLoginPoco, object>>[] navigationProperties)
        {
            IQueryable<SecurityLoginPoco> educationPoco = GetAll().AsQueryable();
            return educationPoco.Where(where).FirstOrDefault();
        }

        public void Remove(params SecurityLoginPoco[] items)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);

            SqlCommand command = new SqlCommand();
            command.Connection = conn;

            foreach (SecurityLoginPoco item in items)
            {
                command.CommandText = @"DELETE FROM [dbo].[Security_Logins]
                                            WHERE [Id] = @Id";

                command.Parameters.AddWithValue("@Id", item.Id);
                conn.Open();
                command.ExecuteNonQuery();
                conn.Close();
            }
        }

        public void Update(params SecurityLoginPoco[] items)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);

            SqlCommand command = new SqlCommand();
            command.Connection = conn;

            foreach (SecurityLoginPoco item in items)
            {
                command.CommandText = @"UPDATE [dbo].[Security_Logins]
                                        SET [Login] = @Login
                                            ,[Password] = @Password
                                            ,[Created_Date] = @CreatedDate
                                            ,[Password_Update_Date] = @PasswordUpdateDate
                                            ,[Agreement_Accepted_Date] = @AgreementAcceptedDate
                                            ,[Is_Locked] = @IsLocked
                                            ,[Is_Inactive] = @IsInactive
                                            ,[Email_Address] = @Email
                                            ,[Phone_Number] = @Phone
                                            ,[Full_Name] = @FullName
                                            ,[Force_Change_Password] = @ForceChangePassword
                                            ,[Prefferred_Language] = @PrefferredLanguage
                                         WHERE [Id] = @Id";

                command.Parameters.AddWithValue("@Id", item.Id);
                command.Parameters.AddWithValue("@Login", item.Login);
                command.Parameters.AddWithValue("@Password", item.Password);
                command.Parameters.AddWithValue("@CreatedDate", item.Created);
                command.Parameters.AddWithValue("@PasswordUpdateDate", item.PasswordUpdate);
                command.Parameters.AddWithValue("@AgreementAcceptedDate", item.AgreementAccepted);
                command.Parameters.AddWithValue("@IsLocked", item.IsLocked);
                command.Parameters.AddWithValue("@IsInactive", item.IsInactive);
                command.Parameters.AddWithValue("@Email", item.EmailAddress);
                command.Parameters.AddWithValue("@Phone", item.PhoneNumber);
                command.Parameters.AddWithValue("@FullName", item.FullName);
                command.Parameters.AddWithValue("@ForceChangePassword", item.ForceChangePassword);
                command.Parameters.AddWithValue("@PrefferredLanguage", item.PrefferredLanguage);

                conn.Open();
                command.ExecuteNonQuery();
                conn.Close();
            }

        }
    }
}
