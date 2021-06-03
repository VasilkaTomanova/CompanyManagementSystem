using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyManagementSystem.Data.Models
{
 public   class EmployeesProjects
    {

        public int EmployeeId { get; set; }

        public Employee Employee { get; set; }


        public int ProjectId { get; set; }

        public Project Project { get; set; }

    }
}
