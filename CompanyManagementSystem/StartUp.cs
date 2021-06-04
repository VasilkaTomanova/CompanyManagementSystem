using CompanyManagementSystem.Data;
using CompanyManagementSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CompanyManagementSystem
{
    public class StartUp
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
                Console.WriteLine("For your materials: 1, for others materials: 2");
                string command = Console.ReadLine();

                if (command == "1")
                {
                    // If you are in this case you have all permission to do everything to these materials, because there yours                
                    CommandsInYourOnwMaterials(context, currentEmployee);
                }
                else if (command == "2")
                {
                    // If you are in this case you must check the level of permision of materials
                    //TODO
                }
                else if (command.ToLower() == "exit")
                {
                    name = "";
                    pass = "";
                    break;
                }
                else
                {
                    Console.WriteLine("Your choise is invalid! Try again!");
                }



            } // end while



        } // end main

        public static CompanyManagementSystemContext CommandsInYourOnwMaterials
            (CompanyManagementSystemContext context, Employee currentEmployee)
        {
            List<Material> myMaterials = context.Materials.Where(m => m.AuthorId == currentEmployee.Id).ToList();
            Console.WriteLine($"You have {myMaterials.Count} materials and they are:");
            Console.WriteLine(string.Join(", ", myMaterials.Select(m => m.Title)));
            Console.WriteLine("If you wannt change a material, write the title:");
            string currentTitleOfMaterialToChnage = Console.ReadLine();
            Material currentMaterialToChange = myMaterials
                                            .FirstOrDefault(m => m.Title == currentTitleOfMaterialToChnage);
            if (currentMaterialToChange != null)
            {
                Console.WriteLine("For change the title push 1, for the access level 2");
                string changeCommand = Console.ReadLine();
                if (changeCommand == "1")
                {
                    Console.WriteLine("Write the new title of your material");
                    string newNameOfMaterial = Console.ReadLine();
                    currentMaterialToChange.Title = newNameOfMaterial;
                    Console.WriteLine
                        ($"You successfully change the name of {currentTitleOfMaterialToChnage} to this new name {currentMaterialToChange.Title}");

                }
                else if (changeCommand == "2")
                {


                }
                else
                {

                }

            }
            else
            {
                Console.WriteLine("Sorry you have not a such material.");
            }
            // check if is possible to change type of this method void?
            return context;
        }




    }
}
