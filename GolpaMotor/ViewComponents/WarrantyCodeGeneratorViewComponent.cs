using Microsoft.AspNetCore.Mvc;

namespace GolpaMotor.ViewComponents
{
    [ViewComponent(Name = "WarrantyCodeGenerator")]
    public class WarrantyCodeGeneratorViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
