using DomainModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Services
{
    public interface ICardRegistrationRepository
    {
        Task<WarrantyCard> GetBySerialAsync(string serial, string code);
        Task<List<CustomerType>> GetCustomerTypesAsync();
        Task<bool> IsRegisteredAsync(long cardId);
        Task AddRegistration(CardRegistration entity);
        Task AddTransaction(PointTransaction entity);
        Task<long> GetTotalPoints(string userId);
        Task<bool> UserCustomerTypeExists(string userId, int customerTypeId);
        Task AddUserCustomerType(UserCustomerType entity);

        //چند تا رکورد ذخیره شده
        Task<int> SaveChangesAsync();

        //Transaction
        Task BeginTransactionAsync(Func<Task> action);
    }
}
