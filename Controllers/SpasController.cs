using Microsoft.AspNetCore.Mvc;

namespace CrudContactListMvc.Controllers
{
    public class SpasController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
