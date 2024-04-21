using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace weblego.Pages
{
    public class ChiTietSanPhamModel : PageModel
    {
        public SanPham Product { get; private set; }

        public void OnGet(string id)
        {
            // Tìm kiếm sản phẩm trong danh sách dựa vào ProductId
            Product = DanhSachSanPham.danhSachSanPham.FirstOrDefault(p => p.MaSP == id);
            if (SanPhamAdd.maSP != Product.MaSP)
            {
                SanPhamAdd.maSP = Product.MaSP;
                SanPhamAdd.tenSP = Product.TenSP;
                SanPhamAdd.chuDe = Product.ChuDe;
                SanPhamAdd.doTuoi = Product.DoTuoi;
                SanPhamAdd.tonKho = Product.SoLuongTonKho;
                SanPhamAdd.donGia = Product.DonGia;
                SanPhamAdd.hinhAnh = Product.HinhAnh;
            }
            
        }

        public IActionResult OnPostAddToBag()
        {
            string maSPP = SanPhamAdd.maSP;
            // Lấy giá trị QuyenHan.maND và Product.MaSP
            int maNDD = QuyenHan.maND; // Đảm bảo rằng QuyenHan đã được khởi tạo

            // Thực hiện thêm vào bảng GioHang
            string connectionString = Constring.stringg;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Tạo câu lệnh SQL để thêm vào bảng GioHang
                string insertQuery = "INSERT INTO GioHang (MaND, MaSP) VALUES (@MaND, @MaSP)";
                SqlCommand insertCommand = new SqlCommand(insertQuery, connection);
                insertCommand.Parameters.AddWithValue("@MaND", maNDD);
                insertCommand.Parameters.AddWithValue("@MaSP", maSPP);

                // Thực thi câu lệnh SQL
                insertCommand.ExecuteNonQuery();
            }

            // Sau khi thêm vào giỏ hàng, chuyển hướng người dùng đến trang giỏ hàng hoặc trang khác
            ViewData["Message"] = "Sản phẩm đã được thêm vào giỏ hàng.";
            return RedirectToPage("/Index"); ;
        }
    }
}
