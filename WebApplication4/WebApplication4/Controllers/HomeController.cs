using Microsoft.AspNetCore.Mvc;
using System.Security;

namespace WebApplication37.Controllers
{
    public class HomeController : Controller
    {

        [HttpGet]
        public IActionResult Index()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string username, string password, int age, string comment, IFormFile photoFile)
        {
            ViewData["username"] = username;
            ViewData["password"] = password;
            ViewData["age"] = age;
            ViewData["comment"] = comment;
            if (photoFile != null && photoFile.Length > 0)
            {
                var fileName = Guid.NewGuid() + Path.GetExtension(photoFile.FileName);
                var filePath = Path.Combine("wwwroot", "Images", fileName);
                try
                {
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await photoFile.CopyToAsync(stream);
                    }
                    ViewData["photo"] = $"Images/{fileName}";
                }
                catch (Exception ex)
                {
                    ViewData["photo"] = $"Помилка при збереженні файлу: {ex.Message}";
                }
                Console.WriteLine($"FilePath: {filePath}");
            }


            return View("answer");
        }

    }
}
