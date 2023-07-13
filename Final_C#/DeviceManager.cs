using Final_C;
using FinalC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Final_C
{
    public class DeviceManager
    {
        DeviceDAL deviceDAL;
        public DeviceManager() 
        {
            deviceDAL = new DeviceDAL();
        }
        //public Employee? Login(string username, string password)
        //{
        //    Console.Write("Day la Login");
        //    //select DB de check
        //    return employeeDAL.SelectByUsernameAndPassword(username, password);
        //}
        public List<Device> Find (string key) 
        {
            int id;
            bool found = int.TryParse(key, out id);
            if (found)
            {
                List<Device>devices = new List<Device>();
                Device? device = deviceDAL.SelectById(id);
                devices.Add(device);
                return devices;
            } else
            {
                return deviceDAL.SelectByKey(key);
            }
        }
        public void Import(string filePath)
        {
            StreamReader reader = new StreamReader(filePath);
            try
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] tokens = line.Split(',');
                    string name = tokens[0].Trim();
                    int quantity = int.Parse(tokens[1].Trim());
                    deviceDAL.Insert(new Device(0, name, quantity));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                reader.Close();
            }
        }
        public List<string> Export()
        {
            return deviceDAL.Export();
        }
        public int AddNew(Device device)
        {
            return deviceDAL.Insert(device);
        }
        public int Remove(Device device)
        {
            return deviceDAL.Delete(device.Dname);
        }
        public int Update(Device device)
        {
            return deviceDAL.Update(device);
        }
        public List<Device> GetAllEmployees()
        {
            return deviceDAL.SelectAll();
        }
    }
}
