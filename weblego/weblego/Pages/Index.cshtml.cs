using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace weblego.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            DanhSachSanPham.danhSachSanPham.Clear();
            QuyenHan.tentaikhoan = User.Identity.Name;
            if (User.Identity.Name == "admin")
            {
                QuyenHan.IsQuanTri = true;
            }
            else
            {
                QuyenHan.IsQuanTri = false;
            }
            Console.WriteLine(QuyenHan.tentaikhoan);

            string query = "SELECT MaSP, TenSP, ChuDe, DoTuoi, SoLuongTonKho, DonGia, HinhAnh FROM SanPham";
            using (SqlConnection connection = new SqlConnection(Constring.stringg))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    SanPham sanPham = new SanPham(
                        reader.GetString(0), // MaSP
                        reader.GetString(1), // TenSP
                        reader.GetString(2), // ChuDe
                        reader.GetInt32(3),  // DoTuoi
                        reader.GetInt32(4),  // SoLuongTonKho
                        reader.GetInt32(5),  // DonGia
                        reader.GetString(6)   // HinhAnh
                    );
                    DanhSachSanPham.danhSachSanPham.Add(sanPham);
                }
                reader.Close();
            }



        }

        public void OnPost()
        {
            HttpContext.SignOutAsync();
            QuyenHan.IsQuanTri = false;
        }
    }
}
