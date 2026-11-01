using HoangHaPC.Data;
using HoangHaPC.Models;
using HoangHaPC.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HoangHaPC.Controllers;

public class OrderController : Controller
{
    private readonly CartService _cart;
    private readonly ProductCatalog _catalog;
    private readonly AppDbContext _db;

    public OrderController(CartService cart, ProductCatalog catalog, AppDbContext db)
    {
        _cart = cart; _catalog = catalog; _db = db;
    }

    // Trang đặt hàng
    [HttpGet("dat-hang")]
    public IActionResult Checkout()
    {
        var items = _cart.Items();
        if (items.Count == 0) return RedirectToAction("Index", "Cart");
        return View(new CheckoutViewModel { Items = items });
    }

    [HttpPost("dat-hang")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Checkout(CheckoutViewModel model)
    {
        var items = _cart.Items();
        if (items.Count == 0) return RedirectToAction("Index", "Cart");
        model.Items = items;

        // Kiểm tra tồn kho và giá hiện tại ở server (không tin dữ liệu từ client)
        foreach (var line in items)
        {
            var p = _catalog.GetById(line.ProductId);
            if (p is null || p.Stock < line.Quantity)
                ModelState.AddModelError("", $"Sản phẩm \"{line.Name}\" không đủ hàng.");
            else
                line.Price = p.Price;
        }
        if (!ModelState.IsValid) return View(model);

        var order = new Order
        {
            Code = "HH" + DateTime.Now.ToString("yyMMddHHmmss") + Random.Shared.Next(10, 99),
            CustomerName = model.CustomerName.Trim(),
            Phone = model.Phone.Trim(),
            Email = model.Email?.Trim(),
            Address = model.Address.Trim(),
            Note = model.Note?.Trim(),
            PaymentMethod = model.PaymentMethod,
            Total = items.Sum(i => i.LineTotal),
            Items = items.Select(i => new OrderItem
            {
                ProductId = i.ProductId, ProductName = i.Name, UnitPrice = i.Price, Quantity = i.Quantity
            }).ToList()
        };
        _db.Orders.Add(order);
        await _db.SaveChangesAsync();
        _cart.Clear();

        return RedirectToAction(nameof(Detail), new { code = order.Code, phone = order.Phone, placed = true });
    }

    // Chi tiết đơn hàng: cần mã đơn + số điện thoại để xem
    [HttpGet("don-hang/{code}")]
    public async Task<IActionResult> Detail(string code, string? phone, bool placed = false)
    {
        if (string.IsNullOrWhiteSpace(phone))
            return View(model: null);

        var order = await _db.Orders.Include(o => o.Items)
            .FirstOrDefaultAsync(o => o.Code == code && o.Phone == phone.Trim());
        if (order is null)
        {
            TempData["Error"] = "Không tìm thấy đơn hàng. Kiểm tra lại mã đơn và số điện thoại.";
            return View(model: null);
        }
        ViewBag.Placed = placed;
        return View(order);
    }
}
