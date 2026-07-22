using DataAccess.Services;
using DomainModel.Models;
using GolpaMotor.Models.ViewModels.Support;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GolpaMotor.Controllers
{
    public class SupportController : Controller
    {
        private readonly ISupportRepository repo;
        private readonly UserManager<ApplicationUser> userManager;

        public SupportController(ISupportRepository repo,UserManager<ApplicationUser> userManager)
        {
            this.repo = repo;
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TicketCreateViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await userManager.GetUserAsync(User);

            if (user == null)
            {
                TempData["Error"] = "ابتدا وارد حساب کاربری شوید";
                return RedirectToAction("Login", "Account");
            }

            await repo.AddTicket(new SupportTicket
            {
                UserID = user.Id,
                Title = model.Title,
                Message = model.Message,
                CreateDate = DateTime.Now,
                IsAnswered = false,
                IsClosed = false,
                Priority = 2
            });

            await repo.SaveChangesAsync();

            TempData["Success"] = "تیکت شما با موفقیت ثبت شد";

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> MyTickets()
        {
            var user = await userManager.GetUserAsync(User);

            if (user == null)
                return RedirectToAction("Login", "Account");

            var model = await repo.GetUserTickets(user.Id);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Details(long id)
        {
            var user = await userManager.GetUserAsync(User);

            if (user == null)
                return RedirectToAction("Login", "Account");

            var model = await repo.GetTicketDetails(id, user.Id);

            if (model == null)
                return NotFound();

            return View(model);
        }
    }
}
