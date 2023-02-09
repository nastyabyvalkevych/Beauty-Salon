//using Microsoft.EntityFrameworkCore;
//using Salon.Models;

//namespace Salon.Infrastructure
//{
//    public class SeedData
//    {
//        public static void SeedDatabase(DataContext context)
//        {
//            context.Database.Migrate();

//            if (!context.Procedures.Any())
//            {
//                Category manicure = new Category { Name = "Манікюр", Slug = "манікюр" };
//                Category massage = new Category { Name = "Косметологія-масаж", Slug = "косметологія" };
//                Category stylist = new Category { Name = "Стиліст", Slug = "стиліст" };
//                Category makeup = new Category { Name = "Візаж", Slug = "макіяж" };

//                context.Procedures.AddRange(
//                        new Procedure
//                        {
//                            Name = "Відновлення волосся OLAPLEX",
//                            Slug = "стрижки",
//                            Description = "За допомогою нової технології відновимо найсухіше волосся. Час: 1год 30хв",
//                            Price = 2300M,
//                            Category = stylist,
//                            Image = "hair1.jpg"
//                        },
//                        new Procedure
//                        {
//                            Name = "Голівудські локони",
//                            Slug = "моделювання",
//                            Description = "Потрібна сучасна, гарна зачіска на вечір? Це точно для вас! Час: 1год 00хв",
//                            Price = 800M,
//                            Category = stylist,
//                            Image = "hair2.jpg"
//                        },
//                        new Procedure
//                        {
//                            Name = "Пілінг",
//                            Slug = "обличчя",
//                            Description = "Пілінг ніжно очистить шкіру від забруднень. Час: 50хв",
//                            Price = 900M,
//                            Category = massage,
//                            Image = "face1.jpg"
//                        },
//                        new Procedure
//                        {
//                            Name = "Догляд за обличчям",
//                            Slug = "обличчя",
//                            Description = "Це нова особиста наша процедура в якій ми використовуємо маски спуцільно під ваш тип шкіри. Час: 1год 00хв",
//                            Price = 1200M,
//                            Category = massage,
//                            Image = "face2.jpg"
//                        },
//                        new Procedure
//                        {
//                            Name = "Класичний манікюр",
//                            Slug = "нігті",
//                            Description = "",
//                            Price = 500M,
//                            Category = manicure,
//                            Image = "manic1.jpg"
//                        },
//                        new Procedure
//                        {
//                            Name = "Масаж спини",
//                            Slug = "масаж",
//                            Description = "Розімнуть спину і буде бобо",
//                            Price = 500M,
//                            Category = massage,
//                            Image = "skin1.jpg"
//                        },
//                        new Procedure
//                        {
//                            Name = "Базовий макіж денний",
//                            Slug = "макіяж",
//                            Description = "Макіяж створений для денної зустрічі в спокійни коричневих відтінках",
//                            Price = 600M,
//                            Category = makeup,
//                            Image = "makeup1.jpg"
//                        },
//                        new Procedure
//                        {
//                            Name = "Вечірній макіяж",
//                            Slug = "макіяж",
//                            Description = "Макіяж створений для денної зустрічі в спокійни коричневих відтінках",
//                            Price = 1200M,
//                            Category = makeup,
//                            Image = "makeup2.jpg"
//                        }
//                );

//                context.SaveChanges();
//            }
//        }
//    }
//}
