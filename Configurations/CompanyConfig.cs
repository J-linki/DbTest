using DbTest.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DbTest.Configurations
{
    public class CompanyConfig : IEntityTypeConfiguration<CompanyModel>
    {
        public void Configure(EntityTypeBuilder<CompanyModel> builder)
        {
            builder.HasKey(c => c.Id);

            builder.HasMany(c => c.Stocks)
                   .WithOne(s => s.Company);
        }
    }
}
