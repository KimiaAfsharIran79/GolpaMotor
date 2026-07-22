using DomainModel.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.DataSeeder
{
    public static class WarrantyCardSeeder
    {
        public static async Task SeedAsync(IServiceProvider services)
        {
            var context = services.GetRequiredService<GolpaMotorDbContext>();

            if (context.WarrantyCards.Any())
                return;

            var products = new[]
            {
                (1L, "سرسیلندر"),
                (2L, "باتری ماشین"),
                (3L, "هدلایت"),
                (4L, "فیلتر هوا"),
                (5L, "فیلتر کابین"),
                (6L, "فیلتر روغن"),
                (7L, "روغن موتور"),
                (8L, "دینام"),
                (9L, "تسمه دینام"),
                (10L, "تسمه تایم"),
                (11L, "استارت"),
                (12L, "ترموستات"),
                (13L, "گیربکس"),
                (14L, "پیستون"),
                (15L, "پمپ بنزین"),
                (16L, "بلبرینگ چرخ"),
                (17L, "میل لنگ"),
                (18L, "دسته موتور"),
                (19L, "موتور"),
                (20L, "شاتون")
            };

            var cards = new List<WarrantyCard>();

            long serial = 100000000001;
            long warrantyCode = 200000000001;

            foreach (var product in products)
            {
                for (int i = 0; i < 3; i++)
                {
                    cards.Add(new WarrantyCard
                    {
                        ProductID = product.Item1,
                        SerialNumber = serial.ToString(),
                        ScratchedCode = warrantyCode.ToString(),
                        ValidityMonths = 12,
                        IsRegistered = false,
                        Description = $"گارانتی {product.Item2}"
                    });

                    serial++;
                    warrantyCode++;
                }
            }

            await context.WarrantyCards.AddRangeAsync(cards);
            await context.SaveChangesAsync();
        }
    }
}
