using DomainModel.Models;
using Microsoft.Extensions.DependencyInjection;

namespace DomainModel.DataSeeder
{
    public static class ProductSeeder
    {
        public static async Task SeedAsync(IServiceProvider services)
        {
            var context = services.GetRequiredService<GolpaMotorDbContext>();

            if (context.Products.Any())
                return;

            var items = new List<Product>
            {
                new() {
                    ProductName="سرسیلندر",
                    Description="داخلی",
                    ImageUrl="0239e58e_1c70_44c3_a456_ce15d7a5e347_cylinder-head.jpg",
                    IsDeleted=false,
                    ProductPoint=3,
                    IsAvailable=true
                },

                new() {
                    ProductName="باتری ماشین",
                    Description="داخلی",
                    ImageUrl="383113da_0784_486d_92f7_3068be1d735a_car-battery.jpg",
                    IsDeleted=false,
                    ProductPoint=2,
                    IsAvailable=true
                },

                new() {
                    ProductName="هدلایت",
                    Description="ایرانی",
                    ImageUrl="2c361791_0d54_4141_bffa_4c17f0ddd7bd_headlight.jpg",
                    IsDeleted=false,
                    ProductPoint=2,
                    IsAvailable=false
                },

                new() {
                    ProductName="فیلتر هوا",
                    Description="ایرانی",
                    ImageUrl="f227a94a_e7c3_4ce2_94f5_70ed9e631121_Air-Filter (2).jpg",
                    IsDeleted=false,
                    ProductPoint=1,
                    IsAvailable=false
                },

                new() {
                    ProductName="فیلتر کابین",
                    Description="ایرانی",
                    ImageUrl="efe21237_1594_4327_b6f8_7e90d071c66d_cabin-filter.jpg",
                    IsDeleted=false,
                    ProductPoint=2,
                    IsAvailable=true
                },

                new() {
                    ProductName="فیلتر روغن",
                    Description="ایرانی",
                    ImageUrl="add574db_9fc9_4fc6_8b4d_08e6bf1cebd7_oil-filter.jpg",
                    IsDeleted=false,
                    ProductPoint=2,
                    IsAvailable=true
                },

                new() {
                    ProductName="روغن موتور",
                    Description="ایرانی",
                    ImageUrl="d0132b75_309d_49d5_a31b_c0718d5bd368_engine-oil.jpg",
                    IsDeleted=false,
                    ProductPoint=1,
                    IsAvailable=true
                },

                new() {
                    ProductName="دینام",
                    Description="محصول مشترک",
                    ImageUrl="f9b50f6f_91c6_465b_85bb_194f266ad8ce_alternator.jpg",
                    IsDeleted=false,
                    ProductPoint=4,
                    IsAvailable=true
                },

                new() {
                    ProductName="تسمه دینام",
                    Description="خارجی",
                    ImageUrl="566ac4f6_38f2_473d_8a78_49f6c8b90aeb_alternator-belt.jpg",
                    IsDeleted=false,
                    ProductPoint=2,
                    IsAvailable=true
                },

                new() {
                    ProductName="تسمه تایم",
                    Description="چینی",
                    ImageUrl="b249bd82_f6b4_4f5b_b1f1_9590eb8f0577_timing-belt.jpg",
                    IsDeleted=false,
                    ProductPoint=2,
                    IsAvailable=true
                },

                new() {
                    ProductName="استارت",
                    Description="داخلی",
                    ImageUrl="a3be9709_dea3_45c2_a345_00893f948c24_starter-motor.jpg",
                    IsDeleted=false,
                    ProductPoint=1,
                    IsAvailable=true
                },

                new() {
                    ProductName="ترموستات",
                    Description="داخلی",
                    ImageUrl="337ad25a_4667_4f08_8cee_10857864943a_thermostat.jpg",
                    IsDeleted=false,
                    ProductPoint=3,
                    IsAvailable=true
                },

                new() {
                    ProductName="گیربکس",
                    Description="محصول مشترک ایران و کره",
                    ImageUrl="524b9017_11e3_47d8_9eed_7301a5931b61_gear-box.jpg",
                    IsDeleted=false,
                    ProductPoint=4,
                    IsAvailable=true
                },

                new() {
                    ProductName="پیستون",
                    Description="ایرانی",
                    ImageUrl="a160f9ef_f6c4_40fe_a3a9_4aca42660892_piston.jpg",
                    IsDeleted=false,
                    ProductPoint=1,
                    IsAvailable=true
                },

                new() {
                    ProductName="پمپ بنزین",
                    Description="چینی",
                    ImageUrl="275e3e53_8688_44ff_91a6_20946c2e6cad_fuel-pump.jpg",
                    IsDeleted=false,
                    ProductPoint=2,
                    IsAvailable=false
                },

                new() {
                    ProductName="بلبرینگ چرخ",
                    Description="چینی",
                    ImageUrl="4a21fc07_5255_4942_bd79_269375a8b038_wheel-bearing.jpg",
                    IsDeleted=false,
                    ProductPoint=2,
                    IsAvailable=true
                },

                new() {
                    ProductName="میل لنگ",
                    Description="محصول کره",
                    ImageUrl="6cb40c8f_3e31_48a9_86aa_49be90915997_crankshaft.jpg",
                    IsDeleted=false,
                    ProductPoint=3,
                    IsAvailable=true
                },

                new() {
                    ProductName="دسته موتور",
                    Description="ایرانی",
                    ImageUrl="d68595ee_aa43_447c_b15b_b0a1f589aad9_turn-signal-switch.jpg",
                    IsDeleted=false,
                    ProductPoint=2,
                    IsAvailable=true
                },

                new() {
                    ProductName="موتور",
                    Description="چینی",
                    ImageUrl="662c5f7f_e440_45da_9b03_01bffd476f7c_car-engine.jpg",
                    IsDeleted=false,
                    ProductPoint=5,
                    IsAvailable=true
                },

                new() {
                    ProductName="شاتون",
                    Description="کره",
                    ImageUrl="011c359d_37b7_41d7_a425_4262beff6188_connectingRod.jpg",
                    IsDeleted=false,
                    ProductPoint=5,
                    IsAvailable=false
                },

                new() {
                    ProductName="لنت ترمز",
                    Description="چینی",
                    ImageUrl="82d2da87_6b2f_42f6_b765_5512d76225f7_brake-pad.jpg",
                    IsDeleted=false,
                    ProductPoint=4,
                    IsAvailable=true
                },

                new() {
                    ProductName="پمپ آب",
                    Description="چینی",
                    ImageUrl="cffe355b_758f_454c_a7e6_895aea7136e2_waterpump.jpg",
                    IsDeleted=false,
                    ProductPoint=4,
                    IsAvailable=false
                },

                new() {
                    ProductName="سرپلوس",
                    Description="ایرانی",
                    ImageUrl="795d1573_2293_42e4_9242_1692080f5269_cv-joint.jpg",
                    IsDeleted=false,
                    ProductPoint=4,
                    IsAvailable=true
                },

                new() {
                    ProductName="کوئل",
                    Description="ایرانی",
                    ImageUrl="4e6bb24f_4ffb_45f7_b557_042018c28f82_ignition-coil.jpg",
                    IsDeleted=false,
                    ProductPoint=4,
                    IsAvailable=true
                },

                new() {
                    ProductName="واشر سرسیلندر",
                    Description="خارجی",
                    ImageUrl="93096157_2459_4d61_a38e_c22976b0f46c_cylanderHead.jpg",
                    IsDeleted=false,
                    ProductPoint=3,
                    IsAvailable=true
                },

                new() {
                    ProductName="فیلتر هوا",
                    Description="ایرانی",
                    ImageUrl=null,
                    IsDeleted=false,
                    ProductPoint=1,
                    IsAvailable=true
                },

                new() {
                    ProductName="رادیاتور",
                    Description="چینی",
                    ImageUrl="81e20eab_eb96_4493_a03b_4f5e37a19dcf_radiator.jpg",
                    IsDeleted=false,
                    ProductPoint=5,
                    IsAvailable=true
                }
            };

            await context.Products.AddRangeAsync(items);
            await context.SaveChangesAsync();
        }
    }
}