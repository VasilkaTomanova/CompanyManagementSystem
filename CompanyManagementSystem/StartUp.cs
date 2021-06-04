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
            //1.Create data base
            CompanyManagementSystemContext context = new CompanyManagementSystemContext();
            //context.Database.EnsureDeleted();
            //context.Database.EnsureCreated();
            //CreateDataBase cb = new CreateDataBase();
            //cb.CreateDatabase(context);


            //2. Entry in system 
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


            //3. In own profile - do something while reading commands from console
            Console.WriteLine($"Welcome, {currentEmployee.FirstName} {currentEmployee.LastName}!");
            while (true)
            {
                //read commmand
                Console.WriteLine("For your own materials: 1, for materials of other people: 2, to see the list of your colleagues: 3, for exit: 4");
                string command = Console.ReadLine();

                if (command == "1")
                {
                    // If you are in this case you have all permission to do everything to these materials, because they are yours                
                    CommandsInYourOnwMaterials(context, currentEmployee);
                    Console.WriteLine();
                }
                else if (command == "2")
                {
                    // If you are in this case there is checking the level of permision of materials
                    ComandsInOthersMaterials(context, currentEmployee);
                    Console.WriteLine();
                }
                else if (command.ToLower() == "3")
                {
                    //In this case you look your colleagues
                    CommandsForColleagues(context, currentEmployee);
                }
                else if (command.ToLower() == "4")
                {
                    // If we have web app we must "delete" the current client information, he still could be in the app
                    // but withour specific permisions
                    name = "";
                    pass = "";
                    Console.WriteLine();
                    break;
                }
                else
                {
                    Console.WriteLine("Your choise is invalid! Try again!");
                    Console.WriteLine();
                }



            } // end while
            Console.WriteLine("Bye!");


        } // end main

        public static CompanyManagementSystemContext CommandsInYourOnwMaterials
            (CompanyManagementSystemContext context, Employee currentEmployee)
        {
            List<Material> myMaterials = context.Materials.Where(m => m.AuthorId == currentEmployee.Id).ToList();

            if (myMaterials.Count != 0)
            {
                Console.WriteLine($"You have {myMaterials.Count} materials and they are:");
                Console.WriteLine(string.Join(", ", myMaterials.Select(m => m.Title)));
                Console.WriteLine("If you wannt change a material, write the title:");
                string currentTitleOfMaterialToChnage = Console.ReadLine();
                Material currentMaterialToChange = myMaterials
                                                .FirstOrDefault(m => m.Title == currentTitleOfMaterialToChnage);
                if (currentMaterialToChange != null)
                {
                    Console.WriteLine("For change the title of this material push 1, for the access level 2");
                    string changeCommand = Console.ReadLine();
                    if (changeCommand == "1")
                    {
                        Console.WriteLine("Write the new title of your material:");
                        string newNameOfMaterial = Console.ReadLine();
                        currentMaterialToChange.Title = newNameOfMaterial;
                        Console.WriteLine
                            ($"You successfully change the name of {currentTitleOfMaterialToChnage} to this new name {currentMaterialToChange.Title}");
                    }
                    else if (changeCommand == "2")
                    {
                        Console.WriteLine("Write the new protection level: private, public, another");
                        string newLevelOfprotection = Console.ReadLine();
                        Access newAcces = (Access)Enum.Parse(typeof(Access), newLevelOfprotection, true);
                        switch (newLevelOfprotection.ToLower())
                        {
                            case "private":
                                currentMaterialToChange.Access = newAcces;
                                break;
                            case "public":
                                currentMaterialToChange.Access = newAcces;
                                break;
                            //TODO logic here and in the class
                            case "another":
                                break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Choose a relevant number:");
                        //Be carefull with eventually ednless recursion
                        CommandsInYourOnwMaterials(context, currentEmployee);
                    }

                }
                else
                {
                    Console.WriteLine($"Sorry you have not a material with name {currentTitleOfMaterialToChnage}.");
                }
            }
            else
            {
                Console.WriteLine("Sorry, but you havn' t any materials!");
            }



            context.SaveChanges();
            // check if is possible to change type of this method void?
            return context;
        }

        public static CompanyManagementSystemContext ComandsInOthersMaterials
             (CompanyManagementSystemContext context, Employee currentEmployee)
        {
            List<Material> materialOfOtherPeople = context.Materials.Where(m => m.AuthorId != currentEmployee.Id).ToList();
            Console.WriteLine($"In our system we have these materials: {string.Join(", ", materialOfOtherPeople.Select(m => m.Title))}");
            Console.WriteLine("If you wanna see details of a material, please write its name:");
            string title = Console.ReadLine();

            Material materialLookingFor = materialOfOtherPeople.FirstOrDefault(m => m.Title == title);

            if (materialLookingFor != null)
            {
                if (materialLookingFor.Access.ToString() == "Public")
                {
                    Console.WriteLine("This document is public. You can manage it:");
                    Console.WriteLine($"Details: Title {materialLookingFor.Title} with Author {materialLookingFor.Author.FirstName} {materialLookingFor.Author.LastName} and address {materialLookingFor.Url}");
                    Console.WriteLine("For change title press 1, for url address press 2:");
                    string command = Console.ReadLine();
                    if (command == "1")
                    {
                        Console.WriteLine("PLease enter the new title of this document:");
                        string newNameOfDoc = Console.ReadLine();
                        materialLookingFor.Title = newNameOfDoc;
                        Console.WriteLine($"You change the title ot {title} with this one {materialLookingFor.Title}");
                    }
                    else if (command == "2")
                    {
                        Console.WriteLine("PLease enter the new url of this document:");
                        string newAddressOfDoc = Console.ReadLine();
                        materialLookingFor.Url = newAddressOfDoc;
                        Console.WriteLine($"You change the url to {materialLookingFor.Url}");
                    }
                    else
                    {
                        Console.WriteLine("Invalid command");
                    }
                }
                else if (materialLookingFor.Access.ToString() == "Private")
                {
                    Console.WriteLine("Sorry, this document is private, and you can see only the title.");
                }
                else
                {
                    Console.WriteLine("Still working on this feature \"manage documents\"");
                    //TODO logic if myId is in the list with eligible id
                }
            }
            else
            {
                Console.WriteLine($"We have not a material with name {title}");
            }
            context.SaveChanges();
            return context;
        }

        public static CompanyManagementSystemContext CommandsForColleagues
             (CompanyManagementSystemContext context, Employee currentEmployee)
        {
            List<Employee> colleagues = context.Employees.Where(e => e.Id != currentEmployee.Id).ToList();
            Console.WriteLine($"Your collegues are: {string.Join(", ", colleagues.Select(c => c.Username))}");
            Console.WriteLine("If you wanna make some changes type the username of your collegaues:");
            string usernameOfColleagueToChange = Console.ReadLine();
            Employee colleagueToChange = colleagues.FirstOrDefault(c => c.Username == usernameOfColleagueToChange);

            if (colleagueToChange != null && currentEmployee.Rank > colleagueToChange.Rank)
            {
                Console.WriteLine($"Information about user with username {usernameOfColleagueToChange}:");
                Console.WriteLine($"Firstname: {colleagueToChange.FirstName}, Lastname: {colleagueToChange.LastName}, Salary: {colleagueToChange.Salary}");
                Console.WriteLine("To change salary press 1, to fire press 2");
                string command = Console.ReadLine();
                if (command == "1")
                {
                    Console.WriteLine($"Plese enter the new salary of {colleagueToChange.FirstName} {colleagueToChange.LastName}:");
                    decimal newSalary = decimal.Parse(Console.ReadLine());
                    if (newSalary < colleagueToChange.Salary)
                    {
                        Console.WriteLine("Are serious? Please enter salary bigger than current! :D");
                        Console.WriteLine("Plese enter the new salary of this employee:");
                        newSalary = decimal.Parse(Console.ReadLine());
                    }
                    colleagueToChange.ChangeSalary(newSalary);
                    Console.WriteLine($"Super! ");
                }
                else if (command == "2")
                {
                    colleagueToChange.IsActiveEmployee = false;
                    Console.WriteLine($"We just have fired {colleagueToChange.FirstName} {colleagueToChange.LastName}");
                    // Maybe delete it from DB or ?
                }
                else
                {
                    Console.WriteLine("Invalid Command");
                }

            }
            else if (colleagueToChange != null && currentEmployee.Rank <= colleagueToChange.Rank)
            {
                Console.WriteLine("Sorry but you have not permission to make some changes on this person");
            }
            else
            {
                Console.WriteLine($"In our database there isn't an user with name {usernameOfColleagueToChange}!");
            }
            context.SaveChanges();
            return context;
        }
    }
}
