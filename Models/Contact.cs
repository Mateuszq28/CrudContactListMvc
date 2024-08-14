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
        // https://stackoverflow.com/questions/5142103/regex-to-validate-password-strength
        /*
        
        ^                         Start anchor
        (?=.*[A-Z].*[A-Z])        Ensure string has two uppercase letters.
        (?=.*[!@#$&*])            Ensure string has one special case letter.
        (?=.*[0-9].*[0-9])        Ensure string has two digits.
        (?=.*[a-z].*[a-z].*[a-z]) Ensure string has three lowercase letters.
        .{8}                      Ensure string is of length 8.
        $                         End anchor.

        */
        [RegularExpression(@"^(?=.*[A-Z]{1,})(?=.*[a-z]{1,})(?=.*[0-9]{1,})(?=.*[~!@#$%^&*()\-_=+{};:,<.>]{1,}).{8,}$")]
        public string Password { get; set; }

        [RegularExpression(@"^\d{3}-\d{3}-\d{3}$")]
        public string Phone { get; set; }

        // only date
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }



        // w - combo box
        // one-to-one
        public Category? Category { get; set; }
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
