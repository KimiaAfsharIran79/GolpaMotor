using DataAccess.Services;
using DomainModel.Models;
using DomainModel.ViewModels.Warranty;
using OfficeOpenXml;

namespace GolpaMotor.FrameworkUI.Services
{
    public class WarrantyExcelService : IWarrantyExcelService
    {
        private readonly IWarrantyCardRepository repo;

        public WarrantyExcelService(IWarrantyCardRepository repo)
        {
            this.repo = repo;
        }

        //public async Task ImportExcel(long productId, IFormFile file)
        //{
        //    // فایل انتخاب نشده
        //    if (file == null || file.Length == 0)
        //        throw new Exception("فایل انتخاب نشده است");

        //    // وجود محصول
        //    if (!await repo.ProductExistsAsync(productId))
        //        throw new Exception("محصول یافت نشد");

        //    //باز شدن فایل اکسل با EPPlus
        //    using var package = new ExcelPackage(file.OpenReadStream());

        //    var worksheet = package.Workbook.Worksheets.FirstOrDefault();

        //    if (worksheet == null)
        //        throw new Exception("فایل اکسل معتبر نیست");

        //    // بررسی هدر فایل
        //    var header1 = worksheet.Cells[1, 1].Text.Trim().ToLower();
        //    var header2 = worksheet.Cells[1, 2].Text.Trim().ToLower(); 

        //    if (header1 != "serialnumber" ||
        //        header2 != "scratchedcode")
        //    {
        //        throw new Exception("فرمت فایل اکسل صحیح نیست");
        //    }

        //    if (worksheet.Dimension == null)
        //        throw new Exception("فایل اکسل خالی است");

        //    int rowCount = worksheet.Dimension.Rows;
        //    if (rowCount < 2)
        //        throw new Exception("فایل اکسل هیچ داده‌ای ندارد");

        //    var list = new List<WarrantyCard>();

        //    var existingSerials = await repo.GetAllSerialsAsync();
        //    var existingCodes = await repo.GetAllCodesAsync();

        //    var fileSerials = new HashSet<string>();
        //    var fileCodes = new HashSet<string>();

        //    var productName = await repo.GetProductNameAsync(productId);
        //    for (int row = 2; row <= rowCount; row++)
        //    {
        //        var serial = worksheet.Cells[row, 1].Text.Trim();
        //        var code = worksheet.Cells[row, 2].Text.Trim();

        //        // سلول خالی
        //        if (string.IsNullOrWhiteSpace(serial) ||
        //            string.IsNullOrWhiteSpace(code))
        //            continue;

        //        // تکراری در همان فایل
        //        if (!fileSerials.Add(serial))
        //            continue;

        //        if (!fileCodes.Add(code))
        //            continue;

        //        // تکراری در دیتابیس
        //        if (existingSerials.Contains(serial))
        //            continue;

        //        if (existingCodes.Contains(code))
        //            continue;

        //        list.Add(new WarrantyCard
        //        {
        //            ProductID = productId,
        //            SerialNumber = serial,
        //            ScratchedCode = code,
        //            ValidityMonths = 12,
        //            IsRegistered = false,
        //            Description = $"گارانتی {productName}"
        //        });
        //    }

        //    if (list.Count == 0)
        //        throw new Exception("رکورد جدیدی برای ثبت یافت نشد");

        //    await repo.AddRangeAsync(list);

        //    await repo.SaveAsync();
        //}

        public async Task<ImportResult> ImportExcel(long productId, IFormFile file)
        {
            // فایل انتخاب نشده
            if (file == null || file.Length == 0)
                throw new Exception("فایل انتخاب نشده است");

            // وجود محصول
            if (!await repo.ProductExistsAsync(productId))
                throw new Exception("محصول یافت نشد");

            //باز شدن فایل اکسل با EPPlus
            using var package = new ExcelPackage(file.OpenReadStream());

            var worksheet = package.Workbook.Worksheets.FirstOrDefault();

            if (worksheet == null)
                throw new Exception("فایل اکسل معتبر نیست");

            // بررسی هدر فایل
            var header1 = worksheet.Cells[1, 1].Text.Trim().ToLower();
            var header2 = worksheet.Cells[1, 2].Text.Trim().ToLower();

            if (header1 != "serialnumber" ||
                header2 != "scratchedcode")
            {
                throw new Exception("فرمت فایل اکسل صحیح نیست");
            }

            if (worksheet.Dimension == null)
                throw new Exception("فایل اکسل خالی است");

            int rowCount = worksheet.Dimension.Rows;
            if (rowCount < 2)
                throw new Exception("فایل اکسل هیچ داده‌ای ندارد");

            var list = new List<WarrantyCard>();

            int duplicateCount = 0;
            int emptyCount = 0;

            var existingSerials = await repo.GetAllSerialsAsync();
            var existingCodes = await repo.GetAllCodesAsync();

            var fileSerials = new HashSet<string>();
            var fileCodes = new HashSet<string>();

            var productName = await repo.GetProductNameAsync(productId);
            for (int row = 2; row <= rowCount; row++)
            {
                var serial = worksheet.Cells[row, 1].Text.Trim();
                var code = worksheet.Cells[row, 2].Text.Trim();

                // سلول خالی
                if (string.IsNullOrWhiteSpace(serial) ||
                    string.IsNullOrWhiteSpace(code))
                {
                    emptyCount++;
                    continue;
                }

                // تکراری در همان فایل
                if (!fileSerials.Add(serial))
                {
                    duplicateCount++;
                    continue;
                }

                if (!fileCodes.Add(code))
                {
                    duplicateCount++;
                    continue;
                }

                // تکراری در دیتابیس
                if (existingSerials.Contains(serial))
                {
                    Console.WriteLine($"DUPLICATE SERIAL: {serial}");
                    duplicateCount++;
                    continue;
                }

                if (existingCodes.Contains(code))
                {
                    duplicateCount++;
                    continue;
                }

                Console.WriteLine($"ADDING: {serial}");

                list.Add(new WarrantyCard
                {
                    ProductID = productId,
                    SerialNumber = serial,
                    ScratchedCode = code,
                    ValidityMonths = 12,
                    IsRegistered = false,
                    Description = $"گارانتی {productName}"
                });
            }

            if (list.Count > 0)
            {
                await repo.AddRangeAsync(list);
                await repo.SaveAsync();
            }

            return new ImportResult
            {
                InsertedCount = list.Count,
                DuplicateCount = duplicateCount,
                EmptyCount = emptyCount
            };
        }
    }
}
