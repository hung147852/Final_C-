﻿using Final_C;
using FinalC;
using System;
using System.Collections.Generic;
using System.Linq;
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
                        //manager.Remove();
                        break;
                    case 5:
                        //manager.Export();
                        break;
                    case 6:
                        //manager.Import();
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
        //{
        //    Console.WriteLine("==== EMPLOYEE SEARCH ====");
        //    Console.Write("ENTER ID or NAME/EMAIL: ");
        //    Console.Write("Enter Employee ID: ");

        //    string key = Console.ReadLine();
        //    EmployeeDAL employeeDAL = new EmployeeDAL();
        //    Employee? employees = EmployeeManager.Find(key);
        //    if (employees != null)
        //    {
        //        Console.WriteLine("Current Full Name: " + employees.FullName);
        //        Console.Write("Enter New Full Name (Leave blank to keep current name): ");
        //        string newFullName = Console.ReadLine();

        //        Console.WriteLine("Current Email: " + employees.Email);
        //        Console.Write("Enter New Email (Leave blank to keep current email): ");
        //        string newEmail = Console.ReadLine();

        //        if (!string.IsNullOrEmpty(newFullName))
        //        {
        //            employees.FullName = newFullName;
        //        }

        //        if (!string.IsNullOrEmpty(newEmail))
        //        {
        //            employees.Email = newEmail;
        //        }

        //        int rowsAffected = employeeDAL.Update(employees);
        //        if (rowsAffected > 0)
        //        {
        //            Console.WriteLine("Employee updated successfully.");
        //        }
        //        else
        //        {
        //            Console.WriteLine("Failed to update employee.");
        //        }
        //    }
        //    else
        //    {
        //        Console.WriteLine("Employee not found.");
        //    }

        //}
        {
            EmployeeDAL employeeDAL = new EmployeeDAL();
            Console.Write("Enter employee ID or name: ");
            string key = Console.ReadLine();
            List<Employee> employees = manager.Find(key);
            if (employees.Count == 0)
            {
                Console.WriteLine("No employee found!");
                return;
            }
            Employee emp = employees[0];
            Console.WriteLine("Current email: " + emp.Email);
            Console.Write("Enter new email (leave blank to keep current): ");
            string newEmail = Console.ReadLine();
            if (!string.IsNullOrEmpty(newEmail))
                emp.Email = newEmail;
            Console.WriteLine("Current Password: " + emp.Password);
            Console.Write("Enter new password (leave blank to keep current): ");
            string newPassword = Console.ReadLine();
            if (!string.IsNullOrEmpty(newPassword))
                emp.Password = newPassword;
            Console.WriteLine("Current Role: " + emp.Role);
            Console.Write("Enter new role (leave blank to keep current): ");

            string key1 = Console.ReadLine();
            int newRole;
            if (!int.TryParse(key, out newRole))
            {
                emp.Role = (EmployeeRole)newRole;
            }
            else
            {
                Console.WriteLine("Giá trị nhập vào không hợp lệ. Vui lòng nhập một số nguyên.");
            }
            // Add code to update password and role here

            employeeDAL.Update(emp);
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
            PrintFindScreen(); 
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
                string uname = Console.ReadLine();
                Console.WriteLine("Password: ");
                string pwd = Console.ReadLine();
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
    }
    
}