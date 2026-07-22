using DataAccess.Services;
using DomainModel.ViewModels.Warranty;
using Microsoft.AspNetCore.Mvc;

namespace GolpaMotor.ViewComponents
{
    [ViewComponent(Name = "WarrantyCardList")]
    public class WarrantyCardListViewComponent : ViewComponent
    {
        private readonly IWarrantyCardRepository repo;

        public WarrantyCardListViewComponent(IWarrantyCardRepository repo)
        {
            this.repo = repo;
        }

        public async Task<IViewComponentResult> InvokeAsync(WarrantyCardSearchModel? searchModel)
        {
            searchModel ??= new WarrantyCardSearchModel();

            var model = new WarrantyListComplexModel();

            model.sm = searchModel;

            model.warrantyList = await repo.Search(searchModel);

            return View(model);
        }
    }
}
