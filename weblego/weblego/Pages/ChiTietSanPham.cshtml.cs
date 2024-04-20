using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace weblego.Pages
{
    public class ChiTietSanPhamModel : PageModel
    {

        public SanPham Product { get; private set; }

        public void OnGet(string id)
        {
            // Tìm kiếm sản phẩm trong danh sách dựa vào ProductId
            Product = DanhSachSanPham.danhSachSanPham.FirstOrDefault(p => p.MaSP == id);
        }
    }
}
