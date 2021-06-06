using CompanyManagementSystem.Data;
using CompanyManagementSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyManagementSystem.Start
{
    public class Engine
    {

        public void Run(CompanyManagementSystemContext context)
        {
            Console.WriteLine("Welcome in our Company Managament system! If you alreday registered press 1, if you have NOT any registration press 2");
            string initialCommand = Console.ReadLine();
            if (initialCommand == "2")
            {
                //not registered
                bool successfullRegistration = RegistrationFrom(context);
                if (successfullRegistration == false)
                {
                    return;
                }
            }
            Employee currentEmployee = LoginInForm(context);
            if (currentEmployee == null)
            {
                Console.WriteLine("Your username or password are inconrrect. Sorry, try again later!");
                return;
            }

            //3. In own profile - do something while reading commands from console
            Console.WriteLine($"Welcome, {currentEmployee.FirstName} {currentEmployee.LastName}!");
            int counterToClearConsole = 0;
            while (true)
            {
                if (counterToClearConsole++ > 2)
                {
                    ClearTheConsole();
                    counterToClearConsole = 0;
                }
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
                    // but without specific permisions
                    currentEmployee = null;
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
        }

        private static void ClearTheConsole()
        {
            System.Threading.Thread.Sleep(1000);
            Console.WriteLine("Waiting...Disinfection...");
            System.Threading.Thread.Sleep(3000);
            Console.Clear();
            System.Threading.Thread.Sleep(1000);
        }

        public static Employee LoginInForm(CompanyManagementSystemContext context)
        {
            Console.Write("Enter your username:");
            string name = Console.ReadLine();
            List<Employee> employyes = context.Employees.ToList();
            Employee currentEmployee = employyes.FirstOrDefault(e => e.Username == name);
            if (currentEmployee == null)
            {
                return null;
            }
            Console.Write("Enter your pass:");
            string pass = Console.ReadLine();
            if (currentEmployee.Password != pass)
            {
                return null;
            }
            return currentEmployee;
        }

        public static bool RegistrationFrom(CompanyManagementSystemContext context)
        {
            Employee newEmployeeToRegister = new Employee();
            Console.WriteLine("Please enter your username:");
            int counterOfEligible = 3;
            bool thisUserNameExistInDatabase = false;
            string username = "";
            while (counterOfEligible-- > 0)
            {
                 username = Console.ReadLine();
                thisUserNameExistInDatabase = CheckThisUsernameExistInDatabase(context, username);
                if (thisUserNameExistInDatabase == true)
                {
                    Console.WriteLine("This username is NOT free! Try again");
                } 
                else
                {
                    thisUserNameExistInDatabase = false;
                }
            }

            if (thisUserNameExistInDatabase == true)
            {
                Console.WriteLine("Sorry you have try a lot ot times!");
                return false;
            }

            newEmployeeToRegister.Username = username;
            Console.WriteLine("Please enter your password:");
            string password = Console.ReadLine();
            newEmployeeToRegister.Password = password;
            Console.WriteLine("Please enter your firstname:");
            string firstName = Console.ReadLine();
            newEmployeeToRegister.FirstName = firstName;
            Console.WriteLine("Please enter your lastname:");
            string lastName = Console.ReadLine();
            newEmployeeToRegister.LastName = lastName;
            Console.WriteLine
                ($"Please choose your relevant position number:{Environment.NewLine}{string.Join(Environment.NewLine, context.Positions.Select(p => p.Id + " " + p.Name))}");
            int positionId = int.Parse(Console.ReadLine());
            newEmployeeToRegister.PositionId = positionId;
            context.Employees.Add(newEmployeeToRegister);
            context.SaveChanges();
            //Simulate web loading and connection to server/db :D :D :D
            SimulateDataLoading();
            return true;
        }


        private static bool CheckThisUsernameExistInDatabase(CompanyManagementSystemContext context, string currentUserNameToCheck)
        {
            if (context.Employees.Any(x=>x.Username.ToLower() == currentUserNameToCheck.ToLower()))
            {
                return true;
            }

            return false;
        }



        private static void SimulateDataLoading()
        {
            Console.WriteLine("Please wait to add your information to our database");
            System.Threading.Thread.Sleep(1000); Console.Write(".");
            System.Threading.Thread.Sleep(1000); Console.Write(".");
            System.Threading.Thread.Sleep(1000); Console.Write(".");
            System.Threading.Thread.Sleep(1000); Console.Write("Loading");
            System.Threading.Thread.Sleep(1000); Console.Write(".");
            System.Threading.Thread.Sleep(1000); Console.Write(".");
            System.Threading.Thread.Sleep(1000); Console.Write(".");
            System.Threading.Thread.Sleep(500);
            Console.Clear();
            Console.WriteLine("You was registered successfully in our database!");
            Console.WriteLine("You will redirected to login form and you must input your data!");
            System.Threading.Thread.Sleep(4500);
            Console.Clear();
        }

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
                    ChangeYourOwnMaterial(context, currentEmployee, currentTitleOfMaterialToChnage, currentMaterialToChange);
                }
                else
                {
                    Console.WriteLine($"Sorry you have not a material with name {currentTitleOfMaterialToChnage}.");
                }
            }
            else
            {
                Console.WriteLine("Sorry, but you havn' t any materials! Do you want add one?");
                AddNewMaterial(context, currentEmployee);
            }
            context.SaveChanges();
            // check if is possible to change type of this method void?
            return context;
        }

        private static void ChangeYourOwnMaterial(CompanyManagementSystemContext context, Employee currentEmployee, string currentTitleOfMaterialToChnage, Material currentMaterialToChange)
        {
            Console.WriteLine("For change the title of this material push 1, for the access level 2, for delete 3");
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
                ChangeAssessOfMaterial(context, currentEmployee, currentMaterialToChange);
            }
            else if (changeCommand == "3")
            {
                context.Materials.Remove(currentMaterialToChange);
                Console.WriteLine("The material was deleted!");
            }
            else
            {
                Console.WriteLine("Choose a relevant number:");
                //Be carefull with eventually ednless recursion
                CommandsInYourOnwMaterials(context, currentEmployee);
            }
        }

        public static void AddNewMaterial(CompanyManagementSystemContext context, Employee currentEmployee)
        {
            Material newMaterialToAdd = new Material();
            newMaterialToAdd.AuthorId = currentEmployee.Id;
            Console.WriteLine("Please enter the title of your material:");
            string title = Console.ReadLine();
            newMaterialToAdd.Title = title;
            Console.WriteLine("Please enter the URL of your material:");
            string url = Console.ReadLine();
            newMaterialToAdd.Url = url;
            ChangeAssessOfMaterial(context, currentEmployee, newMaterialToAdd);
            context.Materials.Add(newMaterialToAdd);
            context.SaveChanges();
            Console.WriteLine($"Congratulations, {currentEmployee.Username}! You successfully added a new material to your documents! ");
        }

        public static CompanyManagementSystemContext ChangeAssessOfMaterial
            (CompanyManagementSystemContext context, Employee currentEmployee, Material material)
        {
            Console.WriteLine("Please enter the level of access of your material: 1 for private, 2 for public, 3 for another:");
            string accessLevel = Console.ReadLine();
            switch (accessLevel)
            {
                case "1":
                    material.Access = (Access)Enum.Parse(typeof(Access), "Private", true);
                    material.RemoveAccess();
                    break;
                case "2":
                    material.Access = (Access)Enum.Parse(typeof(Access), "Public", true);
                    break;
                case "3":
                    material.Access = (Access)Enum.Parse(typeof(Access), "Another", true);
                    List<Employee> othersEmployees = context.Employees.Where(e => e.Id != currentEmployee.Id).ToList();
                    Console.WriteLine("Please enter id numbers of your colleagues to give them permission:");
                    Console.WriteLine(string.Join(Environment.NewLine, othersEmployees.Select(e => e.Id + " " + e.Username)));
                    int numberOfColleagues = 3;
                    while (numberOfColleagues-- > 0)
                    {
                        int currentIdToGivePermission = int.Parse(Console.ReadLine());
                        material.GiveAccess(currentIdToGivePermission);
                    }
                    Console.WriteLine($"You successfully give permision to some of your collegues");
                    break;
            }
            context.SaveChanges();
            return context;
        }


        public static CompanyManagementSystemContext ComandsInOthersMaterials
             (CompanyManagementSystemContext context, Employee currentEmployee)
        {
            List<Material> m1 = context.Materials

                         .ToList();

            List<Material> materialOfOtherPeople = context.Materials
                          .Where(m => m.AuthorId != currentEmployee.Id)
                          .ToList();

            Console.WriteLine($"In our system we have these materials: {string.Join(", ", materialOfOtherPeople.Select(m => m.Title))}");
            Console.WriteLine("If you wanna see details of a material, please write its name:");
            string title = Console.ReadLine();

            Material materialLookingFor = materialOfOtherPeople.FirstOrDefault(m => m.Title == title);

            if (materialLookingFor != null)
            {
                if (materialLookingFor.Access.ToString() == "Public")
                {
                    Console.WriteLine("This document is public. You can manage it:");
                    ChangeMaterialOfOtherPerson(title, materialLookingFor);
                }
                else if (materialLookingFor.Access.ToString() == "Private")
                {
                    Console.WriteLine("Sorry, this document is private, and you can see only the title.");
                }
                else
                {
                    //Another
                    if (materialLookingFor.CheckAccess(currentEmployee.Id))
                    {
                        Console.WriteLine("Your id has permission for this document. You can manage it:");
                        ChangeMaterialOfOtherPerson(title, materialLookingFor);
                    }
                    else
                    {
                        Console.WriteLine("Sorry, this document is private for you, and you can see only the title.");
                    }


                }
            }
            else
            {
                Console.WriteLine($"We have not a material with name {title}");
            }
            context.SaveChanges();
            return context;
        }

        private static void ChangeMaterialOfOtherPerson(string title, Material materialLookingFor)
        {
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
                Console.WriteLine("To change salary press 1, to fire press 2, to continue without change press 3");
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
                else if (command == "3")
                {
                    Console.WriteLine("No changes");
                }
                else
                {
                    Console.WriteLine("Invalid command");
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
