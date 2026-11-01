using HoangHaPC.Models;

namespace HoangHaPC.Services;

/// <summary>Danh mục sản phẩm mẫu (in-memory). Thay bằng bảng Products trong DB khi cần.</summary>
public class ProductCatalog
{
    private readonly List<Product> _products = new()
    {
        new Product {
            Id = 1, Slug = "hh-workstation-core-i7-10700-16g-gtx-1660-6gb",
            Name = "HH Workstation Core i7 10700 / 16GB / GTX 1660 6GB", Category = "PC Workstation", Brand = "Hoàng Hà PC",
            Price = 17_900_000, OldPrice = 19_500_000, Stock = 12, Warranty = "36 tháng",
            ShortDescription = "Cấu hình dựng phim, render và livestream ổn định, đủ sức chạy Premiere, After Effects, Photoshop.",
            Highlights = { "Bảo hành 36 tháng tại showroom", "Test tải 24h trước khi giao", "Miễn phí vệ sinh và nâng cấp trọn đời" },
            Specs = new() {
                ["CPU"] = "Intel Core i7 10700 (8 nhân / 16 luồng)", ["RAM"] = "Corsair LPX 16GB DDR4 3000MHz",
                ["VGA"] = "PNY GTX 1660 6GB GDDR5", ["SSD"] = "PNY CS3030 250GB NVMe", ["HDD"] = "Toshiba P300 1TB 7200rpm",
                ["Nguồn"] = "Cooler Master 700W", ["Case"] = "Sama 3301 kính cường lực + 3 Fan RGB" } },
        new Product {
            Id = 2, Slug = "hh-workstation-core-i5-10400-16g-gtx-1050ti-4gb",
            Name = "HH Workstation Core i5 10400 / 16GB / GTX 1050Ti 4GB", Category = "PC Workstation", Brand = "Hoàng Hà PC",
            Price = 14_500_000, Stock = 20, Warranty = "36 tháng",
            ShortDescription = "Cấu hình tầm 15 triệu cho thiết kế 2D, dựng phim cơ bản và làm việc văn phòng nặng.",
            Highlights = { "Bảo hành 36 tháng", "Giao hàng toàn quốc", "Hỗ trợ trả góp" },
            Specs = new() {
                ["CPU"] = "Intel Core i5 10400 (6 nhân / 12 luồng)", ["RAM"] = "16GB DDR4 3000MHz",
                ["VGA"] = "GTX 1050Ti 4GB GDDR5", ["SSD"] = "NVMe 250GB", ["Nguồn"] = "Cooler Master PC700 700W V3" } },
        new Product {
            Id = 3, Slug = "cpu-intel-core-i7-10700",
            Name = "CPU Intel Core i7 10700 (2.9GHz turbo 4.8GHz, 8 nhân 16 luồng)", Category = "CPU", Brand = "Intel",
            Price = 6_890_000, OldPrice = 7_400_000, Stock = 35, Warranty = "36 tháng",
            ShortDescription = "Bộ xử lý Intel thế hệ 10 socket LGA1200, hiệu năng mạnh cho đa nhiệm và đồ họa.",
            Highlights = { "Chính hãng, fullbox", "Bảo hành 36 tháng 1 đổi 1" },
            Specs = new() { ["Socket"] = "LGA1200", ["Số nhân / luồng"] = "8 / 16", ["Xung nhịp"] = "2.9GHz - 4.8GHz", ["Cache"] = "16MB", ["TDP"] = "65W" } },
        new Product {
            Id = 4, Slug = "cpu-amd-ryzen-5-3600",
            Name = "CPU AMD Ryzen 5 3600 (3.6GHz turbo 4.2GHz, 6 nhân 12 luồng)", Category = "CPU", Brand = "AMD",
            Price = 3_150_000, Stock = 0, Warranty = "36 tháng",
            ShortDescription = "Vi xử lý AM4 hiệu năng/giá tốt nhất phân khúc, kèm sẵn tản nhiệt Wraith Stealth.",
            Highlights = { "Chính hãng, fullbox", "Kèm tản nhiệt zin" },
            Specs = new() { ["Socket"] = "AM4", ["Số nhân / luồng"] = "6 / 12", ["Xung nhịp"] = "3.6GHz - 4.2GHz", ["Cache"] = "32MB", ["TDP"] = "65W" } },
        new Product {
            Id = 5, Slug = "ram-corsair-vengeance-lpx-16gb-ddr4-3000",
            Name = "RAM Corsair Vengeance LPX 16GB DDR4 3000MHz", Category = "RAM", Brand = "Corsair",
            Price = 1_090_000, OldPrice = 1_250_000, Stock = 80, Warranty = "36 tháng",
            ShortDescription = "RAM DDR4 bus 3000MHz tản nhiệt nhôm thấp, tương thích hầu hết mainboard.",
            Highlights = { "Bảo hành 36 tháng", "Lắp đặt miễn phí tại showroom" },
            Specs = new() { ["Dung lượng"] = "16GB (1x16GB)", ["Loại"] = "DDR4", ["Bus"] = "3000MHz", ["Điện áp"] = "1.35V" } },
        new Product {
            Id = 6, Slug = "ssd-pny-cs3030-250gb-nvme",
            Name = "SSD PNY CS3030 250GB NVMe M.2 PCIe Gen3 x4", Category = "Ổ cứng", Brand = "PNY",
            Price = 720_000, Stock = 60, Warranty = "60 tháng",
            ShortDescription = "Ổ SSD NVMe tốc độ đọc 3500MB/s, phù hợp ổ hệ điều hành và phần mềm đồ họa.",
            Highlights = { "Bảo hành 60 tháng", "Tốc độ đọc 3500MB/s" },
            Specs = new() { ["Dung lượng"] = "250GB", ["Chuẩn"] = "M.2 NVMe PCIe Gen3 x4", ["Đọc / Ghi"] = "3500 / 1050 MB/s" } },
    };

    public IReadOnlyList<Product> All => _products;
    public Product? GetById(int id) => _products.FirstOrDefault(p => p.Id == id);
    public Product? GetBySlug(string slug) => _products.FirstOrDefault(p => p.Slug == slug);
    public IEnumerable<Product> Related(Product p, int take = 4) =>
        _products.Where(x => x.Id != p.Id && x.Category == p.Category).Concat(_products.Where(x => x.Id != p.Id && x.Category != p.Category)).Take(take);
}
