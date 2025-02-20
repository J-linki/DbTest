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

            builder.Property(s => s.Id).HasColumnName("StocksId");

            builder.HasOne(s => s.Company)
                   .WithMany(c => c.Stocks)
                   .HasForeignKey(s => s.CompanyId)
                   .HasPrincipalKey(c => c.Id)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(s => s.User)
                   .WithMany(u => u.Stocks)
                   .HasForeignKey(s => s.UserId)
                   .HasPrincipalKey(u => u.Id)
                   .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
