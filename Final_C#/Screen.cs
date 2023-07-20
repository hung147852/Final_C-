using Final_C;
using Final_C_;
using FinalC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ObjectiveC;
using System.Text;
using System.Threading.Tasks;

namespace Final_C
{
    public class Screen
    {
        EmployeeManager manager;
        DeviceManager deviceManager;
        private Employee loggedInEmployee;
        public Screen()
        {
            manager = new EmployeeManager();
            deviceManager = new DeviceManager();
        }
        public void PrintManagerScreen()
        {
            int selected = 0;

            do
            {
                Console.WriteLine("==== MANAGER SCREEN ====");
                Console.WriteLine("\t1.Search Employee by Name or EmpNo");
                Console.WriteLine("\t2.Add New Employee");
                Console.WriteLine("\t3.Update Employee");
                Console.WriteLine("\t4.Remove Employee");
                Console.WriteLine("\t5.Export Data");
                Console.WriteLine("\t6.Import Data");
                Console.WriteLine("\t7.Search Device by Name or Id");
                Console.WriteLine("\t8.Add New Device");
                Console.WriteLine("\t9.Update Device");
                Console.WriteLine("\t10.Remove Device");
                Console.WriteLine("\t11.Export Device Data");
                Console.WriteLine("\t12.Import Device Data");
                Console.WriteLine("\t13.Change employee password");
                Console.WriteLine("\t14.Exit");
                Console.Write("Select (1-14): ");
                selected = Convert.ToInt16(Console.ReadLine());


                switch (selected)
                {
                    case 1:
                        PrintFindScreen();
                        break;
                    case 2:
                        PrintAddScreen();
                        break;
                    case 3:
                        PrintUpdateScreen();
                        break;
                    case 4:
                        PrintDeleteScreen();
                        break;
                    case 5:
                        PrintExportScreen();
                        break;
                    case 6:
                        PrintImportScreen();
                        break;
                    case 7:
                        PrintSearchDeviceScreen();
                        break;
                    case 8:
                        PrintAddDeviceScreen();
                        break;
                    case 9:
                        PrintUpdateDeviceScreen();
                        break;
                    case 10:
                        PrintRemoveDeviceScreen();
                        break;
                    case 11:
                        PrintExportDeviceScreen();
                        break;
                    case 12:
                        PrintImportDeviceScreen();
                        break;
                    case 13:
                        PrintChangePasswordScreen();
                        break;
                    case 14:
                        Console.WriteLine("------------END----------");
                        break;
                    default:
                        Console.WriteLine("Invalid");
                        break;
                }
            } while (selected != 14);
        }
        public void PrintUserScreen()
        {
            int selected = 0;

            do
            {
                Console.WriteLine("==== OPTIONS OF EMPLOYEE USER ====");
                Console.WriteLine("\t1.Search Employee by Name or EmpNo");
                Console.WriteLine("\t2.Search Device by Name");
                Console.WriteLine("\t3.Add Device");
                Console.WriteLine("\t4.Update Device");
                Console.WriteLine("\t5.Remove Device");
                Console.WriteLine("\t6.Export Data");
                Console.WriteLine("\t7.Import Data");
                Console.WriteLine("\t8.Change your user password");
                Console.WriteLine("\t9.Exit");
                Console.Write("Select (1-9): ");
                selected = Convert.ToInt16(Console.ReadLine());


                switch (selected)
                {
                    case 1:
                        PrintFindScreen();
                        break;
                    case 2:
                        PrintSearchDeviceScreen();
                        break;
                    case 3:
                        PrintAddDeviceScreen();
                        break;
                    case 4:
                        PrintUpdateDeviceScreen();
                        break;
                    case 5:
                        PrintRemoveDeviceScreen();
                        break;
                    case 6:
                        PrintExportScreen();
                        break;
                    case 7:
                        PrintImportDeviceScreen();
                        break;
                    case 8:
                        PrintChangePasswordScreen();
                        break;
                    case 9:
                        Console.WriteLine("------------END----------");
                        break;
                    default:
                        Console.WriteLine("Invalid");
                        break;
                }
            } while (selected != 9);
        }

