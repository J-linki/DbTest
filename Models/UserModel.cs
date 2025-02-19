using System.ComponentModel.DataAnnotations;

namespace DbTest.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(63)]
        public string Name { get; set; } = null!;
        public string? Email { get; set; }
        
        [Required]
        [MaxLength(20)]
        public string Password { get; set; } = null!;
        public decimal? Balance { get; set; }

        public virtual List<StocksModel>? Stocks { get; set; } = new();
    }
}
