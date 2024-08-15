using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CrudContactListMvc.Data;
using CrudContactListMvc.Models;
using Microsoft.AspNetCore.Authorization;

namespace CrudContactListMvc.Controllers
{
    public class ContactsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ContactsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Contacts
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Contact.Include(c => c.Category).Include(c => c.Subcategory);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Contacts/ShowSearchForm
        public async Task<IActionResult> ShowSearchForm()
        {
            return View();
        }

        // PoST: Contacts/ShowSearchResults
        public async Task<IActionResult> ShowSearchResults(String SearchPhrase)
        {
            return View("Index", await _context.Contact.Where( j => (j.Name.Contains(SearchPhrase) ||
                                                                     j.Surname.Contains(SearchPhrase) ||
                                                                     j.Email.Contains(SearchPhrase) ||
                                                                     //j.BirthDate.Date.Equals(DateTime.Parse(SearchPhrase).Date) ||
                                                                     j.Phone.Contains(SearchPhrase)) ).ToListAsync());
        }

        // GET: Contacts/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
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

            return View(contact);
        }

        // GET: Contacts/Create
        [Authorize]
        public IActionResult Create()
        {
            // string for condition to show drop-down subcategory menu
            Category expectedCategory = _context.Category.Where(m => m.Id == 1).First();
            ViewBag.DropDownCategory = expectedCategory.Name;
            //ViewBag.DropDownCategory = "Business";


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
            ViewData["SubcategoryId"] = new SelectList(_context.Subcategory, "Id", "Name");


            return View();
        }

        // POST: Contacts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Id,Email,Name,Surname,Password,Phone,BirthDate,CategoryId,SubcategoryId")] Contact contact)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contact);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name", contact.CategoryId);
            ViewData["SubcategoryId"] = new SelectList(_context.Subcategory, "Id", "Name", contact.SubcategoryId);
            return View(contact);
        }

        // GET: Contacts/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
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
            return View(contact);
        }

        // POST: Contacts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Email,Name,Surname,Password,Phone,BirthDate,CategoryId,SubcategoryId")] Contact contact)
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
                    if (!ContactExists(contact.Id))
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
            return View(contact);
        }

        // GET: Contacts/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
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

            return View(contact);
        }

        // POST: Contacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contact = await _context.Contact.FindAsync(id);
            if (contact != null)
            {
                _context.Contact.Remove(contact);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContactExists(int id)
        {
            return _context.Contact.Any(e => e.Id == id);
        }
    }
}
