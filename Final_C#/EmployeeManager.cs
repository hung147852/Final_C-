﻿using Final_C;
using FinalC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Final_C_;

namespace Final_C
{
    public class EmployeeManager
    {
        EmployeeDAL employeeDAL;
        //public string GetCurrentEmployeeUsername()
        //{
        //    return CurrentEmployee?.User;
        //}
        public EmployeeManager() 
        {
            employeeDAL = new EmployeeDAL();
        }
        public Employee? Login(string username, string password)
        {
            Console.WriteLine(username + " " + password);
            //string hashedPassword = Utils.Hash(password, "sha512");
            //return employeeDAL.SelectByUsernameAndPassword(username, password);
            return employeeDAL.SelectByUsernameAndPassword(username, password);
        }
        public List<Employee> Find (string key) 
        {
            int id;
            bool found = int.TryParse(key, out id);
            if (found)
            {
                List<Employee>employees = new List<Employee>();
                Employee? employee = employeeDAL.SelectById(id);
                employees.Add(employee);
                return employees;
            } else
            {
                return employeeDAL.SelectByKey(key);
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
                    string user = tokens[0].Trim();
                    string fullname = tokens[1].Trim();
                    string email = tokens[2].Trim();
                    string password = tokens[3].Trim();
                    EmployeeRole role = (EmployeeRole)Convert.ToInt32(tokens[4]);
                    employeeDAL.Insert(new Employee(0, user, fullname, email, password, role));
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
        public List<string> ExportEmp()
        {
            return employeeDAL.Export();
        }
        public int AddNew(Employee emp)
        {
            return employeeDAL.Insert(emp);
        }
        public int Remove(Employee emp)
        {
            return employeeDAL.Delete(emp.Id);
        }
        public int Update(Employee emp)
        {
            return employeeDAL.Update(emp);
        }
        public List<Employee> GetAllEmployees()
        {
            return employeeDAL.SelectAll();
        }
        //public bool ChangePassword(string username, string currentPassword, string newPassword)
        //{
        //    Employee employee = employeeDAL.SelectByUsernameAndPassword(username, currentPassword);
        //    if (employee != null)
        //    {
        //        int rowsAffected = employeeDAL.ChangePassword(username, currentPassword, newPassword);
        //        return rowsAffected > 0;
        //    }
        //    return false;
        //}
        public bool ChangePassword(string username, string currentPassword, string newPassword)
        {
            string hashedCurrentPassword = Utils.Hash(currentPassword, "sha512");
            string hashedNewPassword = Utils.Hash(newPassword, "sha512");

            return employeeDAL.ChangePassword(username, hashedCurrentPassword, hashedNewPassword);
        }
        //public void EmployeeDALCls()
        //{
        //    employeeDAL.Close();
        //}
    }
}
