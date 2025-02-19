using System.ComponentModel.DataAnnotations;

namespace DbTest.Models
{
    public class CompanyModel
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(63, ErrorMessage = "Name is too long.")]
        [MinLength(2)]
        public string Name { get; set; } = null!;
        public decimal? MarketValue { get; set; }
        public virtual List<StocksModel> Stocks { get; set; } = new();
    }
}
