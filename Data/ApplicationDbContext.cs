using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CrudContactListMvc.Models;

namespace CrudContactListMvc.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<CrudContactListMvc.Models.Category> Category { get; set; } = default!;
        public DbSet<CrudContactListMvc.Models.Subcategory> Subcategory { get; set; } = default!;
        public DbSet<CrudContactListMvc.Models.Contact> Contact { get; set; } = default!;
        public DbSet<CrudContactListMvc.Models.Spa> Spa { get; set; } = default!;
        public DbSet<CrudContactListMvc.Models.Simp> Simp { get; set; } = default!;
    }
}
