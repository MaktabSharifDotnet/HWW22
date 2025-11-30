using App.Domain.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infra.Db.SqlServer.Ef.Configorations
{
    public class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasQueryFilter(u => !u.IsDeleted);


            builder.Property(p => p.Price)
           .HasColumnType("decimal(18,2)");

            builder.HasData(
               new Product
               {
                   Id = 1,
                   Title = "گوشی موبایل آیفون 13",
                   Description = "گوشی موبایل اپل مدل iPhone 13 ظرفیت 128 گیگابایت",
                   Price = 45000000M,
                   CreatedAt = new DateTime(2025, 1, 1),
                   Inventory = 5, 
                   CategoryId = 1, 
                   Image = "iphone13.jpg",
                   IsDeleted = false
               },
               new Product
               {
                   Id = 2,
                   Title = "لپ تاپ ایسوس",
                   Description = "لپ تاپ 15.6 اینچی ایسوس مدل TUF Gaming",
                   Price = 38000000M,
                   CreatedAt = new DateTime(2025, 1, 7),
                   Inventory = 0, 
                   CategoryId = 1, 
                   Image = "asus_laptop.jpg",
                   IsDeleted = false
               },
               new Product
               {
                   Id = 3,
                   Title = "کفش ورزشی نایکی",
                   Description = "کفش مخصوص دویدن مردانه مدل Revolution 6",
                   Price = 2500000M,
                   CreatedAt = new DateTime(2025, 1, 6),
                   Inventory = 20,
                   CategoryId = 2, 
                   Image = "nike_shoes.jpg",
                   IsDeleted = false
               },
               new Product
               {
                   Id = 4,
                   Title = "یخچال فریزر دوقلو",
                   Description = "یخچال و فریزر دوقلو 40 فوت پارس",
                   Price = 22000000M,
                   CreatedAt = new DateTime(2025, 1, 5),
                   Inventory = 2,
                   CategoryId = 3, 
                   Image = "fridge.jpg",
                   IsDeleted = false
               },
               new Product
               {
                   Id = 5,
                   Title = "کتاب Clean Code",
                   Description = "کتاب کدنویسی تمیز اثر رابرت سی. مارتین (مرجع برنامه‌نویسان)",
                   Price = 450000M,
                   CreatedAt = new DateTime(2025, 1, 4),
                   Inventory = 100,
                   CategoryId = 4, 
                   Image = "clean_code_book.jpg",
                   IsDeleted = false
               },
                 new Product
                 {
                     Id = 6,
                     Title = "چادر مسافرتی 4 نفره",
                     Description = "چادر مسافرتی فنری ضد آب مناسب برای کمپینگ و طبیعت‌گردی",
                     Price = 1800000M,
                     CreatedAt = new DateTime(2025, 1, 3),
                     Inventory = 10,
                     CategoryId = 5, 
                     Image = "camping_tent.jpg",
                     IsDeleted = false
                 },
                new Product
                {
                    Id = 7,
                    Title = "کوله پشتی کوهنوردی",
                    Description = "کوله پشتی 50 لیتری مناسب برای کوهنوردی و سفر چند روزه",
                    Price = 3500000M,
                    CreatedAt = new DateTime(2025, 1, 2),
                    Inventory = 8,
                    CategoryId = 5, 
                    Image = "backpack.jpg",
                    IsDeleted = false
                }
           );

          

         
        }
    }
}
