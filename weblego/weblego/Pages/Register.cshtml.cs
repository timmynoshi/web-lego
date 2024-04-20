using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Data.SqlClient;

namespace weblego.Pages
{
    public class RegisterModel : PageModel
    {
        [BindProperty]
        public string Name { get; set; }
        [BindProperty]
        public string Username { get; set; }
        [BindProperty]
        public string Password { get; set; }
        [BindProperty]
        public string Address { get; set; }
        [BindProperty]
        public string PhoneNumber { get; set; }

        public IActionResult OnPost()
        {
            string connectionString = Constring.stringg;
            string query = "INSERT INTO NguoiDung (TenND, DiaChi, SDT, TaiKhoan, MatKhau, QuyenHan) VALUES (@TenND, @DiaChi, @SDT, @TaiKhoan, @MatKhau, @QuyenHan)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@TenND", Name);
                command.Parameters.AddWithValue("@DiaChi", Address);
                command.Parameters.AddWithValue("@SDT", PhoneNumber);
                command.Parameters.AddWithValue("@TaiKhoan", Username);
                command.Parameters.AddWithValue("@MatKhau", Password);
                command.Parameters.AddWithValue("@QuyenHan", "khach");

                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Insert thành công!");
                        return RedirectToPage("/Login"); // Điều hướng đến trang đăng nhập sau khi đăng ký thành công
                    }
                    else
                    {
                        Console.WriteLine("Insert không thành công!");
                        return Page(); // Trở lại trang đăng ký để người dùng có thể thử lại
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Lỗi: " + ex.Message);
                    return Page(); // Trở lại trang đăng ký nếu có lỗi xảy ra
                }
            }
        }
    }
}
