using CrudContactListMvc.Data;
using CrudContactListMvc.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CrudContactListMvc.Controllers
{
    [ApiController]
    [Route("api/")]
    public class SimpsControllerAPI : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SimpsControllerAPI(ApplicationDbContext context)
        {
            _context = context;
        }



        // GET: MAIN PAGE
        [HttpGet]
        public ActionResult <IEnumerable<Contact>> Index()
        {
            return _context.Contact.ToList();
        }


        /*


        // ===================================
        // BLOCK FOR INTERACTIONS WITH CONTACT
        // ===================================

        // GET: Contacts
        public async Task<IActionResult> Contact_Index(IFormCollection form)
        {
            var applicationDbContext = _context.Contact.Include(c => c.Category).Include(c => c.Subcategory);
            return PartialView(await applicationDbContext.ToListAsync());
        }

        // GET: Contacts/ShowSearchForm
        public async Task<IActionResult> Contact_Search(IFormCollection form)
        {
            return PartialView();
        }

        // PoST: Contacts/ShowSearchResults
        public async Task<IActionResult> Contact_SearchShowResults(String SearchPhrase)
        {
            return PartialView("Contact_Index", await _context.Contact.Where(j => (j.Name.Contains(SearchPhrase) ||
                                                                     j.Surname.Contains(SearchPhrase) ||
                                                                     j.Email.Contains(SearchPhrase) ||
                                                                     //j.BirthDate.Date.Equals(DateTime.Parse(SearchPhrase).Date) ||
                                                                     j.Phone.Contains(SearchPhrase))).ToListAsync());
        }

        // GET: Contacts/Details/5
        [Authorize]
        public async Task<IActionResult> Contact_Details(IFormCollection form)
        {
            string? id_str = form["button"];
            if (id_str == null)
            {
                return NotFound();
            }
            int id = int.Parse(id_str);

            var contact = await _context.Contact
                .Include(c => c.Category)
                .Include(c => c.Subcategory)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contact == null)
            {
                return NotFound();
            }

            return PartialView(contact);
        }

        // GET: Contacts/Create
        [Authorize]
        public IActionResult Contact_Create(IFormCollection form)
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

        /*
            ViewData["SubcategoryId"] = new SelectList(_context.Subcategory.Where(m => m.CategoryId == 1), "Id", "Name");


            return PartialView();
        }

        // POST: Contacts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Contact_CreateAddRecord(IFormCollection form)
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

        // GET: Contacts/Edit/5
        [Authorize]
        public async Task<IActionResult> Contact_Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contact = await _context.Contact.FindAsync(id);
            if (contact == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Id", contact.CategoryId);
            ViewData["SubcategoryId"] = new SelectList(_context.Subcategory, "Id", "Id", contact.SubcategoryId);
            return PartialView(contact);
        }

        // POST: Contacts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Contact_EditApply(int id, [Bind("Id,Email,Name,Surname,Password,Phone,BirthDate,CategoryId,SubcategoryId")] Contact contact)
        {
            if (id != contact.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contact);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Contact_ContactExists(contact.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Id", contact.CategoryId);
            ViewData["SubcategoryId"] = new SelectList(_context.Subcategory, "Id", "Id", contact.SubcategoryId);
            return PartialView(contact);
        }

        // GET: Contacts/Delete/5
        [Authorize]
        public async Task<IActionResult> Contact_Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contact = await _context.Contact
                .Include(c => c.Category)
                .Include(c => c.Subcategory)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contact == null)
            {
                return NotFound();
            }

            return PartialView(contact);
        }

        // POST: Contacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Contact_DeleteConfirmed(int id)
        {
            var contact = await _context.Contact.FindAsync(id);
            if (contact != null)
            {
                _context.Contact.Remove(contact);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Contact_ContactExists(int id)
        {
            return _context.Contact.Any(e => e.Id == id);
        }



        // ======================================
        // BLOCK FOR INTERACTIONS WITH CATEGORIES
        // ======================================

        // GET: Categories
        public async Task<IActionResult> Category_Index(IFormCollection form)
        {
            return PartialView(await _context.Category.ToListAsync());
        }

        // GET: Categories/Details/5
        [Authorize]
        public async Task<IActionResult> Category_Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Category
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            // Put all correlated subcategories pointers into category
            // List is empty at the start (data is only in the database)
            category.Subcategory = null;
            ICollection<Subcategory> findedSubcategories = await _context.Subcategory
                .Where(m => m.CategoryId == id).ToListAsync();
            category.Subcategory = findedSubcategories;

            return PartialView(category);
        }

        // GET: Categories/Create
        [Authorize]
        public IActionResult Category_Create(IFormCollection form)
        {
            return PartialView();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Category_CreateAddRecord([Bind("Id,Name")] Category category)
        {
            if (ModelState.IsValid)
            {
                _context.Add(category);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return PartialView(category);
        }

        // GET: Categories/Edit/5
        [Authorize]
        public async Task<IActionResult> Category_Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Category.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return PartialView(category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Category_EditApply(int id, [Bind("Id,Name")] Category category)
        {
            if (id != category.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(category);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Category_CategoryExists(category.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return PartialView(category);
        }

        // GET: Categories/Delete/5
        [Authorize]
        public async Task<IActionResult> Category_Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Category
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return PartialView(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Category_DeleteConfirmed(int id)
        {
            var category = await _context.Category.FindAsync(id);
            if (category != null)
            {
                _context.Category.Remove(category);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Category_CategoryExists(int id)
        {
            return _context.Category.Any(e => e.Id == id);
        }



        // =========================================
        // BLOCK FOR INTERACTIONS WITH SUBCATEGORIES
        // =========================================

        // GET: Subcategories
        public async Task<IActionResult> Subcategory_Index(IFormCollection form)
        {
            var applicationDbContext = _context.Subcategory.Include(s => s.Category);
            return PartialView(await applicationDbContext.ToListAsync());
        }

        // GET: Subcategories/Details/5
        [Authorize]
        public async Task<IActionResult> Subcategory_Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subcategory = await _context.Subcategory
                .Include(s => s.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (subcategory == null)
            {
                return NotFound();
            }

            return PartialView(subcategory);
        }

        // GET: Subcategories/Create
        [Authorize]
        public IActionResult Subcategory_Create(IFormCollection form)
        {
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name");
            return PartialView();
        }

        // POST: Subcategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Subcategory_CreateAddRecord([Bind("Id,Name,CategoryId")] Subcategory subcategory)
        {
            //subcategory.Category = await _context.Category.Where(m => m.Id == subcategory.CategoryId).FirstOrDefaultAsync();
            if (ModelState.IsValid)
            {
                _context.Add(subcategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name", subcategory.CategoryId);
            return PartialView(subcategory);
        }

        // GET: Subcategories/Edit/5
        [Authorize]
        public async Task<IActionResult> Subcategory_Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subcategory = await _context.Subcategory.FindAsync(id);
            if (subcategory == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Id", subcategory.CategoryId);
            return PartialView(subcategory);
        }

        // POST: Subcategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Subcategory_EditApply(int id, [Bind("Id,Name,CategoryId")] Subcategory subcategory)
        {
            if (id != subcategory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(subcategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Subcategory_SubcategoryExists(subcategory.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Id", subcategory.CategoryId);
            return PartialView(subcategory);
        }

        // GET: Subcategories/Delete/5
        [Authorize]
        public async Task<IActionResult> Subcategory_Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subcategory = await _context.Subcategory
                .Include(s => s.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (subcategory == null)
            {
                return NotFound();
            }

            return PartialView(subcategory);
        }

        // POST: Subcategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Subcategory_DeleteConfirmed(int id)
        {
            var subcategory = await _context.Subcategory.FindAsync(id);
            if (subcategory != null)
            {
                _context.Subcategory.Remove(subcategory);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Subcategory_SubcategoryExists(int id)
        {
            return _context.Subcategory.Any(e => e.Id == id);
        }


        */



    }
}
