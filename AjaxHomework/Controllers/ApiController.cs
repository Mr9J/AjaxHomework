using AjaxHomework.Models;
using AjaxHomework.Services;
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

        [HttpPost]
        public IActionResult Register(Member member, IFormFile avatar)
        {
            try
            {
                if (string.IsNullOrEmpty(member.Name))
                {
                    member.Name = "guest";
                }

                if (avatar != null)
                {
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
                }

                string hashedPassword = Hash.HashPassword(member.Password!);
                member.Password = hashedPassword;

                context.Members.Add(member);
                context.SaveChanges();

                return Ok("註冊成功");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }



        }

        public IActionResult GetSpots()
        {
            var result = (from s in context.Spots
                          join si in context.SpotImages on s.SpotId equals si.SpotId
                          select new { s.SpotTitle, s.SpotDescription, si.ImagePath });

            return Json(result);
        }

        public IActionResult CheckUserExist(string email)
        {
            var result = context.Members.Any(m => m.Email == email);

            return Content(result.ToString(), "text/plain", System.Text.Encoding.UTF8);
        }

        [HttpPost]
        public IActionResult SearchSpots([FromBody] SearchDTO x)
        {
            var spots = x.categoryId == 0 ? context.SpotImagesSpots : context.SpotImagesSpots.Where(s => s.CategoryId == x.categoryId);

            if (!string.IsNullOrEmpty(x.keyword))
            {
                spots = spots.Where(s => s.SpotTitle!.Contains(x.keyword) || s.SpotDescription!.Contains(x.keyword));
            }

            switch (x.sortBy)
            {
                case "spotTitle":
                    spots = x.sortType == "asc" ? spots.OrderBy(s => s.SpotTitle) : spots.OrderByDescending(s => s.SpotTitle);
                    break;
                case "categoryId":
                    spots = x.sortType == "asc" ? spots.OrderBy(s => s.CategoryId) : spots.OrderByDescending(s => s.CategoryId);
                    break;
                default:
                    spots = x.sortType == "asc" ? spots.OrderBy(s => s.SpotId) : spots.OrderByDescending(s => s.SpotId);
                    break;
            }

            int totalCount = spots.Count();

            int pageSize = x.pageSize;

            int page = x.page;

            int totalPages = (int)Math.Ceiling((decimal)totalCount / pageSize);

            spots = spots.Skip((page - 1) * pageSize).Take(pageSize);

            SpotsPagingDTO spotsPaging = new SpotsPagingDTO();
            spotsPaging.TotalCount = totalCount;
            spotsPaging.TotalPages = totalPages;
            spotsPaging.SpotsResult = spots.ToList();

            return Json(spotsPaging);
        }
    }
}
