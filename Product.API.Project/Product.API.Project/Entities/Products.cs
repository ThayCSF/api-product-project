namespace Product.API.Project.Entities
{
    public class Products
    {
        private long Id { get; set; }
        public string Name { get; set; }
        public decimal UnitValue { get; set; }
        public string Seller { get; set; }
        public int Quantity { get; set; }
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
