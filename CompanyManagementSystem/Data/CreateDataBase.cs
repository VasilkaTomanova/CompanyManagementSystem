using CompanyManagementSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyManagementSystem.Data
{
  public  class CreateDataBase
    {
        public  CompanyManagementSystemContext CreateDatabase(CompanyManagementSystemContext context)
        {
            

            Position posision1 = new Position();
            posision1.Id = 1;
            posision1.Name = "Architect";
            Position posision2 = new Position();
            posision1.Id = 2;
            posision2.Name = "Project Manager";
            Position posision3 = new Position();
            posision1.Id = 3;
            posision3.Name = "Worker";
            Position posision4 = new Position();
            posision1.Id = 4;
            posision4.Name = "QA";
            context.Positions.Add(posision1);
            context.Positions.Add(posision2);
            context.Positions.Add(posision3);
            context.Positions.Add(posision4);




            context.SaveChanges();
            return context;
        }
    }
}
