using Microsoft.AspNetCore.Mvc;

namespace GolpaMotor.ViewComponents
{
    public class LoginViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }

}
