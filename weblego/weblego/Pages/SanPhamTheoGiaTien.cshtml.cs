using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace weblego.Pages
{
    public class SanPhamTheoGiaTienModel : PageModel
    {
        public void OnGet()
        {
        }
        public IActionResult OnPost(string buttonId)
        {
            switch (buttonId)
            {
                case "btn1":
                    PhanLoai.GiaCa = 50;
                    break;
                case "btn2":
                    PhanLoai.GiaCa = 400;
                    break;
                case "btn3":
                    PhanLoai.GiaCa = 1000;
                    break;

            }

            // Sau khi xử lý xong, bạn có thể chuyển hướng hoặc thực hiện các thao tác khác
            return Page(); // Chẳng hạn, chuyển hướng về trang hiện tại
        }

    }
}
