using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using FinalC;
using System.Reflection.Metadata.Ecma335;

namespace Final_C
{
    public class EmployeeDAL
    {
        SqlConnection conn;
        public EmployeeDAL() 
        {
            conn = new DBConnector().GetConnection();
            conn.Open();
        }
        public Employee? SelectByUsernamAndPassword(string username, string password)
        {
            
            Employee emp = null;
            string sql = "SELECT * FROM EMPLOYEE WHERE username = @0 AND password = @1";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("0", username);
            cmd.Parameters.AddWithValue("1", password);
            SqlDataReader reader =  cmd.ExecuteReader();
            while (reader.Read())
            {
               emp = new Employee();
               emp.Id = (int) reader["id"];
               emp.User = (string)reader["username"];
               emp.FullName = (string) reader["name"];
               emp.Email = (string) reader["email"];
               //emp.Password = (string) reader["password"];
               emp.Role = (EmployeeRole) reader["role"];
            }
            return emp;
        }
        public Employee? SelectById(int id)
        {

            Employee? emp = null;
            string sql = "SELECT * FROM EMPLOYEE WHERE id = @0";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("0", id);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                emp = new Employee();
                emp.Id = (int)reader["id"];
                emp.User = (string)reader["username"];
                emp.FullName = (string)reader["name"];
                emp.Email = (string)reader["email"];
                emp.Role = (EmployeeRole)reader["role"];
            }
            return emp;
        }
        public List<Employee> SelectByKey(string key)
        {

            List<Employee> list = new List<Employee>();
            string sql = "SELECT * FROM EMPLOYEE WHERE email LIKE @0 OR name LIKE @1 OR username LIKE @2";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("0", key);
            cmd.Parameters.AddWithValue("1", key);
            cmd.Parameters.AddWithValue("2", key);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Employee emp = new Employee();
                emp.Id = (int)reader["id"];
                emp.User = (string)reader["username"];
                emp.FullName = (string)reader["name"];
                emp.Email = (string)reader["email"];
                emp.Role = (EmployeeRole)reader["role"];
                list.Add(emp);
            }
            return list;
        }
        public List<Employee> SelectAll() 
        {

            List<Employee> list = new List<Employee>();
            string sql = "SELECT * FROM EMPLOYEE";
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Employee emp = new Employee();
                emp.Id = (int)reader["id"];
                emp.User = (string)reader["username"];
                emp.FullName = (string)reader["name"];
                emp.Email = (string)reader["email"];
                emp.Role = (EmployeeRole)reader["role"];
                list.Add(emp);
            }
            return list;
        }
        public int Insert(Employee emp)
        {
            string sql = "INSERT INTO EMPLOYEE(user,fullname,email,password,role) VALUES (@0,@1,@2,@3,@4)";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("0", emp.User);
            cmd.Parameters.AddWithValue("1", emp.FullName);
            cmd.Parameters.AddWithValue("2", emp.Email);
            cmd.Parameters.AddWithValue("3", emp.Password);
            cmd.Parameters.AddWithValue("4", emp.Role);
            int rows = cmd.ExecuteNonQuery();
            return rows;
        }

        public int Update(Employee emp)
        {
            string sql = "UPDATE EMPLOYEE SET email = @1 OR SET fullname = @2 OR SET role = @3 WHERE id=@0";
            SqlCommand command = new SqlCommand(sql, conn);
            command.Parameters.AddWithValue("@0", emp.Id);
            command.Parameters.AddWithValue("@1", emp.Email);
            command.Parameters.AddWithValue("@2", emp.FullName);
            command.Parameters.AddWithValue("@3", emp.Role);
            return command.ExecuteNonQuery();
        }
        public int Delete(int id)
        {
            string sql = "DELETE FROM EMPL WHERE id=@0";
            SqlCommand command = new SqlCommand(sql, conn);
            command.Parameters.AddWithValue("@0", id);
            return command.ExecuteNonQuery();
        }
        public void Close()
        {
            conn.Close();
        }

        internal int Update(List<Employee?> employees)
        {
            throw new NotImplementedException();
        }
    }
}
