using System.Text.Json;
using HoangHaPC.Models;

namespace HoangHaPC.Services;

/// <summary>Giỏ hàng lưu trong Session.</summary>
public class CartService
{
    private const string Key = "CART";
    private readonly IHttpContextAccessor _http;
    private readonly ProductCatalog _catalog;

    public CartService(IHttpContextAccessor http, ProductCatalog catalog) { _http = http; _catalog = catalog; }

    private ISession Session => _http.HttpContext!.Session;

    public List<CartItem> Items()
    {
        var json = Session.GetString(Key);
        return string.IsNullOrEmpty(json) ? new() : JsonSerializer.Deserialize<List<CartItem>>(json) ?? new();
    }

    public int Count => Items().Sum(i => i.Quantity);
    public decimal Total => Items().Sum(i => i.LineTotal);

    private void Save(List<CartItem> items) => Session.SetString(Key, JsonSerializer.Serialize(items));

    public bool Add(int productId, int quantity)
    {
        var p = _catalog.GetById(productId);
        if (p is null || p.Stock <= 0) return false;
        var items = Items();
        var line = items.FirstOrDefault(i => i.ProductId == productId);
        if (line is null)
            items.Add(new CartItem { ProductId = p.Id, Name = p.Name, Slug = p.Slug, Price = p.Price, Quantity = Math.Clamp(quantity, 1, p.Stock) });
        else
            line.Quantity = Math.Clamp(line.Quantity + quantity, 1, p.Stock);
        Save(items);
        return true;
    }

    public void SetQuantity(int productId, int quantity)
    {
        var items = Items();
        var line = items.FirstOrDefault(i => i.ProductId == productId);
        var p = _catalog.GetById(productId);
        if (line is null || p is null) return;
        if (quantity <= 0) items.Remove(line);
        else line.Quantity = Math.Clamp(quantity, 1, Math.Max(p.Stock, 1));
        Save(items);
    }

    public void Remove(int productId) { var items = Items(); items.RemoveAll(i => i.ProductId == productId); Save(items); }
    public void Clear() => Session.Remove(Key);
}
