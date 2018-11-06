using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XmlProcessingExercise.Models;

namespace XmlProcessingExercise.Data.EntityConfig
{
    class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasMany(x => x.ProductsSold).WithOne(x => x.Seller).HasForeignKey(x => x.SellerId);
            builder.HasMany(x => x.ProductsBought).WithOne(x => x.Buyer).HasForeignKey(x => x.BuyerId);
        }
    }
}
