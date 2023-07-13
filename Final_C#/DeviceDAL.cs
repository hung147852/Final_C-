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
    public class DeviceDAL
    {
        SqlConnection conn;
        public DeviceDAL() 
        {
            conn = new DBConnector().GetConnection();
            conn.Open();
        }
        public Device? SelectById(int id)
        {

            Device? device = null;
            string sql = "SELECT * FROM DEVICE WHERE id = @0";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("0", id);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                device = new Device();
                device.Id = (int)reader["id"];
                device.Dname = (string)reader["name"];
                device.Quantity = (int)reader["quanity"];
            }
            reader.Close();
            return device;
        }
        public List<Device> SelectByKey(string key)
        {

            List<Device> list = new List<Device>();
            string sql = "SELECT * FROM DEVICE WHERE name LIKE @0";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("0", key);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Device device = new Device();
                device.Id = (int)reader["id"];
                device.Dname = (string)reader["name"];
                device.Quantity = (int)reader["quantity"];
                list.Add(device);
            }
            reader.Close();
            return list;
        }
        public List<Device> SelectAll() 
        {

            List<Device> list = new List<Device>();
            string sql = "SELECT * FROM DEVICE";
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Device device = new Device();
                device.Id = (int)reader["id"];
                device.Dname = (string)reader["name"];
                device.Quantity = (int)reader["quantity"];
                list.Add(device);
            }
            reader.Close();
            return list;
        }
        public int Insert(Device device)
        {
            string sql = "INSERT INTO DEVICE(name,quantity) VALUES (@0, @1, @2)";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("0", device.Id);
            cmd.Parameters.AddWithValue("1", device.Dname);
            cmd.Parameters.AddWithValue("2", device.Quantity);
            int rows = cmd.ExecuteNonQuery();
            return rows;
        }

        public int Update(Device device)
        {
            string sql = "UPDATE DEVICE SET name = @1 OR SET quantity = @2 WHERE id=@0";
            SqlCommand command = new SqlCommand(sql, conn);
            command.Parameters.AddWithValue("@0", device.Id);
            command.Parameters.AddWithValue("@1", device.Dname);
            command.Parameters.AddWithValue("@2", device.Quantity);
            return command.ExecuteNonQuery();
        }
        public int Delete(string name)
        {
            string sql = "DELETE FROM DEVICE WHERE name=@0";
            SqlCommand command = new SqlCommand(sql, conn);
            command.Parameters.AddWithValue("@0", name);
            return command.ExecuteNonQuery();
        }
        public List<string> Export()
        {
            List<string> csvData = new List<string>();
            try
            {
                // Truy vấn dữ liệu từ bảng EMPLOYEE
                string sql = "SELECT id, name, quantity FROM DEVICE";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                // Tạo dữ liệu CSV từ kết quả truy vấn
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("ID,Devicename,Quantity");

                while (reader.Read())
                {
                    int id = (int)reader["id"];
                    string name = (string)reader["name"];
                    int quantity = (int)reader["quantity"];

                    string csvLine = $"{id},{name},{quantity}";
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
        public void Close()
        {
            conn.Close();
        }
    }
}
