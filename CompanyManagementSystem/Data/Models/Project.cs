using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyManagementSystem.Data.Models
{
public    class Project
    {
        public Project()
        {
            this.Employees = new HashSet<EmployeesProjects>();
        }
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<EmployeesProjects> Employees { get; set; }

    }
}
