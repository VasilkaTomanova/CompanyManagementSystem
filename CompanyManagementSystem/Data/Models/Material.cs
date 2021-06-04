using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyManagementSystem.Data.Models
{
  public class Material
    {
        private List<int> idsWithAccessToMyMaterials;

        public Material()
        {
            this.idsWithAccessToMyMaterials = new List<int>();
        }

        public string Title { get; set; }
        public int Id { get; set; }

        [Required]
        public int AuthorId { get; set; }
        public Employee Author { get; set; }

        public Access Access { get; set; }

        public string Url { get; set; }

        
        //TODO
        public void ChangeAccess()
        {
            //this.idsWithAccessToMyMaterials.Clear();
        }




    }
}
