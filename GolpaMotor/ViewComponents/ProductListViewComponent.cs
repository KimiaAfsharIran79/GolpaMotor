using DataAccess.Services;
using DomainModel.ViewModels.Product;
using Microsoft.AspNetCore.Mvc;

namespace GolpaMotor.ViewComponents
{
    [ViewComponent(Name = "ProductList")]
    public class ProductListViewComponent : ViewComponent
    {
        private readonly IProductRepository repo;
        public ProductListViewComponent(IProductRepository repo)
        {
            this.repo = repo;
        }
        //public async Task<IViewComponentResult> InvokeAsync()
        //{
        //    var products = await repo.GetAll();
        //    return View(products);
        //}

        public async Task<IViewComponentResult> InvokeAsync(ProductSearchModel? searchModel)
        {
            searchModel ??= new ProductSearchModel();

            var model = await repo.Search(searchModel);

            return View(model);
        }
    }
}
