using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DbTest.Models;

namespace DbTest.Configurations
{
    public class StocksConfig : IEntityTypeConfiguration<StocksModel>
    {
        public void Configure(EntityTypeBuilder<StocksModel> builder)
        {
            builder.HasKey(s => s.Id);

            builder.HasOne(s => s.Company)
                   .WithMany(c => c.Stocks)
                   .HasForeignKey(s => s.CompanyId)
                   .HasPrincipalKey(c => c.Id);

            builder.HasOne(s => s.User)
                   .WithMany(u => u.Stocks)
                   .HasForeignKey(s => s.UserId)
                   .HasPrincipalKey(u => u.Id);
        }
    }
}
