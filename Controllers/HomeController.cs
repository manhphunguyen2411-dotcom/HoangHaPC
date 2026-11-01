using HoangHaPC.Services;
using Microsoft.AspNetCore.Mvc;

namespace HoangHaPC.Controllers;

public class HomeController : Controller
{
    private readonly ProductCatalog _catalog;
    public HomeController(ProductCatalog catalog) => _catalog = catalog;

    public IActionResult Index(string? category)
    {
        var products = string.IsNullOrWhiteSpace(category)
            ? _catalog.All
            : _catalog.All.Where(p => p.Category == category).ToList();
        ViewBag.Categories = _catalog.All.Select(p => p.Category).Distinct().ToList();
        ViewBag.Current = category;
        return View(products);
    }

    public IActionResult Error() => Content("Đã có lỗi xảy ra. Vui lòng thử lại.");
}
