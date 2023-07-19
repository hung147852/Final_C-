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
        private int importedDeviceCount;
        public DeviceManager() 
        {
            deviceDAL = new DeviceDAL();
            importedDeviceCount = 0;
        }

        public List<Device> FindDV (string key) 
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
        //public void ImportDV(string filePath)
        //{
        //    StreamReader reader = new StreamReader(filePath);
        //    try
        //    {
        //        string line;
        //        while ((line = reader.ReadLine()) != null)
        //        {
        //            string[] tokens = line.Split(',');
        //            string name = tokens[0].Trim();
        //            int quantity = int.Parse(tokens[1].Trim());
        //            deviceDAL.Insert(new Device(0, name, quantity));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex);
        //    }
        //    finally
        //    {
        //        reader.Close();
        //    }
        //}

        public void ImportDV(string filePath)
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
                    importedDeviceCount++; // Tăng số lượng thiết bị được nhập thành công
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

        public int GetImportedDeviceCount()
        {
            return importedDeviceCount;
        }

        public List<string> ExportDV()
        {
            return deviceDAL.Export();
        }
        public int AddNewDV(Device device)
        {
            return deviceDAL.Insert(device);
        }
        public int RemoveDV(Device device)
        {
            return deviceDAL.Delete(device.Dname);
        }
        public int UpdateDV(Device device)
        {
            return deviceDAL.Update(device);
        }
        public List<Device> GetAllDevice()
        {
            return deviceDAL.SelectAll();
        }
        public Device GetDeviceByName(string deviceName)
        {
            return deviceDAL.SelectByName(deviceName);
        }
        public Device GetDeviceById(int deviceId)
        {
            List<Device> devices = GetAllDevice();
            Device device = devices.FirstOrDefault(d => d.Id == deviceId);
            return device;
        }
    
    }
}
