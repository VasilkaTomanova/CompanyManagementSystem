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



            //Entry in system 
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


            //In own profile
            Console.WriteLine($"Welcome, {currentEmployee.FirstName} {currentEmployee.LastName}!");
            while (true)
            {
                //read commmand
                Console.WriteLine("For your materials: 1, for subordinates 2");
                string command = Console.ReadLine();

                if(command == "1")
                {
                    List<Material> myMaterials = context.Materials.Where(m => m.AuthorId == currentEmployee.Id).ToList();
                    Console.WriteLine($"You have {myMaterials.Count} materials and they are:");
                    Console.WriteLine(string.Join(", ", myMaterials.Select(m=>m.Title)));
                    Console.WriteLine("If you wannt change a material, write the title:");
                    //TODO
                }
                else if (command == "2")
                {
                    //TODO
                }
                else
                {
                    Console.WriteLine("Your choise is invalid! Try again!");
                }



            } // end while



        } // end main
    }
}
