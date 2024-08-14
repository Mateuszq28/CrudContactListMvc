using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrudContactListMvc.Models
{
    public class Subcategory
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }


        // one-to-one
        public Category? Category { get; set; }
        public int CategoryId { get; set; }

        public Subcategory()
        {
            
        }
    }
}
