using DomainModel.Models;
using DomainModel.ViewModels.Warranty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Services
{
    public interface IWarrantyCardRepository
    {
        Task<List<WarrantyCardListItem>> GetAll();
        Task<bool> ExistsSerialAsync(string serialNumber);
        Task<bool> ExistsCodeAsync(string scratchedCode);
        Task<bool> ProductExistsAsync(long productId);
        Task AddRangeAsync(List<WarrantyCard> cards);
        Task SaveAsync();

        //HashSet
        Task<HashSet<string>> GetAllSerialsAsync();
        Task<HashSet<string>> GetAllCodesAsync();

        Task<List<WarrantyCardListItem>> Search(WarrantyCardSearchModel search);

        Task<string?> GetProductNameAsync(long productId);

        Task GenerateWarrantyCodes(WarrantyCodeGeneratorViewModel model);
    }
}
