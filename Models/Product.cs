namespace HoangHaPC.Models;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Slug { get; set; } = "";
    public string Category { get; set; } = "";
    public string Brand { get; set; } = "";
    public decimal Price { get; set; }
    public decimal? OldPrice { get; set; }
    public int Stock { get; set; }
    public string Warranty { get; set; } = "36 tháng";
    public string ShortDescription { get; set; } = "";
    public List<string> Highlights { get; set; } = new();
    public Dictionary<string, string> Specs { get; set; } = new();

    public int DiscountPercent =>
        OldPrice is > 0 && OldPrice > Price ? (int)Math.Round((double)((OldPrice.Value - Price) / OldPrice.Value) * 100) : 0;
}
