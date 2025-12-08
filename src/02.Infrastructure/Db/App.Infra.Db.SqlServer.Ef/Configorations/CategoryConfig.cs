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
    public class CategoryConfig : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasQueryFilter(c => !c.IsDeleted);

            builder.HasData(
        new Category
        {
            Id = 1,
            Name = "کالای دیجیتال",
            Description = "شامل انواع موبایل، لپ‌تاپ، تبلت و لوازم جانبی هوشمند",
            IsDeleted = false
        },
        new Category
        {
            Id = 2,
            Name = "پوشاک و مد",
            Description = "انواع لباس‌های مردانه، زنانه، بچگانه و اکسسوری‌های روز",
            IsDeleted = false
        },
        new Category
        {
            Id = 3,
            Name = "لوازم خانگی",
            Description = "تجهیزات برقی آشپزخانه، دکوراسیون و وسایل کاربردی منزل",
            IsDeleted = false
        },
        new Category
        {
            Id = 4,
            Name = "کتاب و لوازم تحریر",
            Description = "کتاب‌های آموزشی، رمان، مجلات و انواع نوشت‌افزار",
            IsDeleted = false
        },
        new Category
        {
            Id = 5,
            Name = "ورزش و سفر",
            Description = "تجهیزات تخصصی ورزشی، لباس ورزشی و لوازم کمپینگ و سفر",
            IsDeleted = false
        }
    );



            builder.HasMany(c=>c.Products)
                .WithOne(p=>p.Category)
                .HasForeignKey(P=>P.CategoryId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
