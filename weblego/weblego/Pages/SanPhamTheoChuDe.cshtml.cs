using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
namespace weblego.Pages
{
    public class SanPhamTheoChuDeModel : PageModel
    {
        public void OnGet()
        {


        }
        public IActionResult OnPost(string buttonId)
        {
            switch (buttonId)
            {
                case "btn1":
                    PhanLoai.ChuDe = "Star Wars";
                    break;
                case "btn2":
                    PhanLoai.ChuDe = "Ninjago";
                    break;
                case "btn3":
                    PhanLoai.ChuDe = "Technic";
                    break;
                case "btn4":
                    PhanLoai.ChuDe = "Classic";
                    break;

            }

            // Sau khi xử lý xong, bạn có thể chuyển hướng hoặc thực hiện các thao tác khác
            return Page(); // Chẳng hạn, chuyển hướng về trang hiện tại
        }
    }
}
