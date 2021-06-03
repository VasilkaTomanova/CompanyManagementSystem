using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyManagementSystem.Data.Models
{
    public class Employee
    {
        public Employee()
        {
            //Initialize the collection correct
            this.OwnMaterials = new HashSet<Material>();
            this.Projects = new HashSet<Project>();
        }
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public decimal Salary { get; set; }

        public Position Position { get; set; }

        public int Rank { get; set; }       

        //TODO
        public bool IsActiveEmployee { get; set; }

        //TODO
        public ICollection<Material> OwnMaterials { get; set; }

        public ICollection<Project> Projects { get; set; }


        //Could make with persantage or -=sum
        public void ChangeSalary(decimal newSalary)
        {

            this.Salary = newSalary;

        }

    }
}
