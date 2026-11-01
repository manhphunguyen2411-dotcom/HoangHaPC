using System.ComponentModel.DataAnnotations;

namespace HoangHaPC.Models;

public class CheckoutViewModel
{
    [Required(ErrorMessage = "Vui lòng nhập họ tên")]
    [StringLength(100)]
    [Display(Name = "Họ và tên")]
    public string CustomerName { get; set; } = "";

    [Required(ErrorMessage = "Vui lòng nhập số điện thoại")]
    [RegularExpression(@"^(0|\+84)\d{9,10}$", ErrorMessage = "Số điện thoại không hợp lệ")]
    [Display(Name = "Số điện thoại")]
    public string Phone { get; set; } = "";

    [EmailAddress(ErrorMessage = "Email không hợp lệ")]
    [Display(Name = "Email")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập địa chỉ nhận hàng")]
    [StringLength(250)]
    [Display(Name = "Địa chỉ nhận hàng")]
    public string Address { get; set; } = "";

    [StringLength(500)]
    [Display(Name = "Ghi chú")]
    public string? Note { get; set; }

    [Required]
    [Display(Name = "Hình thức thanh toán")]
    public string PaymentMethod { get; set; } = "COD";

    public List<CartItem> Items { get; set; } = new();
    public decimal Total => Items.Sum(i => i.LineTotal);
}
