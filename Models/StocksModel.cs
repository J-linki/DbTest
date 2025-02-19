namespace DbTest.Models
{
    public class StocksModel
    {
        public decimal GrowthRate { get; set; }
        public decimal Value { get; set; }
        public int CompanyId { get; set; }
        public int? UserId { get; set; }
        public virtual CompanyModel Company { get; set; } = null!;
        public virtual UserModel? User { get; set; } = null!;
    }
}
