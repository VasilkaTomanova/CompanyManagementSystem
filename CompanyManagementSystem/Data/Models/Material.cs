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
        public Material()
        {
            this.IdsWithAccessToMyMaterials = new List<int>();
        }

        [Required]
        public string Title { get; set; }
        public int Id { get; set; }

        [Required]
        public int AuthorId { get; set; }
        public Employee Author { get; set; }

        [Required]
        public Access Access { get; set; }

        [Required]
        public string Url { get; set; }

        public List<int> IdsWithAccessToMyMaterials;
        //TODO
        public void ChangeAccess(int id)
        {
            this.IdsWithAccessToMyMaterials.Add(id);

        }




    }
}
