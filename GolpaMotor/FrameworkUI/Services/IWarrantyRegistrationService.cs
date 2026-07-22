using GolpaMotor.Models.ViewModels.WarrantyManagement;

namespace GolpaMotor.FrameworkUI.Services
{
    public interface IWarrantyRegistrationService
    {
        Task CardRegistration(RegisterationCardViewModel request);
    }
}
