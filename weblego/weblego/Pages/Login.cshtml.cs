using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Security.Claims;

namespace weblego.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public string userName { get; set; }
        [BindProperty]
        public string passWord { get; set; }
        public void OnGet()
        {

             // Trả về trang đăng nhập khi GET request được gửi đến
        }
        public IActionResult OnPost()
        {
            // Kết nối đến cơ sở dữ liệu
            using (SqlConnection connection = new SqlConnection(Constring.stringg))
            {
                // Mở kết nối
                connection.Open();

                // Tạo truy vấn SQL để kiểm tra thông tin đăng nhập
                string query = "SELECT MaND, DiaChi, COUNT(*) FROM NguoiDung WHERE TaiKhoan = @UserName AND MatKhau = @Password GROUP BY MaND, DiaChi";

                // Tạo đối tượng Command
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Thêm tham số cho truy vấn SQL để tránh tấn công SQL Injection
                    command.Parameters.AddWithValue("@UserName", userName);
                    command.Parameters.AddWithValue("@Password", passWord);

                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read()) // Đọc kết quả
                    {
                        // Lấy giá trị MaND từ cột thứ nhất (index 0)
                        QuyenHan.maND = reader.GetInt32(0);
                        QuyenHan.diaChi = reader.GetString(1);
                    }
                    reader.Close();
                    // Thực thi truy vấn và lấy kết quả
                    int count = (int)command.ExecuteScalar();

                    // Kiểm tra kết quả
                    if (count > 0)
                    {
                        if (userName == "admin")
                        {
                            QuyenHan.IsQuanTri=true;
                        }
                        else
                        {
                            QuyenHan.IsQuanTri = false;

                        }
                        QuyenHan.tentaikhoan = userName;
                        // Đăng nhập thành công, thực hiện các hành động liên quan đến đăng nhập và chuyển hướng đến trang chính
                        List<Claim> lst = new List<Claim>()
                        {
                            new Claim(ClaimTypes.NameIdentifier, userName),
                            new Claim(ClaimTypes.Name, userName)
                        };
                        ClaimsIdentity ci = new ClaimsIdentity(lst, CookieAuthenticationDefaults.AuthenticationScheme);
                        ClaimsPrincipal cp = new ClaimsPrincipal(ci);
                        HttpContext.SignInAsync(cp);
                        Console.WriteLine(QuyenHan.maND);

                        return RedirectToPage("/Index");
                    }
                    else
                    {
                        // Thông báo lỗi nếu thông tin đăng nhập không chính xác
                        ModelState.AddModelError(string.Empty, "Tên tài khoản hoặc mật khẩu không chính xác.");
                        return Page();
                    }
                }
            }
        }

    }
}
