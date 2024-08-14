using Microsoft.EntityFrameworkCore;
using CrudContactListMvc.Data;
using System.ComponentModel.DataAnnotations;
using NuGet.Packaging;

namespace CrudContactListMvc.Models;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new ApplicationDbContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<ApplicationDbContext>>()))
        {
            if (context == null || context.Contact == null)
            {
                throw new ArgumentNullException("Null ApplicationDbContext");
            }

            // Look for any Categories.
            if (!context.Category.Any())
            {

                context.Category.AddRange(
                    new Category
                    {
                        Name = "Business"
                    },

                    new Category
                    {
                        Name = "Private"
                    },

                    new Category
                    {
                        Name = "Other"
                    }
                );
                context.SaveChanges();
            }

            // Look for any Subcategory.
            if (!context.Subcategory.Any())
            {
                // categories Business and Other
                // they have subcategories from the begenning of the program
                Category category1 = context.Category.Where(m => m.Id == 1).First();
                Category category3 = context.Category.Where(m => m.Id == 3).First();


                // Create Subcategories instances
                Subcategory boss = new Subcategory
                {
                    Category = category1,
                    CategoryId = 1,
                    Name = "Boss"
                };

                Subcategory client = new Subcategory
                {
                    Category = category1,
                    CategoryId = 1,
                    Name = "Client"
                };

                Subcategory basia = new Subcategory
                {
                    Category = category1,
                    CategoryId = 1,
                    Name = "Mrs. Basia"
                };

                Subcategory fotowolt = new Subcategory
                {
                    Category = category3,
                    CategoryId = 3,
                    Name = "fotowoltaika"
                };


                // assign them to the parent category
                List<Subcategory> subsBusiness = new List<Subcategory> { boss, client, basia };
                List<Subcategory> subsOther = new List<Subcategory> { fotowolt };
                // business
                if (category1.Subcategory != null) category1.Subcategory.AddRange(subsBusiness);
                else category1.Subcategory = subsBusiness;
                // other
                if (category3.Subcategory != null) category3.Subcategory.AddRange(subsOther);
                else category3.Subcategory = subsOther;


                // add them to the context
                context.Subcategory.AddRange(boss, client, basia, fotowolt);
                context.SaveChanges();
            }

            // Look for any Contacts.
            if (!context.Contact.Any())
            {
                Category category1 = context.Category.Where(m => m.Id == 1).First();
                Category category2 = context.Category.Where(m => m.Id == 2).First();
                Category category3 = context.Category.Where(m => m.Id == 3).First();

                Subcategory subcategory1 = context.Subcategory.Where(m => m.Id == 1).First();
                Subcategory subcategory2 = context.Subcategory.Where(m => m.Id == 2).First();
                Subcategory subcategory4 = context.Subcategory.Where(m => m.Id == 4).First();

                context.Contact.AddRange(
                    new Contact
                    {
                        Name = "Jan",
                        Surname = "Nowak",
                        Email = "jan@nowak.pl",
                        Password = "12d_54$%_12Fd",
                        CategoryId = 1,
                        Category = category1,
                        SubcategoryId = 1,
                        Subcategory = subcategory1,
                        Phone = "111-111-111",
                        BirthDate = DateTime.Parse("1990-3-13")
                    },

                    new Contact
                    {
                        Name = "Anna",
                        Surname = "Kowalska",
                        Email = "anna@kowalska.pl",
                        Password = "12d_54$%_12Fd",
                        CategoryId = 1,
                        Category = category1,
                        SubcategoryId = 2,
                        Subcategory = subcategory2,
                        Phone = "222-111-111",
                        BirthDate = DateTime.Parse("1992-3-13")
                    },

                    new Contact
                    {
                        Name = "Beata",
                        Surname = "Jaworska",
                        Email = "beata@jaworska.pl",
                        Password = "12d_54$%_12Fd",
                        CategoryId = 2,
                        Category = category2,
                        SubcategoryId = null,
                        Subcategory = null,
                        Phone = "333-111-111",
                        BirthDate = DateTime.Parse("1993-3-13")
                    },

                    new Contact
                    {
                        Name = "Marek",
                        Surname = "Rolada",
                        Email = "marek@rolada.pl",
                        Password = "12d_54$%_12Fd",
                        CategoryId = 3,
                        Category = category3,
                        SubcategoryId = 4,
                        Subcategory = subcategory4,
                        Phone = "444-111-111",
                        BirthDate = DateTime.Parse("1994-3-13")
                    }
                );
                context.SaveChanges();
            }

        }
    }
}
