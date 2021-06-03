using CompanyManagementSystem.Data;
using CompanyManagementSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CompanyManagementSystem
{
  public  class StartUp
    {
        public static void Main(string[] args)
        {

            CompanyManagementSystemContext context = new CompanyManagementSystemContext();
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();


            Console.Write("Enter your username:");
            string name = Console.ReadLine();

            List<Employee> employyes = context.Employees.ToList();
            Employee currentEmployee = employyes.FirstOrDefault(e => e.Username == name);
            if (currentEmployee == null)
            {
                Console.WriteLine("Your username dosn't exist in our system. Sorry, try again later!");
                return;
            }

            Console.Write("Enter your pass:");
            string pass = Console.ReadLine();

            if (currentEmployee.Password != pass)
            {
                Console.WriteLine("Your username and password are diffrennt. Sorry, try again later!");
                return;
            }

            while (true)
            {
                //read commmand
            }



        }
    }
}
