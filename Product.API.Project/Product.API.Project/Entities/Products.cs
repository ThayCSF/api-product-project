namespace Product.API.Project.Entities
{
    public class Products
    {
        public long Id { get; private set; }
        public string? Name { get; set; }
        public decimal UnitValue { get; set; }
        public string? Seller { get; set; }
       
    }
}
