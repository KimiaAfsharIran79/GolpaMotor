using DataAccess.Services;
using Microsoft.AspNetCore.Mvc;

namespace GolpaMotor.ViewComponents
{
    [ViewComponent(Name = "ProductStatistics")]
    public class ProductStatisticsViewComponent : ViewComponent
    {
        private readonly IProductRepository repo;

        public ProductStatisticsViewComponent(IProductRepository repo)
        {
            this.repo = repo;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = await repo.GetStatistics();

            return View(model);
        }
    }
}
