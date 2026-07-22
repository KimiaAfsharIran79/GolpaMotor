using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GolpaMotor.Controllers
{
    [Authorize(Roles = "Admin")]   // فقط کاربران دارای نقش Admin اجازه ورود دارند
    public class AdminController : Controller
    {
        // داشبورد اصلی ادمین
        public IActionResult Index()
        {
            return View();
        }
    }
}