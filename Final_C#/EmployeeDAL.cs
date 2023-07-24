using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using FinalC;
using System.Reflection.Metadata.Ecma335;
using System.IO;
using Final_C_;

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

        public Employee? SelectByUsernameAndPassword(string username, string password)
        {
            Console.WriteLine("I LOVE YIU EmployeeDAL");
            Employee? emp = null;
            string sql = "SELECT * FROM EMPLOYEE WHERE username = @0 AND password = @1";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("0", username);
            cmd.Parameters.AddWithValue("1", password);
            SqlDataReader reader =  cmd.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine("I LOVE YIU EmployeeDAL1");
                emp = new Employee();
               emp.Id = (int) reader["id"];
               emp.User = (string)reader["username"];
               emp.FullName = (string) reader["name"];
               emp.Email = (string) reader["email"];
               emp.Password = (string) reader["password"];
               emp.Role = (EmployeeRole) reader["role"];
            }
            reader.Close();
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
            reader.Close();
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
            reader.Close();
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
            reader.Close();
            return list;
        }
        public int Insert(Employee emp)
        {
            string sql = "INSERT INTO EMPLOYEE(username,name,email,password,role) VALUES (@0,@1,@2,@3,@4)";
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
            string sql = "UPDATE EMPLOYEE SET email = @1, name = @2, role = @3 WHERE id=@0";
            SqlCommand command = new SqlCommand(sql, conn);
            command.Parameters.AddWithValue("@0", emp.Id);
            command.Parameters.AddWithValue("@1", emp.Email);
            command.Parameters.AddWithValue("@2", emp.FullName);
            command.Parameters.AddWithValue("@3", emp.Role);
            return command.ExecuteNonQuery();
        }
        public int Delete(int id)
        {
            string sql = "DELETE FROM EMPLOYEE WHERE id=@0";
            SqlCommand command = new SqlCommand(sql, conn);
            command.Parameters.AddWithValue("@0", id);
            return command.ExecuteNonQuery();
        }
        public List<string> Export()
        {
            List<string> csvData = new List<string>();
            try
            {
                string sql = "SELECT id, username, name, role FROM EMPLOYEE";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                StringBuilder sb = new StringBuilder();
                sb.AppendLine("ID,Username,Name,Role");

                while (reader.Read())
                {
                    int id = (int)reader["id"];
                    string username = (string)reader["username"];
                    string name = (string)reader["name"];
                    EmployeeRole role = (EmployeeRole)reader["role"];

                    string csvLine = $"{id},{username},{name},{role}";
                    sb.AppendLine(csvLine);
                }

                csvData.Add(sb.ToString());
                Console.WriteLine("Du lieu xuat ra thanh cong.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Khong the xuat du lieu ra xuat hien loi: " + ex.Message);
            }
            conn.Close();
            return csvData;
            
        }
        //public bool ChangePassword(string username, string currentPassword, string newPassword)
        //{
        //    string hashedCurrentPassword = Utils.Hash(currentPassword, "sha512");
        //    string hashedNewPassword = Utils.Hash(newPassword, "sha512");

        //    string sql = "UPDATE EMPLOYEE SET password = @newPassword WHERE username = @username AND password = @currentPassword";
        //    SqlCommand cmd = new SqlCommand(sql, conn);
        //    cmd.Parameters.AddWithValue("@newPassword", hashedNewPassword);
        //    cmd.Parameters.AddWithValue("@username", username);
        //    cmd.Parameters.AddWithValue("@currentPassword", hashedCurrentPassword);
        //    return cmd.ExecuteNonQuery();
        //}
        public bool ChangePassword(string username, string currentPassword, string newPassword)
        {

            if (String.IsNullOrEmpty(username) || String.IsNullOrEmpty(currentPassword))
            {
                return false;
            }

            string hashedCurrentPassword = currentPassword;
            string hashedNewPassword = newPassword;


            string sql = "UPDATE EMPLOYEE SET password = @newPassword WHERE username = @username AND password = @currentPassword";

            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@newPassword", hashedNewPassword);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@currentPassword", hashedCurrentPassword);

                int affectedRows = cmd.ExecuteNonQuery();

                return affectedRows == 1;
            }
        }
    }
}
