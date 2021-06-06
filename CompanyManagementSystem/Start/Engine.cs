using CompanyManagementSystem.Data;
using CompanyManagementSystem.Data.Models;
using System;


namespace CompanyManagementSystem.Start
{
    public class Engine
    {
        private CompanyManagementSystemContext context;
        private Methods method;
        public Engine(CompanyManagementSystemContext context)
        {
            this.context = context;
            this.method = new Methods();
        }
        public void Run()
        {
            Console.WriteLine("Welcome in our Company Managament system! If you alreday registered press 1, if you have NOT any registration press 2");
            string initialCommand = Console.ReadLine();
            if (initialCommand == "2")
            {
                //not registered
                bool successfullRegistration = this.method.RegistrationFrom(this.context);
                if (successfullRegistration == false)
                {
                    Console.WriteLine("Unsuccessfull registration! Try later!");
                    return;
                }
            }
            Employee currentEmployee = this.method.LoginInForm(this.context);
            if (currentEmployee == null)
            {
                // Your username is invalid or password is not correct!
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
                    this.method.ClearTheConsole();
                    counterToClearConsole = 0;
                }
                //read commmand
                Console.WriteLine("For your own materials: 1, for materials of other people: 2, to see the list of your colleagues: 3, for exit: 4");
                string command = Console.ReadLine();

                if (command == "1")
                {
                    // If you are in this case you have all permission to do everything to these materials, because they are yours                
                    this.method.CommandsInYourOnwMaterials(this.context, currentEmployee);
                    Console.WriteLine();
                }
                else if (command == "2")
                {
                    // If you are in this case there is checking the level of permision of materials
                    this.method.ComandsInOthersMaterials(this.context, currentEmployee);
                    Console.WriteLine();
                }
                else if (command == "3")
                {
                    //In this case you look your colleagues
                    this.method.CommandsForColleagues(this.context, currentEmployee);
                }
                else if (command == "4")
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

    }
}