        private void PrintUpdateScreen()
        {
            Console.WriteLine("==== UPDATE EMPLOYEE ====");
            Console.Write("Enter Employee ID or Name: ");
            string key = Console.ReadLine();
            EmployeeDAL employeeDAL = new EmployeeDAL();
            EmployeeManager employeeManager = new EmployeeManager();
            List<Employee> employees = employeeManager.Find(key);

            if (employees.Count > 0)
            {
                if (employees.Count == 1)
                {
                    Employee employee = employees[0];
                    Console.WriteLine("Employee found:");
                    Console.WriteLine("ID: " + employee.Id);
                    Console.WriteLine("USER: " + employee.User);
                    Console.WriteLine("Full Name: " + employee.FullName);
                    Console.WriteLine("Email: " + employee.Email);
                    Console.WriteLine("Role: " + employee.Role);

                    Console.WriteLine("Current Password: " + employee.Password);
                    Console.Write("Enter Current Password to update (Leave blank to keep current password): ");
                    string currentPassword = Console.ReadLine();

                    if (!string.IsNullOrEmpty(currentPassword) && currentPassword == employee.Password)
                    {
                        //Console.Write("Enter New Password: ");
                        //string newPassword = Console.ReadLine();
                        //employee.Password = newPassword;
                        Console.Write("Enter New Password: ");
                        string newPassword = Console.ReadLine();
                        if (!string.IsNullOrEmpty(newPassword))
                        {
                            string hasedPassword = Utils.Hash(newPassword, "sha512");
                            employee.Password = hasedPassword;
                        }

                    }

                    Console.Write("Enter New Email (Leave blank to keep current email): ");
                    string newEmail = Console.ReadLine();
                    if (!string.IsNullOrEmpty(newEmail))
                    {
                        employee.Email = newEmail;
                    }

                    Console.Write("Enter New Role '0' if want manager or '1' if user (Leave blank to keep current role): ");
                    string newRole = Console.ReadLine();
                    if (!string.IsNullOrEmpty(newRole))
                    {
                        if (Enum.TryParse(newRole, out EmployeeRole changeRole))
                        {
                            employee.Role = changeRole;
                        }
                        else
                        {
                            Console.WriteLine("Invalid role. Role remains unchanged.");
                        }
                    }

                    int rowsAffected = employeeDAL.Update(employee);
                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Employee updated successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Failed to update employee.");
                    }
                }
                else
                {
                    Console.WriteLine("Multiple employees found. Please provide a more specific search key.");
                }
            }
            else
            {
                Console.WriteLine("Employee not found.");
            }
        }

        private void PrintAddScreen()
        {
            Console.WriteLine("==== EMPLOYEE SEARCH ====");
            Console.Write("User: ");
            String user = Console.ReadLine();
            Console.Write("FullName: ");
            String fullname = Console.ReadLine();
            Console.Write("Email: ");
            String email = Console.ReadLine();
            Console.Write("Is Manger y/n:");
            EmployeeRole role = EmployeeRole.USER;
            if (Console.ReadLine().ToUpper() == "Y")
            {
                role = EmployeeRole.MANAGER;
            }
            Console.Write("Password:");
            String password = Console.ReadLine();
            manager.AddNew(new Employee(0, user, fullname, email, password, role));
        }

        private void PrintFindScreen()
        {
            Console.WriteLine("==== EMPLOYEE SEARCH ====");
            Console.Write("ENTER ID or NAME/EMAIL: ");
            string key = Console.ReadLine();

            List<Employee> employees = manager.Find(key);
            if (employees.Count > 0)
            {
                DisplayEmployees(employees);
            }
            else
            {
                Console.WriteLine("Data not found!");
            }
        }
        
