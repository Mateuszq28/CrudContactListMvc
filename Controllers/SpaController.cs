using Microsoft.AspNetCore.Mvc;

namespace CrudContactListMvc.Controllers
{
    public class SpaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
