using CrudContactListMvc.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CrudContactListMvc.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }


        // one-to-many
        public ICollection<Subcategory>? Subcategory { get; set; }


        public Category()
        {
            
        }

    }
}
