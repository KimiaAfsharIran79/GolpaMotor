using DataAccess.Services;
using DomainModel.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class CardRegistrationRepository : ICardRegistrationRepository
    {
        private readonly GolpaMotorDbContext db;

        public CardRegistrationRepository(GolpaMotorDbContext db)
        {
            this.db = db;
        }

        public async Task<WarrantyCard> GetBySerialAsync(string serial, string code)
        {
            var card = await db.WarrantyCards.FirstOrDefaultAsync(x => x.SerialNumber == serial && x.ScratchedCode == code);

            if (card == null)
                throw new InvalidOperationException("کد گارانتی معتبر نیست.");

            return card;
        }

        public async Task<List<CustomerType>> GetCustomerTypesAsync()
        {
            return await db.CustomerTypes.AsNoTracking().OrderBy(x => x.CustomerTypeID).ToListAsync();
        }

        public async Task<bool> IsRegisteredAsync(long cardId)
        {
            return await db.CardRegistrations
                .AnyAsync(x => x.WarrantyCardID == cardId);
        }

        public async Task AddRegistration(CardRegistration entity)
        {
            await db.CardRegistrations.AddAsync(entity);
        }

        public async Task AddTransaction(PointTransaction entity)
        {
            await db.PointTransactions.AddAsync(entity);
        }

        public async Task<long> GetTotalPoints(string userId)
        {
            return await db.PointTransactions
                .Where(x => x.UserID == userId)
                .SumAsync(x => (long)x.PointsAmount);
        }

        public async Task<bool> UserCustomerTypeExists(string userId, int customerTypeId)
        {
            return await db.UserCustomerTypes
                .AnyAsync(x =>
                    x.UserID == userId &&
                    x.CustomerTypeID == customerTypeId);
        }

        public async Task AddUserCustomerType(UserCustomerType entity)
        {
            await db.UserCustomerTypes.AddAsync(entity);
        }        

        //چند رکورد ذخیره شده
        public async Task<int> SaveChangesAsync()
        {
            return await db.SaveChangesAsync();
        }

        public async Task BeginTransactionAsync(Func<Task> action)
        {
            await using var transaction = await db.Database.BeginTransactionAsync();

            try
            {
                await action();

                await db.SaveChangesAsync();

                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}
