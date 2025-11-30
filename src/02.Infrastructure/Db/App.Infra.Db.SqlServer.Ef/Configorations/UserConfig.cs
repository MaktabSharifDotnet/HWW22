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
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasData(
                  new User
                  {
                      Id = 1,
                      Username = "ali",
                      Password = "123",
                      Balance = 10000000M,
                      IsDeleted = false
                  },
                  new User
                  {
                      Id = 2,
                      Username = "ali_rezaei",
                      Password = "123",
                      Balance = 500000M,
                      IsDeleted = false
                  },
                  new User
                  {
                      Id = 3,
                      Username = "sara_kamali",
                      Password = "123",
                      Balance = 0,
                      IsDeleted = false
                  },
                  new User
                  {
                      Id = 4,
                      Username = "test_user",
                      Password = "123",
                      Balance = 2500000M,
                      IsDeleted = false
                  }
              );

            builder.HasQueryFilter(u => !u.IsDeleted);

            builder.HasMany(u => u.Carts)
                .WithOne(c => c.User)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }

}

