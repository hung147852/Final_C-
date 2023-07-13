﻿using Final_C;
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
        public Screen() 
        {
            manager = new EmployeeManager();
        } 
        public void PrintManagerScreen()
        {
            int selected = 0;

            do
            {
                Console.WriteLine("==== EMPLOYEE MANAGER ====");
                Console.WriteLine("\t1.Search Employee by Name or EmpNo");
                Console.WriteLine("\t2.Add New Employee");
                Console.WriteLine("\t3.Update Employee");
                Console.WriteLine("\t4.Remove Employee");
                Console.WriteLine("\t5.Export Data");
                Console.WriteLine("\t6.Import Data");
                Console.WriteLine("\t7.Exit");
                Console.Write("Select (1-7): ");
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
                        Console.WriteLine("------------END----------");
                        break;
                    default:
                        Console.WriteLine("Invalid");
                        break;
                }
            } while (selected != 7);
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
                        Console.Write("Enter New Password: ");
                        string newPassword = Console.ReadLine();
                        employee.Password = newPassword;
                    }

                    Console.Write("Enter New Email (Leave blank to keep current email): ");
                    string newEmail = Console.ReadLine();
                    if (!string.IsNullOrEmpty(newEmail))
                    {
                        employee.Email = newEmail;
                    }

                    Console.Write("Enter New Role (Leave blank to keep current role): ");
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
        public void PrintUserScreen()
        {
            int selected = 0;

            do
            {
                Console.WriteLine("==== OPTIONS OF EMPLOYEE USER ====");
                Console.WriteLine("\t1.Search Employee by Name or EmpNo");
                Console.WriteLine("\t2.Search Device by Name");
                Console.WriteLine("\t3.Update Device");
                Console.WriteLine("\t4.Remove Device");
                Console.WriteLine("\t5.Export Data");
                Console.WriteLine("\t6.Import Data");
                Console.WriteLine("\t7.Exit");
                Console.Write("Select (1-7): ");
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
                        Console.WriteLine("------------END----------");
                        break;
                    default:
                        Console.WriteLine("Invalid");
                        break;
                }
            } while (selected != 7);
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
                string? pwd = Console.ReadLine();
                // Check Login
                Employee? employee = manager.Login(uname, pwd);
                if (employee == null)
                {
                    Console.WriteLine("Dang nhap khong thanh cong! Kiem tra Username hoac Password");
                }else
                {
                    checkLogin = true;
                    Console.WriteLine("Dang nhap thanh cong!");
                    if(employee.Role == EmployeeRole.MANAGER)
                    {
                        PrintManagerScreen();
                    }else if(employee.Role == EmployeeRole.USER)
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

            List<string> csvData = manager.Export();

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
        private void PrintChangePasswordScreen()
        {
            Console.WriteLine("==== CHANGE PASSWORD ====");
            Console.Write("Retype current user: ");
            string key = Console.ReadLine();
            Console.Write("Enter current password: ");
            string currentPassword = Console.ReadLine();
            Console.Write("Enter new password: ");
            string newPassword = Console.ReadLine();
            Console.Write("Confirm new password: ");
            string confirmNewPassword = Console.ReadLine();

            if (newPassword == confirmNewPassword)
            {
                bool passwordChanged = manager.ChangePassword(key, currentPassword, newPassword);
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
    }
}
