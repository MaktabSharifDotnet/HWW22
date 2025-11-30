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
              new Category { Id = 1, Name = "کالای دیجیتال", IsDeleted = false },
              new Category { Id = 2, Name = "پوشاک و مد", IsDeleted = false },
              new Category { Id = 3, Name = "لوازم خانگی", IsDeleted = false },
              new Category { Id = 4, Name = "کتاب و لوازم تحریر", IsDeleted = false },
              new Category { Id = 5, Name = "ورزش و سفر", IsDeleted = false }
          );

            

            builder.HasMany(c=>c.Products)
                .WithOne(p=>p.Category)
                .HasForeignKey(P=>P.CategoryId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
