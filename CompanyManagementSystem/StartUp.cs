using CompanyManagementSystem.Data;
using System;
using System.Linq;

namespace CompanyManagementSystem
{
  public  class StartUp
    {
        public static void Main(string[] args)
        {

            var context = new CompanyManagementSystemContext();
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();


            //Console.Write("Enter your name:");
            //string name = Console.ReadLine();

            //var empl = context.Employees.ToList();
            //if (empl.Any(e=>e.Username == name))
            //{

            //}
            //Console.Write("Enter your pass:");


        }
    }
}
