using DomainModel.ViewModels.Warranty;

namespace GolpaMotor.FrameworkUI.Services
{
    public interface IWarrantyExcelService
    {
        //Task ImportExcel(long productId, IFormFile file);
        Task<ImportResult> ImportExcel(long productId, IFormFile file);
    }
}
