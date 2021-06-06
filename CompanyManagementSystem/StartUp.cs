
using CompanyManagementSystem.Data;
using CompanyManagementSystem.Start;

namespace CompanyManagementSystem
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            //1.Create data base
            CompanyManagementSystemContext context = new CompanyManagementSystemContext();

            // If you wannt to create a simple database with some entities for demo purpose on your computer uncomment these rows!
            // 16-19
            //context.Database.EnsureDeleted();
            //context.Database.EnsureCreated();
            //CreateDataBase cb = new CreateDataBase();
            //cb.CreateDatabase(context);

            //2. Entry in system 
            Engine startTheApp = new Engine(context);
            startTheApp.Run();
            
        } // end main

    }
}
