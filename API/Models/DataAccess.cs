using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace APIWork.Models
{
    public class DataAccess : DbContext
    {
        private string connectionString = "Server=socem1.uopnet.plymouth.ac.uk;Database=COMP2001_ZDauletov;User Id=ZDauletov;Password=RtoB400+;";
        public DataAccess(DbContextOptions<DataAccess> options) : base(options)
        {
            
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
        }
        public string GetConnectionString()
        {
            var configurationBuilder = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                 .AddJsonFile("appsettings.json");

            IConfiguration config = configurationBuilder.Build();
            string connectionString = config["ConnectionStrings:COMP2001_DB"];
            return connectionString;
        }
        public bool Validate(UserModel user)
        {
            bool isValidate = false;
            int retval = 0;
            try
            {
                using (var connection = new SqlConnection(GetConnectionString()))
                {
                    SqlCommand command = connection.CreateCommand();
                    command.CommandText = "dbo.spUserModel_Validate";
                    command.CommandType = CommandType.StoredProcedure;

                    SqlParameter p1 = command.CreateParameter();
                    p1.SqlDbType = SqlDbType.NVarChar;
                    p1.ParameterName = "@Email";
                    p1.Value = user.Email;

                    SqlParameter p2 = command.CreateParameter();
                    p2.SqlDbType = SqlDbType.NVarChar;
                    p2.ParameterName = "@Password";
                    p2.Value = user.Password;

                    command.Parameters.Add(p1);
                    command.Parameters.Add(p2);
                    command.Parameters.Add("@returnValue", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;

                    connection.Open();
                    command.ExecuteNonQuery();
                    retval = (int)command.Parameters["@returnValue"].Value;
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
            }
            if(retval == 1)
            {
                isValidate = true;
            }

            return isValidate;
        }

        public void Register(UserModel user, out string responseMessage)
        {
            string rMessage= "";
            try
            {
                using(var connection = new SqlConnection(GetConnectionString()))
                {
                    SqlCommand command = connection.CreateCommand();
                    command.CommandText = "dbo.UserModel_RegisterUser";
                    command.CommandType = CommandType.StoredProcedure;

                    SqlParameter p1 = command.CreateParameter();
                    p1.SqlDbType = SqlDbType.NVarChar;
                    p1.ParameterName = "@FirstName";
                    p1.Value = user.FirstName;

                    SqlParameter p2 = command.CreateParameter();
                    p2.SqlDbType = SqlDbType.NVarChar;
                    p2.ParameterName = "@LastName";
                    p2.Value = user.LastName;

                    SqlParameter p3 = command.CreateParameter();
                    p3.SqlDbType = SqlDbType.NVarChar;
                    p3.ParameterName = "@Email";
                    p3.Value = user.Email;

                    SqlParameter p4 = command.CreateParameter();
                    p4.SqlDbType = SqlDbType.NVarChar;
                    p4.ParameterName = "@Password";
                    p4.Value = user.Password;

                    command.Parameters.Add(p1);
                    command.Parameters.Add(p2);
                    command.Parameters.Add(p3);
                    command.Parameters.Add(p4);
                    command.Parameters.Add("@responseMessage", SqlDbType.NVarChar).Direction = ParameterDirection.ReturnValue;

                    connection.Open();
                    command.ExecuteNonQuery();
                    rMessage = (string)command.Parameters["@responseMessage"].Value;
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
            }

            responseMessage = rMessage;
        }
        public void Update(UserModel user, int id)
        {
            try
            {
                using(var connection = new SqlConnection(GetConnectionString()))
                {
                    SqlCommand command = connection.CreateCommand();
                    command.CommandText = "dbo.spUserModel_UpdateUser";
                    command.CommandType = CommandType.StoredProcedure;

                    SqlParameter p1 = command.CreateParameter();
                    p1.SqlDbType = SqlDbType.Int;
                    p1.ParameterName = "@UserId";
                    p1.Value = id;

                    SqlParameter p2 = command.CreateParameter();
                    p2.SqlDbType = SqlDbType.NVarChar;
                    p2.ParameterName = "@FirstName";
                    p2.Value = user.FirstName;

                    SqlParameter p3 = command.CreateParameter();
                    p3.SqlDbType = SqlDbType.NVarChar;
                    p3.ParameterName = "@LastName";
                    p3.Value = user.LastName;

                    SqlParameter p4 = command.CreateParameter();
                    p4.SqlDbType = SqlDbType.NVarChar;
                    p4.ParameterName = "@Email";
                    p4.Value = user.Email;

                    SqlParameter p5 = command.CreateParameter();
                    p5.SqlDbType = SqlDbType.NVarChar;
                    p5.ParameterName = "@Password";
                    p5.Value = user.Password;

                    command.Parameters.Add(p1);
                    command.Parameters.Add(p2);
                    command.Parameters.Add(p3);
                    command.Parameters.Add(p4);
                    command.Parameters.Add(p5);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }catch(SqlException e)
            {
                Console.WriteLine(e);
            }
        }
        public void Delete(int id)
        {
            try
            {
                using (var connection = new SqlConnection(GetConnectionString()))
                {
                    SqlCommand command = connection.CreateCommand();
                    command.CommandText = "dbo.spUserModel_DeleteUser";
                    command.CommandType = CommandType.StoredProcedure;

                    SqlParameter p1 = command.CreateParameter();
                    p1.SqlDbType = SqlDbType.Int;
                    p1.ParameterName = "@UserId";
                    p1.Value = id;

                    command.Parameters.Add(p1);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            } catch (SqlException e)
            {
                Console.WriteLine(e);
            }
        }
        public virtual DbSet<UserModel> Users { get; set; }
        public virtual DbSet<Session> Sessions { get; set; }
        public virtual DbSet<PasswordModel> Passwords { get; set; }
    }
}