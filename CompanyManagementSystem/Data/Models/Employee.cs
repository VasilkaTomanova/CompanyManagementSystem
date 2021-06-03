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
        }
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public decimal Salary { get; set; }

        public Position Position { get; set; }

        public Rank Rank { get; set; }


        public Employee Manager { get; set; }

        public int ManagerId { get; set; }

        //TODO
        public bool IsActiveEmployee { get; set; }

        //TODO
        public ICollection<Material> Materials { get; set; }

    }
}
