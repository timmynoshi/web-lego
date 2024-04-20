using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace weblego.Pages
{
    public class LogoutModel : PageModel
    {
        public async Task<IActionResult> OnGetAsync()
        {
            QuyenHan.tentaikhoan = "";
            // Thực hiện đăng xuất
            await HttpContext.SignOutAsync();

            // Chuyển hướng đến trang chính hoặc trang đăng nhập
            return RedirectToPage("/Index"); // Ví dụ: chuyển hướng đến trang chính
        }
    }
}