        private void DisplayEmployees(List<Employee> employees)
        {
            foreach (Employee emp in employees)
            {
                Console.WriteLine(emp);
            }
        }
        public void PrintLoginScreen()
        {
            bool checkLogin = false;
            do
            {
                Console.WriteLine("==== EMPLOYEE MANAGE ====");
                Console.WriteLine("====       LOGIN     ====");
                Console.WriteLine("Username: ");
                string? uname = Console.ReadLine();
                Console.WriteLine("Password: ");
                //string? pwd = Console.ReadLine();
                string? pwd = Utils.Hash(Console.ReadLine(), "sha512");
                Console.WriteLine("I LOVE YIU Screen");
                // Check Login
                Employee? employee = manager.Login(uname, pwd);
                if (employee == null)
                {
                    Console.WriteLine("Dang nhap khong thanh cong! Kiem tra Username hoac Password");
                }
                else
                {
                    checkLogin = true;
                    loggedInEmployee = employee;
                    Console.WriteLine("Dang nhap thanh cong!");
                    if (employee.Role == EmployeeRole.MANAGER)
                    {
                        PrintManagerScreen();
                    }
                    else if (employee.Role == EmployeeRole.USER)
                    {
                        PrintUserScreen();
                    }
                }
            } while (!checkLogin);
        }
        private void PrintDeleteScreen()
        {
            Console.WriteLine("==== EMPLOYEE DELETE ====");
            Console.WriteLine("==== ALL EMPLOYEE ====");
            List<Employee> employees = manager.GetAllEmployees();
            DisplayEmployees(employees);

            Console.Write("ENTER ID: ");
            int id = Convert.ToInt32(Console.ReadLine());

            Employee emp = new Employee { Id = id };
            int rowsAffected = manager.Remove(emp);
            if (rowsAffected > 0)
            {
                Console.WriteLine("Employee deleted successfully.");
            }
            else
            {
                Console.WriteLine("Employee not found or failed to delete.");
            }
        }
        private void PrintExportScreen()
        {
            Console.WriteLine("==== EXPORT DATA ====");
            Console.Write("Enter file path to export data: ");
            string filePath = Console.ReadLine();

            List<string> csvData = manager.ExportEmp();

            // Ghi dữ liệu CSV vào file
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    foreach (string csvLine in csvData)
                    {
                        writer.WriteLine(csvLine);
                    }
                }

