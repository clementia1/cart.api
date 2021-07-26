namespace CartApi.Entities
{
    public class ProductEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int Amount { get; set; }
        public decimal Price { get; set; }
        public double Weight { get; set; }
        public string PreviewImageUrl { get; set; } = null!;
    }
}