namespace Product.API.Project.Entities
{
    public class Products
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public double UnitValue { get; set; }
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