                Console.WriteLine("Data exported successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occurred while exporting data: " + ex.Message);
            }

        }

        private void PrintImportScreen()
        {
            Console.WriteLine("==== IMPORT DATA ====");
            Console.Write("Enter file path to import data: ");
            string filePath = Console.ReadLine();

            manager.Import(filePath);
        }
        //private void PrintChangePasswordScreen()
        //{
        //    Console.WriteLine("==== CHANGE PASSWORD ====");
        //    Console.Write("Retype current user: ");
        //    string key = Console.ReadLine();
        //    Console.Write("Enter current password: ");
        //    string currentPassword = Console.ReadLine();
        //    Console.Write("Enter new password: ");
        //    string newPassword = Console.ReadLine();
        //    Console.Write("Confirm new password: ");
        //    string confirmNewPassword = Console.ReadLine();

        //    if (newPassword == confirmNewPassword)
        //    {
        //        bool passwordChanged = manager.ChangePassword(loggedInEmployee.User, currentPassword, newPassword);
        //        if (passwordChanged)
        //        {
        //            Console.WriteLine("Password changed successfully.");
        //        }
        //        else
        //        {
        //            Console.WriteLine("Incorrect current password.");
        //        }
        //    }
        //    else
        //    {
        //        Console.WriteLine("New password and confirm password do not match.");
        //    }
        //}
        private void PrintChangePasswordScreen()
        {
            Console.WriteLine("==== CHANGE PASSWORD ====");
            Console.Write("Retype current user: ");
            string key = Console.ReadLine();
            Console.Write("Enter current password: ");
            string currentPassword = Utils.Hash(Console.ReadLine(), "sha512");
            Console.Write("Enter new password: ");
            string newPassword = Utils.Hash(Console.ReadLine(), "sha512");
            Console.Write("Confirm new password: ");
            string confirmNewPassword = Utils.Hash(Console.ReadLine(), "sha512");

            if (newPassword == confirmNewPassword)
            {
                // Gọi hàm ChangePassword của bạn và kiểm tra giá trị trả về
                bool passwordChanged = manager.ChangePassword(loggedInEmployee.User, currentPassword, newPassword);
                if (passwordChanged)
                {
                    Console.WriteLine("Password changed successfully.");
                }
                else
                {
                    Console.WriteLine("Incorrect current password.");
                }
            }
            else
            {
                Console.WriteLine("New password and confirm password do not match.");
            }
        }
        private void PrintSearchDeviceScreen()
        {
            Console.WriteLine("==== SEARCH DEVICE ====");
            Console.Write("Enter device name or keyword: ");
            string searchKey = Console.ReadLine();

            // Gọi phương thức tìm kiếm thiết bị từ lớp DeviceManager
            List<Device> devices = deviceManager.FindDV(searchKey);

            // In danh sách thiết bị trước khi thực hiện tìm kiếm
            PrintDeviceList(devices);
        }
        private void PrintAddDeviceScreen()
        {
            Console.WriteLine("==== ADD DEVICE ====");
            Console.Write("Name: ");
            String name = Console.ReadLine();
            Console.Write("Quantity: ");
            int quantity = Convert.ToInt32(Console.ReadLine());
            deviceManager.AddNewDV(new Device(0, name, quantity));
        }
        private void PrintUpdateDeviceScreen()
        {
            Console.WriteLine("==== UPDATE DEVICE ====");
            Console.WriteLine("==== LIST UPDATE DEVICE ====");
            List<Device> devices = deviceManager.GetAllDevice();
            PrintDeviceList(devices);
            Console.Write("Enter device name: ");
            string deviceName = Console.ReadLine();

            // Kiểm tra xem thiết bị có tồn tại hay không
            Device device = deviceManager.GetDeviceByName(deviceName);
            if (device != null)
            {
                Console.WriteLine("Device found:");
                Console.WriteLine("Name: " + device.Dname);
                Console.WriteLine("Description: " + device.Quantity);


                // Nhập thông tin cập nhật
                Console.Write("Enter new name (Leave blank to keep current name): ");
                string newName = Console.ReadLine();
                Console.Write("Enter new quantity (Leave blank to keep current description): ");
                string newQuantityinput = Console.ReadLine();

                int newQuantity;
                if (int.TryParse(newQuantityinput, out newQuantity))
                {
                    // Thực hiện cập nhật thông tin thiết bị
                    device.Dname = string.IsNullOrEmpty(newName) ? device.Dname : newName;
                    device.Quantity = newQuantity;

                    int rowsAffected = deviceManager.UpdateDV(device);
                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Device updated successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Failed to update device.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid quantity. Device update cancelled.");
                }
            }
        }
        private void PrintRemoveDeviceScreen()
        {
            Console.WriteLine("==== REMOVE DEVICE ====");
            List<Device> devices = deviceManager.GetAllDevice();
            PrintDeviceList(devices);
            Console.Write("Enter device ID: ");
            int deviceId = Convert.ToInt32(Console.ReadLine());

            // Kiểm tra xem thiết bị có tồn tại hay không
            Device device = deviceManager.GetDeviceById(deviceId);
            if (device != null)
            {
                int rowsAffected = deviceManager.RemoveDV(device);
                if (rowsAffected > 0)
                {
                    Console.WriteLine("Device removed successfully.");
                }
                else
                {
                    Console.WriteLine("Failed to remove device.");
                }
            }
            else
            {
                Console.WriteLine("Device not found.");
            }
        }
        private void PrintDeviceList(List<Device> devices)
        {
            Console.WriteLine("==== DEVICE LIST ====");
            if (devices.Count > 0)
            {
                foreach (Device device in devices)
                {
                    Console.WriteLine("ID: " + device.Id);
                    Console.WriteLine("Device Name: " + device.Dname);
                    Console.WriteLine("Quantity: " + device.Quantity);
                    Console.WriteLine("----------------------");
                }
            }
            else
            {
                Console.WriteLine("No devices found.");
            }
        }
        private void PrintExportDeviceScreen()
        {
            Console.WriteLine("==== EXPORT DEVICE DATA ====");
            Console.Write("Enter file path to export data: ");
            string filePath = Console.ReadLine();

            // Gọi phương thức xuất dữ liệu thiết bị từ lớp DeviceManager
            List<string> csvData = deviceManager.ExportDV();

            // Ghi dữ liệu CSV vào file
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    foreach (string csvLine in csvData)
                    {
                        writer.WriteLine(csvLine);
                    }
                }

                Console.WriteLine("Device data exported successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occurred while exporting device data: " + ex.Message);
            }
        }

        private void PrintImportDeviceScreen()
        {
            Console.WriteLine("==== IMPORT DEVICE DATA ====");
            Console.Write("Enter file path to import data: ");
            string filePath = Console.ReadLine();

            deviceManager.ImportDV(filePath);

            // Lấy số lượng thiết bị được nhập thành công từ lớp DeviceManager
            int importedDeviceCount = deviceManager.GetImportedDeviceCount();

            Console.WriteLine(importedDeviceCount + " devices imported.");
        }
        public void PrintImportDeviceScreennewest()
        {
            Console.WriteLine("==== IMPORT DEVICE DATA ====");
            Console.Write("Enter file path to import data: ");
            string filePath = Console.ReadLine();

            deviceManager.ImportDV(filePath);

            int importedDeviceCount = deviceManager.GetImportedDeviceCount();

            Console.WriteLine(importedDeviceCount + " devices imported.");
        }
    }
}
