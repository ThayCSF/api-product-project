namespace Product.API.Project.Entities
{
    public class Products
    {
        public long Id { get; private set; }
        public string Name { get; set; }
        public decimal UnitValue { get; set; }
        public string Seller { get; set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsActive { get; set; }

        public Products()
        {
            var now = DateTime.Now;
            CreatedAt = now;
            UpdatedAt = now;
            IsActive = true;
        }
    }
}
