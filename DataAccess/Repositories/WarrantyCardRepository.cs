using DataAccess.Services;
using DomainModel.Models;
using DomainModel.ViewModels.Warranty;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class WarrantyCardRepository : IWarrantyCardRepository
    {
        private readonly GolpaMotorDbContext db;

        public WarrantyCardRepository(GolpaMotorDbContext db)
        {
            this.db = db;
        }

        public async Task<List<WarrantyCardListItem>> GetAll()
        {
            return await db.WarrantyCards
                .Include(x => x.Product)
                .Select(x => new WarrantyCardListItem
                {
                    WarrantyCardID = x.WarrantyCardID,
                    ProductName = x.Product.ProductName,
                    SerialNumber = x.SerialNumber,
                    WarrantyCode = x.ScratchedCode,
                    IsActive = !x.IsRegistered
                })
                .ToListAsync();
        }

        public async Task<bool> ExistsSerialAsync(string serialNumber)
        {
            return await db.WarrantyCards.AnyAsync(x => x.SerialNumber == serialNumber);
        }


        public async Task<bool> ExistsCodeAsync(string scratchedCode)
        {
            return await db.WarrantyCards.AnyAsync(x => x.ScratchedCode == scratchedCode);
        }       


        public async Task<bool> ProductExistsAsync(long productId)
        {
            return await db.Products.AnyAsync(x => x.ProductID == productId && !x.IsDeleted);
        }


        public async Task AddRangeAsync(List<WarrantyCard> cards)
        {
            await db.WarrantyCards.AddRangeAsync(cards);
        }        

        public async Task SaveAsync()
        {
            await db.SaveChangesAsync();
        }

        //HashSet
        public async Task<HashSet<string>> GetAllSerialsAsync()
        {
            return (await db.WarrantyCards.AsNoTracking().Select(x => x.SerialNumber).ToListAsync()).ToHashSet();
        }

        public async Task<HashSet<string>> GetAllCodesAsync()
        {
            return (await db.WarrantyCards.AsNoTracking().Select(x => x.ScratchedCode).ToListAsync()).ToHashSet();
        }

        public async Task<List<WarrantyCardListItem>> Search(WarrantyCardSearchModel search)
        {
            search ??= new WarrantyCardSearchModel();

            Console.WriteLine("PageIndex : " + search.PageIndex);
            Console.WriteLine("PageSize : " + search.PageSize);

            if (search.PageSize <= 0)
                search.PageSize = 10;

            if (search.PageIndex < 0)
                search.PageIndex = 0;

            var query = db.WarrantyCards.Include(x => x.Product).AsQueryable();

            if (!string.IsNullOrWhiteSpace(search.Search))
            {
                var text = search.Search.Trim();

                query = query.Where(x => x.SerialNumber.Contains(text) || x.ScratchedCode.Contains(text));
            }


            if (search.ProductID.HasValue)
            {
                query = query.Where(x => x.ProductID == search.ProductID.Value);
            }


            if (search.IsRegistered.HasValue)
            {
                query = query.Where(x => x.IsRegistered == search.IsRegistered.Value);
            }

            search.RecordCount = await query.CountAsync();

            return await query.Skip(search.PageIndex * search.PageSize).Take(search.PageSize)
                .Select(x => new WarrantyCardListItem
                {
                    WarrantyCardID = x.WarrantyCardID,
                    ProductName = x.Product.ProductName,
                    SerialNumber = x.SerialNumber,
                    WarrantyCode = x.ScratchedCode,
                    IsActive = !x.IsRegistered
                })
            .ToListAsync();
        }

        public async Task<string?> GetProductNameAsync(long productId)
        {
            return await db.Products
                .Where(x => x.ProductID == productId)
                .Select(x => x.ProductName)
                .FirstOrDefaultAsync();
        }

        public async Task GenerateWarrantyCodes(WarrantyCodeGeneratorViewModel model)
        {
            if (!await ProductExistsAsync(model.ProductID))
                throw new Exception("محصول انتخاب شده وجود ندارد.");

            if (model.SerialFrom == 0)
                throw new Exception("شماره سریال از را وارد کنید.");

            if (model.SerialTo == 0)
                throw new Exception("شماره سریال تا را وارد کنید.");

            if (model.CodeFrom == 0)
                throw new Exception("کد گارانتی از را وارد کنید.");

            if (model.CodeTo == 0)
                throw new Exception("کد گارانتی تا را وارد کنید.");

            if (model.Count <= 0)
                throw new Exception("تعداد باید بیشتر از صفر باشد.");

            if (model.SerialFrom > model.SerialTo)
                throw new Exception("بازه شماره سریال نامعتبر است.");

            if (model.CodeFrom > model.CodeTo)
                throw new Exception("بازه کد گارانتی نامعتبر است.");


            long serialRange = model.SerialTo - model.SerialFrom + 1;
            long codeRange = model.CodeTo - model.CodeFrom + 1;

            if (model.Count > serialRange)
                throw new Exception("تعداد درخواستی از بازه شماره سریال بیشتر است.");

            if (model.Count > codeRange)
                throw new Exception("تعداد درخواستی از بازه کد گارانتی بیشتر است.");


            //تولید اعداد تصادفی
            var random = new Random();

            var cards = new List<WarrantyCard>();

            var existingSerials = await GetAllSerialsAsync();
            var existingCodes = await GetAllCodesAsync();

            int availableSerials = 0;

            for (long i = model.SerialFrom; i <= model.SerialTo; i++)
            {
                if (!existingSerials.Contains(i.ToString()))
                    availableSerials++;
            }

            if (availableSerials < model.Count)
                throw new Exception("بازه شماره سریال انتخاب‌شده ظرفیت کافی برای تولید ندارد.");



            int availableCodes = 0;

            for (long i = model.CodeFrom; i <= model.CodeTo; i++)
            {
                if (!existingCodes.Contains(i.ToString()))
                    availableCodes++;
            }

            if (availableCodes < model.Count)
                throw new Exception("بازه کد گارانتی انتخاب‌شده ظرفیت کافی برای تولید ندارد.");

            var productName = await GetProductNameAsync(model.ProductID);

            for (int i = 0; i < model.Count; i++)
            {
                string serial;
                string code;

                do
                {
                    serial = random.NextInt64(model.SerialFrom, model.SerialTo + 1).ToString();
                }
                while (existingSerials.Contains(serial));

                existingSerials.Add(serial);

                do
                {
                    code = random.NextInt64(model.CodeFrom, model.CodeTo + 1).ToString();
                }
                while (existingCodes.Contains(code));

                existingCodes.Add(code);

                cards.Add(new WarrantyCard
                {
                    ProductID = model.ProductID,
                    SerialNumber = serial,
                    ScratchedCode = code,
                    ValidityMonths = model.ValidityMonths,
                    Description = $"گارانتی {productName}",
                    IsRegistered = false
                });
            }
            await AddRangeAsync(cards);
            await SaveAsync();
        }
    }
}
