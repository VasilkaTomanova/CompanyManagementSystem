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


        //TODO
        public void GiveAccess(int id)
        {
            this.idsWithAccessToMyMaterials.Add(id);
        }

        public void RemoveAccess()
        {
            this.idsWithAccessToMyMaterials.Clear();
        }

        public bool CheckAccess(int otherId)
        {
            if(this.idsWithAccessToMyMaterials.Contains(otherId))
            {
                return true;
            }
            return false;
        }


    }
}
