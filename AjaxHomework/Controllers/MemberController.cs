using Microsoft.AspNetCore.Mvc;

namespace AjaxHomework.Controllers
{
    public class MemberController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }
    }
}
