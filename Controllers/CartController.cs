using HoangHaPC.Services;
using Microsoft.AspNetCore.Mvc;

namespace HoangHaPC.Controllers;

public class CartController : Controller
{
    private readonly CartService _cart;
    public CartController(CartService cart) => _cart = cart;

    // Chi tiết đơn hàng (giỏ hàng) trước khi đặt
    [HttpGet("gio-hang")]
    public IActionResult Index() => View(_cart.Items());

    [HttpPost("gio-hang/them")]
    [ValidateAntiForgeryToken]
    public IActionResult Add(int productId, int quantity = 1, bool buyNow = false, string? returnUrl = null)
    {
        if (!_cart.Add(productId, quantity))
        {
            TempData["Error"] = "Sản phẩm hiện đã hết hàng hoặc không tồn tại.";
            return Redirect(returnUrl ?? "/");
        }
        if (buyNow) return RedirectToAction("Checkout", "Order");
        TempData["Success"] = "Đã thêm sản phẩm vào giỏ hàng.";
        return Redirect(Url.IsLocalUrl(returnUrl) ? returnUrl! : Url.Action(nameof(Index))!);
    }

    [HttpPost("gio-hang/cap-nhat")]
    [ValidateAntiForgeryToken]
    public IActionResult Update(int productId, int quantity)
    {
        _cart.SetQuantity(productId, quantity);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost("gio-hang/xoa")]
    [ValidateAntiForgeryToken]
    public IActionResult Remove(int productId)
    {
        _cart.Remove(productId);
        return RedirectToAction(nameof(Index));
    }
}
