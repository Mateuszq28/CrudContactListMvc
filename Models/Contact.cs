using CrudContactListMvc.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CrudContactListMvc.Models
{
    public class Contact
    {
        [Key]
        public int Id { get; set; }

        // unique
        [RegularExpression(@"^.*@.*$")]
        public string Email { get; set; }


        public string Name { get; set; }
        public string Surname { get; set; }

        // strong password
        public string Password { get; set; }

        [RegularExpression(@"^\d{3}-\d{3}-\d{3}$")]
        public string Phone { get; set; }

        // only date
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }



        // w - combo box
        // one-to-one
        public Category Category { get; set; }
        public int CategoryId { get; set; }




        // optional
        // w - combo box or text field
        // one-to-one
        public Subcategory? Subcategory { get; set; }
        public int? SubcategoryId { get; set; }



        public Contact()
        {
            
        }
    }
}
