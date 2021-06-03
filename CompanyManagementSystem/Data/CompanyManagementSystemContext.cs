using Microsoft.EntityFrameworkCore;

namespace CompanyManagementSystem.Data
{
   public class CompanyManagementSystemContext : DbContext
    {
        public CompanyManagementSystemContext()
        {

        }
        public CompanyManagementSystemContext(DbContextOptions options)
            :base(options)
        {

        }






    }
}
