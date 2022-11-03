using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastructure.Persistence.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
       builder.Property(t => t.Name).HasMaxLength(50).IsRequired();
       builder.Property(t=>t.Category).IsRequired();
       

        builder.OwnsOne(product => product.Price,
                        navigationBuilder =>
                        {
                            navigationBuilder.Property(price => price.Amount).HasColumnName("Amount");
                            navigationBuilder.Property(price => price.Currency).HasColumnName("Currency");
                        });
    }
}

