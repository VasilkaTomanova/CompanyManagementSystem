using CompanyManagementSystem.Data;
using System;

namespace CompanyManagementSystem
{
  public  class StartUp
    {
        public static void Main(string[] args)
        {

            var context = new CompanyManagementSystemContext();
            //context.Database.EnsureDeleted();
            context.Database.EnsureCreated();



        }
    }
}
