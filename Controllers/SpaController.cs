using CrudContactListMvc.Data;
using Microsoft.AspNetCore.Mvc;

namespace CrudContactListMvc.Controllers
{
    public class SpaController : Controller
    {

        private readonly ApplicationDbContext _context;

        public SpaController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }


    }
}
