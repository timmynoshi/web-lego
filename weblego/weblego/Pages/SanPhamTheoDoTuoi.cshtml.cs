using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace weblego.Pages
{
    public class SanPhamTheoDoTuoiModel : PageModel
    {
        public void OnGet()
        {
        }

        public IActionResult OnPost(string buttonId)
        {
            switch (buttonId)
            {
                case "btn1":
                    PhanLoai.DoTuoi = 1;
                    break;
                case "btn2":
                    PhanLoai.DoTuoi = 8;
                    break;
                case "btn3":
                    PhanLoai.DoTuoi = 12;
                    break;

            }

            // Sau khi x? l� xong, b?n c� th? chuy?n h??ng ho?c th?c hi?n c�c thao t�c kh�c
            return Page(); // Ch?ng h?n, chuy?n h??ng v? trang hi?n t?i
        }
    }
}
