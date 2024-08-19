namespace CrudContactListMvc.Models
{
    public class Simp
    {
        public int Id { get; set; }
        public List<Subcategory> SubcategoryList { get; set; }
        public List<Category> CategoryList { get; set; }
        public List<Contact> ContactList { get; set; }

        public Simp()
        {
            
        }
    }
}
