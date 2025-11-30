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
    public class CartProductConfig : IEntityTypeConfiguration<CartProduct>
    {
        public void Configure(EntityTypeBuilder<CartProduct> builder)
        {

            builder.HasKey(cp => new { cp.CartId, cp.ProductId });

         
            builder.HasOne(cp => cp.Cart)
                .WithMany(c => c.CartProducts)
                .HasForeignKey(cp => cp.CartId)
                .OnDelete(DeleteBehavior.NoAction);

          
            builder.HasOne(cp => cp.Product)
                .WithMany(p => p.CartProducts)
                .HasForeignKey(cp => cp.ProductId).OnDelete(DeleteBehavior.NoAction);

           
            builder.Property(cp => cp.Count)
                .IsRequired()
                .HasDefaultValue(1); 

         
            builder.Property(cp => cp.FinalPrice)
                .HasColumnType("decimal(18,2)");

            builder.HasQueryFilter(cp => !cp.IsDeleted);

        }
    }
}