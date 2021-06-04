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
            //TODO eventually constructors in classes?
            Position posision1 = new Position();
            posision1.Name = "Architect";
            Position posision2 = new Position();
            posision2.Name = "Project Manager";
            Position posision3 = new Position();
            posision3.Name = "Worker";
            Position posision4 = new Position();
            posision4.Name = "QA";
            context.Positions.Add(posision1);
            context.Positions.Add(posision2);
            context.Positions.Add(posision3);
            context.Positions.Add(posision4);
            context.SaveChanges();

            Employee employee1 = new Employee();
            employee1.FirstName = "Vasilka"; employee1.LastName = "Tomanova";
            employee1.Username = "vtomanova"; employee1.Password = "123456";
            employee1.PositionId = 2; employee1.Salary = 1000;
            employee1.Rank = 3; employee1.IsActiveEmployee = true;
            Employee employee2 = new Employee();
            employee2.FirstName = "Briana"; employee2.LastName = "Dimitrova";
            employee2.Username = "bridi"; employee2.Password = "654321";
            employee2.PositionId = 4; employee2.Salary = 8000;
            employee2.Rank = 2; employee2.IsActiveEmployee = true;
            context.Employees.Add(employee1);
            context.Employees.Add(employee2);
            context.SaveChanges();

            Material materialToVasi1 = new Material();
            materialToVasi1.Title = "Iron candle"; materialToVasi1.AuthorId = 1;
            Material materialToVasi2 = new Material();
            materialToVasi2.Title = "No title"; materialToVasi2.AuthorId = 1;
            Material materialToBri = new Material();
            materialToBri.Title = "Title"; materialToBri.AuthorId = 2;
            context.Materials.Add(materialToVasi1);
            context.Materials.Add(materialToVasi2);
            context.Materials.Add(materialToBri);


            context.SaveChanges();
            return context;
        }
    }
}
