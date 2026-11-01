# Hoàng Hà PC - ASP.NET Core MVC (.NET 8)

Chạy: `dotnet restore && dotnet run` rồi mở địa chỉ hiển thị trong terminal (vd https://localhost:5001).

| Trang | URL |
|---|---|
| Danh sách sản phẩm | / |
| Product Detail | /san-pham/{slug} |
| Chi tiết đơn hàng (giỏ hàng) | /gio-hang |
| Đặt hàng | /dat-hang |
| Xem / tra cứu đơn đã đặt | /don-hang/{mã đơn}?phone={sđt} |

Đơn hàng lưu vào SQLite (file hoangha.db tự tạo). Sản phẩm mẫu nằm ở Services/ProductCatalog.cs.
