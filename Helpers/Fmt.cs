using System.Globalization;

namespace HoangHaPC.Helpers;

public static class Fmt
{
    private static readonly CultureInfo Vi = new("vi-VN");
    public static string Vnd(decimal value) => value.ToString("N0", Vi) + "đ";
}
