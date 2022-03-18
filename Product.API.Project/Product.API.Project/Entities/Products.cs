
using System.ComponentModel.DataAnnotations;

namespace Product.API.Project.Entities
{
    public class Products
    {
        public Guid Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public decimal UnitValue { get; set; }
        [Required]
        public string? Seller { get; set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsAvailable { get; set; }

        public Products()
        {
            Id = Guid.NewGuid();
            var now = DateTime.Now;
            CreatedAt = now;
            UpdatedAt = now;
            IsAvailable = true;
        }
    }
}
