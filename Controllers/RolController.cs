using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace CarritoDeCompras.Controllers
{
    public class RolController : Controller
    {
        [Authorize(Policy ="EmployeeOnly")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
