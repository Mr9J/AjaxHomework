using AjaxHomework.Models;
using Microsoft.AspNetCore.Mvc;

namespace AjaxHomework.Controllers
{
    public class AddressController : Controller
    {
        private readonly MyDbContext context;

        public AddressController(MyDbContext _context)
        {
            this.context = _context;
        }
        public IActionResult Index()
        {

            return View();
        }
    }
}
