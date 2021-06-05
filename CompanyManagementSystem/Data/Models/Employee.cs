using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
            this.Projects = new HashSet<EmployeesProjects>();
        }
        public int Id { get; set; }

        [Required]
        //[MinLength(3)]
        //[MaxLength(15)]
        [RegularExpression(@"[A-Z][a-z]{2,15}")]
        public string FirstName { get; set; }

        [Required]
        //[MinLength(3)]
        //[MaxLength(15)]
        [RegularExpression(@"[A-Z][a-z]{2,15}")]
        public string LastName { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(15)]
        public string Username { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(15)]
        public string Password { get; set; }

       
        public decimal? Salary { get; set; }
   


        public int PositionId { get; set; }
        public Position Position { get; set; }

        public int Rank { get; set; }       


       
        public int? ManagerId { get; set; }
        public Employee Manager { get; set; }




        //TODO
        public bool IsActiveEmployee { get; set; }

        //TODO
        public ICollection<Material> OwnMaterials { get; set; }

        public ICollection<EmployeesProjects> Projects { get; set; }


        //Could make with persantage or -=sum
        public void ChangeSalary(decimal newSalary)
        {

            this.Salary = newSalary;

        }

    }
}
