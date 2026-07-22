using DataAccess.Services;
using DomainModel.Models;
using DomainModel.ViewModels.Warranty;
using GolpaMotor.FrameworkUI.Services;
using GolpaMotor.Models.ViewModels.WarrantyManagement;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GolpaMotor.Controllers
{
    public class WarrantyManagementController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly ICardRegistrationRepository repo;
        private readonly IWarrantyCardRepository warrantyRepo;
        private readonly IProductRepository productRepo;
        private readonly IWarrantyExcelService excelService;
        private readonly IWarrantyRegistrationService warrantyRegistrationService;
        private readonly ILogger<WarrantyManagementController> logger;

        public WarrantyManagementController(
            ICardRegistrationRepository repo,
            IWarrantyCardRepository warrantyRepo,
            IProductRepository productRepo,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IWarrantyExcelService excelService,
            IWarrantyRegistrationService warrantyRegistrationService,
            ILogger<WarrantyManagementController> logger)
        {
            this.repo = repo;
            this.warrantyRepo = warrantyRepo;
            this.productRepo = productRepo;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.excelService = excelService;
            this.warrantyRegistrationService = warrantyRegistrationService;
            this.logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }           

        // فرم کارت گارانتی 
        [HttpGet]
        public async Task<IActionResult> Register()
        {
            var model = new RegisterationCardViewModel();

            model.CustomerTypes = await repo.GetCustomerTypesAsync();

            return View(model);
        }

        // ثبت کارت گارانتی
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterationCardViewModel request)
        {
            try
            {
                // اعتبارسنجی اولیه فرم
                if (!ModelState.IsValid)
                {
                    request.CustomerTypes = await repo.GetCustomerTypesAsync();

                    return View(request);
                }

                // بررسی انتخاب حداقل یک نوع مشتری
                if (!request.CustomerTypeIds.Any())
                {
                    ModelState.AddModelError(nameof(request.CustomerTypeIds), "حداقل یک نقش را انتخاب کنید.");

                    request.CustomerTypes = await repo.GetCustomerTypesAsync();

                    return View(request);
                }

                // انجام فرآیند ثبت گارانتی
                await warrantyRegistrationService.CardRegistration(request);

                TempData["Success"] = "ثبت گارانتی با موفقیت انجام شد.";

                return RedirectToAction(nameof(Register));
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError("", ex.Message);

                request.CustomerTypes = await repo.GetCustomerTypesAsync();

                return View(request);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error while registering warranty cards.");

                ModelState.AddModelError("", "خطایی در ثبت گارانتی رخ داد.");

                request.CustomerTypes = await repo.GetCustomerTypesAsync();

                return View(request);
            }
        }

        // آپلود اکسل
        //[HttpPost]
        //public async Task<IActionResult> UploadExcel(long ProductID, IFormFile ExcelFile)
        //{
        //    try
        //    {
        //        await excelService.ImportExcel(ProductID, ExcelFile);

        //        return Json(new { success = true, message = "آپلود موفق بود" });
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { success = false, message = ex.Message });
        //    }
        //}
        [HttpPost]
        public async Task<IActionResult> UploadExcel(long ProductID, IFormFile ExcelFile)
        {
            try
            {
                var result = await excelService.ImportExcel(ProductID, ExcelFile);

                return Json(new
                {
                    success = true,
                    insertedCount = result.InsertedCount,
                    duplicateCount = result.DuplicateCount,
                    emptyCount = result.EmptyCount
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    message = ex.Message
                });
            }
        }

        [HttpGet]
        public IActionResult WarrantyCardList(WarrantyCardSearchModel searchModel)
        {
            return ViewComponent("WarrantyCardList", searchModel);
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await productRepo.GetAll();

            return Json(products.Select(x => new
            {
                id = x.ProductID,
                name = x.ProductName
            }));
        }

        [HttpPost]
        public async Task<IActionResult> GenerateWarrantyCodes(WarrantyCodeGeneratorViewModel model)
        {
            try
            {
                await warrantyRepo.GenerateWarrantyCodes(model);

                return Json(new
                {
                    success = true,
                    message = "کدهای گارانتی با موفقیت تولید شدند."
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    message = ex.Message
                });
            }
        }
    }
}
