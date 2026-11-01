using HoangHaPC.Services;
using Microsoft.AspNetCore.Mvc;

namespace HoangHaPC.Controllers;

public class ProductController : Controller
{
    private readonly ProductCatalog _catalog;
    public ProductController(ProductCatalog catalog) => _catalog = catalog;

    // /san-pham/{slug}
    [HttpGet("san-pham/{slug}")]
    public IActionResult Detail(string slug)
    {
        var product = _catalog.GetBySlug(slug);
        if (product is null) return NotFound();
        ViewBag.Related = _catalog.Related(product).ToList();
        return View(product);
    }
}
