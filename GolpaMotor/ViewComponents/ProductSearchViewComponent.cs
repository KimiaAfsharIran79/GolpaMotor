using DataAccess.Services;
using DomainModel.ViewModels.Product;
using Microsoft.AspNetCore.Mvc;

namespace GolpaMotor.ViewComponents
{
    [ViewComponent(Name = "ProductSearch")]
    public class ProductSearchViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var model = new ProductSearchModel();

            return View(model);
        }
    }
}
