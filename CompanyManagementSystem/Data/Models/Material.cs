using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyManagementSystem.Data.Models
{
  public class Material
    {
        public int Id { get; set; }


        public int AuthorId { get; set; }
        public Employee Author { get; set; }

       

        public string Url { get; set; }



    }
}
