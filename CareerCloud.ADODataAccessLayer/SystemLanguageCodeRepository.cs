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
    public class SystemLanguageCodeRepository : ADOBaseRepository , IDataRepository<SystemLanguageCodePoco>
    {
        public void Add(params SystemLanguageCodePoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand();

                command.Connection = conn;

                foreach (SystemLanguageCodePoco item in items)
                {
                    command.CommandText = @"INSERT INTO [dbo].[System_Language_Codes]
                                                   ([LanguageID]
                                                   ,[Name]
                                                   ,[Native_Name])
                                             VALUES
                                                   (@LanguageID
                                                   ,@Name
                                                   ,@NativeName)";

                    command.Parameters.AddWithValue("@LanguageID", item.LanguageID);
                    command.Parameters.AddWithValue("@Name", item.Name);
                    command.Parameters.AddWithValue("@NativeName", item.NativeName);

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

        public IList<SystemLanguageCodePoco> GetAll(params Expression<Func<SystemLanguageCodePoco, object>>[] navigationProperties)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;

            cmd.CommandText = @"SELECT [LanguageID]
                                      ,[Name]
                                      ,[Native_Name]
                                  FROM [dbo].[System_Language_Codes]";

            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            SystemLanguageCodePoco[] sysLangCodePoco = new SystemLanguageCodePoco[1000];
            int counter = 0;

            while (reader.Read())
            {
                SystemLanguageCodePoco langCodePoco = new SystemLanguageCodePoco();
                langCodePoco.LanguageID = Convert.ToString(reader["LanguageID"]);
                langCodePoco.Name = Convert.ToString(reader["Name"]);
                langCodePoco.NativeName = Convert.ToString(reader["Native_Name"]);

                sysLangCodePoco[counter++] = langCodePoco;
            }

            conn.Close();

            return sysLangCodePoco.Where(p => p != null).ToList();
        }

        public IList<SystemLanguageCodePoco> GetList(Expression<Func<SystemLanguageCodePoco, bool>> where, params Expression<Func<SystemLanguageCodePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public SystemLanguageCodePoco GetSingle(Expression<Func<SystemLanguageCodePoco, bool>> where, params Expression<Func<SystemLanguageCodePoco, object>>[] navigationProperties)
        {
            IQueryable<SystemLanguageCodePoco> educationPoco = GetAll().AsQueryable();
            return educationPoco.Where(where).FirstOrDefault();
        }

        public void Remove(params SystemLanguageCodePoco[] items)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);

            SqlCommand command = new SqlCommand();
            command.Connection = conn;

            foreach (SystemLanguageCodePoco item in items)
            {
                command.CommandText = @"DELETE FROM [dbo].[System_Language_Codes]
                                            WHERE [LanguageID] = @LanguageID";

                command.Parameters.AddWithValue("@LanguageID", item.LanguageID);
                conn.Open();
                command.ExecuteNonQuery();
                conn.Close();
            }
        }

        public void Update(params SystemLanguageCodePoco[] items)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);

            SqlCommand command = new SqlCommand();
            command.Connection = conn;

            foreach (SystemLanguageCodePoco item in items)
            {
                command.CommandText = @"UPDATE [dbo].[System_Language_Codes]
                                           SET [Name] = @Name
                                              ,[Native_Name] = @NativeName
                                         WHERE [LanguageID] = @LanguageID";

                command.Parameters.AddWithValue("@LanguageID", item.LanguageID);
                command.Parameters.AddWithValue("@Name", item.Name);
                command.Parameters.AddWithValue("@NativeName", item.NativeName);

                conn.Open();
                command.ExecuteNonQuery();
                conn.Close();
            }

        }
    }
}
