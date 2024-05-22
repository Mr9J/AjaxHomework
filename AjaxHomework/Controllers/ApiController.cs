using AjaxHomework.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace AjaxHomework.Controllers
{
    public class ApiController : Controller
    {
        private readonly MyDbContext context;
        private readonly IWebHostEnvironment hostEnvironment;

        public ApiController(MyDbContext _context, IWebHostEnvironment _hostEnvironment)
        {
            this.context = _context;
            this.hostEnvironment = _hostEnvironment;
        }

        public IActionResult GetCity()
        {
            var result = context.Addresses.Select(a => a.City).Distinct();
            return Json(result);
        }

        public IActionResult GetDistrict(string city)
        {
            var result = context.Addresses.Where(a => a.City == city).Select(a => a.SiteId).Distinct();
            return Json(result);
        }

        public IActionResult GetRoad(string district)
        {
            var result = context.Addresses.Where(a => a.SiteId == district).Select(a => a.Road).Distinct();
            return Json(result);
        }

        public IActionResult Register(Member member, IFormFile avatar)
        {
            if (string.IsNullOrEmpty(member.Name))
            {
                member.Name = "guest";
            }

            string uploadPath = Path.Combine(hostEnvironment.WebRootPath, "uploads", avatar.FileName);
            string info = uploadPath;
            using (var fileStream = new FileStream(uploadPath, FileMode.Create))
            {
                avatar.CopyTo(fileStream);
            }

            byte[]? imgByte = null;
            using (var memoryStream = new MemoryStream())
            {
                avatar.CopyTo(memoryStream);
                imgByte = memoryStream.ToArray();
            }


            member.FileName = avatar.FileName;
            member.FileData = imgByte;
            context.Members.Add(member);
            context.SaveChanges();

            return Content(info, "text/plain", System.Text.Encoding.UTF8);
          
        }

        public IActionResult GetSpots()
        {
            var result = (from s in context.Spots
                          join si in context.SpotImages on s.SpotId equals si.SpotId
                          select new {s.SpotTitle,s.SpotDescription, si.ImagePath});

            return Json(result);
        }

        public IActionResult CheckUserExist(string email)
        {
            var result = context.Members.Any(m => m.Email == email);

            return Content(result.ToString(), "text/plain", System.Text.Encoding.UTF8);
        }
    }
}
