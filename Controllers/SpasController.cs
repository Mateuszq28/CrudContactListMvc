using CrudContactListMvc.Data;
using Microsoft.AspNetCore.Mvc;
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

        public IActionResult Index()
        {
            return View();
        }
    }
}
