using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace weblego.Pages
{
    public class ChiTietSanPhamModel : PageModel
    {
        public string maSP;
        public string tenSP;
        public string chuDe;
        public int doTuoi;
        public int tonKho;
        public int donGia;
        public string hinhAnh;

     


        public SanPham Product { get; private set; }

        public void OnGet(string id)
        {
            // Tìm kiếm sản phẩm trong danh sách dựa vào ProductId
            Product = DanhSachSanPham.danhSachSanPham.FirstOrDefault(p => p.MaSP == id);
            maSP = Product.MaSP; tenSP = Product.TenSP; hinhAnh = Product.HinhAnh;
            chuDe = Product.ChuDe; doTuoi = Product.DoTuoi; tonKho = Product.SoLuongTonKho;
            donGia = Product.DonGia;
        }

        public IActionResult OnPostAddToBag()
        {
            Console.WriteLine(maSP);

            // Lấy giá trị QuyenHan.maND và Product.MaSP
            int maND = QuyenHan.maND; // Đảm bảo rằng QuyenHan đã được khởi tạo
            Console.WriteLine(maND);

            // Thực hiện thêm vào bảng GioHang
            string connectionString = Constring.stringg;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Tạo câu lệnh SQL để thêm vào bảng GioHang
                string insertQuery = "INSERT INTO GioHang (MaND, MaSP) VALUES (@MaND, @MaSP)";
                SqlCommand insertCommand = new SqlCommand(insertQuery, connection);
                insertCommand.Parameters.AddWithValue("@MaND", maND);
                insertCommand.Parameters.AddWithValue("@MaSP", maSP);

                // Thực thi câu lệnh SQL
                insertCommand.ExecuteNonQuery();
            }

            // Sau khi thêm vào giỏ hàng, chuyển hướng người dùng đến trang giỏ hàng hoặc trang khác
            return RedirectToPage("/Index");
        }
    }
}
