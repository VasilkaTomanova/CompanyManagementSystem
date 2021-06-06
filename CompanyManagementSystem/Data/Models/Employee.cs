using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CompanyManagementSystem.Data.Models
{
    public class Employee
    {
        //private const int minLenghtOfString = 3;
        //private const int maxLength = 15;

        private const string usernamePattern = @"[a-z]{3,15}";
        private readonly Regex usernameRegex;

        private const string passwordPattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{4,8}$";
        private readonly Regex passowrdRegex;

        private string username;
        private string password;


        public Employee()
        {
            //Initialize the collection correct
            this.OwnMaterials = new HashSet<Material>();
            this.Projects = new HashSet<EmployeesProjects>();
            this.usernameRegex = new Regex(usernamePattern);
            this.passowrdRegex = new Regex(passwordPattern);
        }
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }
        

        [Required]
        public string LastName { get; set; }

        [Required]
        //eventually with init but we need constructor
        public string Username
        {
            get { return this.username; }
            set
            {
                if (ValidateUsername(value) == false)
                {
                    throw new ArgumentException("Invalid format value of your username!");
                }
                this.username = value;
            }
        }

        [Required]
        public string Password
        {
            get { return this.password; }
            set
            {
                if (ValidatePassowrd(value) == false)
                {
                    throw new ArgumentException("Invalid format value of your passwrd!");
                }
                this.password = value;
            }
        }


        public decimal? Salary { get; set; }
   


        public int PositionId { get; set; }
        public Position Position { get; set; }

        public int Rank { get; set; }       

        public int? ManagerId { get; set; }
        public Employee Manager { get; set; }

        public bool IsActiveEmployee { get; set; }

        public ICollection<Material> OwnMaterials { get; set; }

        public ICollection<EmployeesProjects> Projects { get; set; }


        public void ChangeSalary(decimal newSalary)
        {
            this.Salary = newSalary;
        }

        private bool ValidateUsername (string stringToValidate)
        {
            Match match = this.usernameRegex.Match(stringToValidate);
            if(match != null)
            {
                return true;
            }

            return false;
        }

        //TODO
        private bool ValidatePassowrd(string stringToValidate)
        {
            Match match = this.passowrdRegex.Match(stringToValidate);
            if (match != null)
            {
                return true;
            }

            return false;
        }


    }
}
