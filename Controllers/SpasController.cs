using CrudContactListMvc.Data;
using CrudContactListMvc.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CrudContactListMvc.Controllers
{
    public class SpasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SpasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: MAIN PAGE
        public IActionResult Index()
        {
            return View();
        }


        // GET: Contacts
        public async Task<IActionResult> ShowContactIndex()
        {
            var applicationDbContext = _context.Contact.Include(c => c.Category).Include(c => c.Subcategory);
            return PartialView(await applicationDbContext.ToListAsync());
        }


        // GET: Contacts/Create
        [Authorize]
        public IActionResult Create(IFormCollection form)
        {
            // string for condition to show drop-down subcategory menu
            Category expectedCategory = _context.Category.Where(m => m.Id == 1).First();
            ViewBag.DropDownCategory = expectedCategory.Name;
            //ViewBag.DropDownCategory = "Business";


            // other category - it is used to show text field instead of drop-down
            Category otherCategoryName = _context.Category.Where(m => m.Id == 3).First();
            ViewBag.otherCategoryName = otherCategoryName.Name;
            //ViewBag.otherCategoryName = "Other";


            // Send emails to View. This are emails from the Database
            // Reason: They must be UNIQUE
            List<string> emails = _context.Contact.Select(m => m.Email).ToList();
            ViewBag.emails = emails;




            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name");



            // Working, but pointless - it should be done in Views JavaScript

            /*
            var subcategories = _context.Category.ToList();
            // Create a list of SelectListItem
            var subcategoryList = subcategories.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name
            }).ToList();
            // Add a new option
            subcategoryList.Insert(0, new SelectListItem { Value = "", Text = "" });
            // Pass the list to the SelectList constructor
            ViewData["SubcategoryId"] = new SelectList(subcategoryList, "Value", "Text");
            */




            // Doesn't working

            //SelectList slSubcategory = new SelectList(_context.Subcategory, "Id", "Name");
            //slSubcategory.Insert(0, new SelectListItem { Value = "", Text = "" });
            //ViewData["SubcategoryId"] = slSubcategory;



            // NULL values in Selection List should be implemented in Views JavaScript
            ViewData["SubcategoryId"] = new SelectList(_context.Subcategory.Where(m => m.CategoryId == 1), "Id", "Name");


            return PartialView();
        }

        // POST: Contacts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> CreateReturn(IFormCollection form)
        {
            string newSubcatName = form["NewCategory"];

            Contact contact = new Contact
            {
                //Id = int.Parse(form["Id"]),
                Email = form["Email"],
                Name = form["Name"],
                Surname = form["Surname"],
                Password = form["Password"],
                Phone = form["Phone"],
                BirthDate = DateTime.Parse(form["BirthDate"]),
                CategoryId = int.Parse(form["CategoryId"])
            };

            // SubcategoryId can be in at least 3 different states
            if (contact.CategoryId == 3)
            {
                int catId = 3;
                Subcategory newSub = new Subcategory
                {
                    Name = newSubcatName,
                    CategoryId = catId
                };
                _context.Add(newSub);
                await _context.SaveChangesAsync();

                contact.SubcategoryId = newSub.Id;
                //contact.SubcategoryId = _context.Subcategory.Where(m => m.Name == newSubcatName).Last().Id;
            }
            else if (contact.CategoryId == 2)
            {
                contact.SubcategoryId = null;
            }
            else
            {
                contact.SubcategoryId = int.Parse(form["SubcategoryId"]);
            }




            if (ModelState.IsValid)
            {
                _context.Add(contact);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name", contact.CategoryId);
            ViewData["SubcategoryId"] = new SelectList(_context.Subcategory, "Id", "Name", contact.SubcategoryId);
            return PartialView(contact);
        }

    }
}
