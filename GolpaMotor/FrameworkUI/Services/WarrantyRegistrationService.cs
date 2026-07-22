using DataAccess.Services;
using DomainModel.Models;
using GolpaMotor.Models.ViewModels.WarrantyManagement;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GolpaMotor.FrameworkUI.Services
{
    public class WarrantyRegistrationService : IWarrantyRegistrationService
    {
        private readonly ICardRegistrationRepository repo;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public WarrantyRegistrationService(
            ICardRegistrationRepository repo,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            this.repo = repo;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        private static string NormalizeName(string? value)
        {
            return (value ?? "")
                .Trim()
                .Replace("ي", "ی")
                .Replace("ك", "ک")
                .Replace("‌", "");
        }

        public async Task CardRegistration(RegisterationCardViewModel request)
        {
            // اعتبارسنجی تمام کارت‌های وارد شده
            var warrantyCards = new List<WarrantyCard>();

            foreach (var item in request.Cards)
            {
                if (string.IsNullOrWhiteSpace(item.SerialNumber) ||
                    string.IsNullOrWhiteSpace(item.ScratchedCode))
                {
                    throw new InvalidOperationException("تمام کارت‌ها باید شماره سریال و کد گارانتی داشته باشند.");
                }

                var card = await repo.GetBySerialAsync(item.SerialNumber,item.ScratchedCode);

                if (await repo.IsRegisteredAsync(card.WarrantyCardID))
                {
                    throw new InvalidOperationException($"کارت با سریال {item.SerialNumber} قبلاً ثبت شده است.");
                }

                warrantyCards.Add(card);
            }            

            // پیدا کردن کاربر با شماره موبایل
            var user = await userManager.Users
                .FirstOrDefaultAsync(x => x.PhoneNumber == request.CustomerPhoneNumber);

            // در صورت نبود کاربر، ایجاد حساب کاربری جدید
            if (user == null)
            {
                user = new ApplicationUser
                {
                    UserName = request.CustomerPhoneNumber,
                    PhoneNumber = request.CustomerPhoneNumber,
                    FirstName = NormalizeName(request.FirstName),
                    LastName = NormalizeName(request.LastName),
                    EmailConfirmed = true,
                    IsActive = true,
                    IsConfirmedCode = true
                };

                var createResult = await userManager.CreateAsync(user);

                if (!createResult.Succeeded)
                {
                    throw new InvalidOperationException(
                        string.Join(" ، ", createResult.Errors.Select(x => x.Description)));
                }

                // اختصاص نقش Customer به کاربر
                await userManager.AddToRoleAsync(user, "Customer");
            }
            else
            {
                var currentFirstName = NormalizeName(user.FirstName);
                var currentLastName = NormalizeName(user.LastName);

                var enteredFirstName = NormalizeName(request.FirstName);
                var enteredLastName = NormalizeName(request.LastName);

                if (currentFirstName != enteredFirstName ||
                    currentLastName != enteredLastName)
                {
                    throw new InvalidOperationException(
                        $"این شماره موبایل قبلاً با نام «{user.FirstName} {user.LastName}» ثبت شده است.");
                }

                // اگر کاربر نقش Customer نداشته باشد، به او اختصاص داده می‌شود
                if (!await userManager.IsInRoleAsync(user, "Customer"))
                {
                    await userManager.AddToRoleAsync(user, "Customer");
                }
            }

            // شناسه کاربر
            var userId = user.Id;

            // اجرای عملیات ثبت در یک تراکنش
            await repo.BeginTransactionAsync(async () =>
            {
                // ثبت تمام کارت‌های گارانتی و امتیاز هر کارت
                foreach (var card in warrantyCards)
                {
                    card.IsRegistered = true;

                    await repo.AddRegistration(new CardRegistration
                    {
                        WarrantyCardID = card.WarrantyCardID,
                        UserID = userId,
                        SerialNumber = card.SerialNumber,
                        ScratchedCode = card.ScratchedCode,
                        CustomerPhoneNumber = request.CustomerPhoneNumber,
                        CreatedAt = DateTime.UtcNow
                    });

                    await repo.AddTransaction(new PointTransaction
                    {
                        UserID = userId,
                        PointsAmount = 10,
                        PointTransactionDate = DateTime.UtcNow,
                        Description = $"ثبت کارت گارانتی {card.SerialNumber}"
                    });
                }

                // ثبت نوع(های) مشتری
                foreach (var typeId in request.CustomerTypeIds.Distinct())
                {
                    var exists = await repo.UserCustomerTypeExists(userId, typeId);

                    if (!exists)
                    {
                        await repo.AddUserCustomerType(new UserCustomerType
                        {
                            UserID = userId,
                            CustomerTypeID = typeId
                        });
                    }
                }

                // ذخیره تمام تغییرات
                await repo.SaveChangesAsync();
            });
        }
    }
}
