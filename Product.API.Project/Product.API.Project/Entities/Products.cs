namespace Product.API.Project.Entities
{
    public class Products
    {
        public Guid Id { get; private set; }
        public string? Name { get; set; }
        public decimal UnitValue { get; set; }
        public string? Seller { get; set; }
        public DateTime CreatedAt { get; private set; }

        public Products()
        {
            Id = Guid.NewGuid();
            var now = DateTime.Now;
            CreatedAt = now;
        }
       
    }
}
