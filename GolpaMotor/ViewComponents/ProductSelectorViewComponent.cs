using DataAccess.Services;
using Microsoft.AspNetCore.Mvc;

namespace GolpaMotor.ViewComponents
{
    [ViewComponent(Name = "ProductSelector")]
    public class ProductSelectorViewComponent : ViewComponent
    {
        private readonly IProductRepository repo;

        public ProductSelectorViewComponent(IProductRepository repo)
        {
            this.repo = repo;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var products = await repo.GetAll();

            return View(products);
        }
    }
}
