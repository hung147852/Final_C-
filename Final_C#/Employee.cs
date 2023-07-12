using Final_C;
using System;
namespace Final_C
{

	public class Employee
	{
        public int Id { get; set; }
        public string User { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public EmployeeRole Role { get; set; } //Manager - Users


        public Employee()
        {
        }

        public Employee(int id, string user, string fullname, string email, string password, EmployeeRole role)
        {
            Id = id;
            User = user;
            FullName = fullname;
            Email = email;
            Password = password;
            Role = role;
        }

        public override string ToString()
        {
            return Id + " | " + User + " | " + " | " + FullName + " | " + Email + " | " + Role + " | ";
        }
    }
}

