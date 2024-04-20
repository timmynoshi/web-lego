using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace weblego.Pages
{
    public class BuildModeModel : PageModel
    {
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            Console.WriteLine("Siuu");
            string maSP = Request.Form["inputmasp"];
            string tenSP = Request.Form["inputtensp"];
            string chuDe = Request.Form["inputchude"];
            int doTuoi = int.Parse(Request.Form["inputtuoi"]);
            int tonKho = int.Parse(Request.Form["inputtonkho"]);
            int giaTien = int.Parse(Request.Form["inputgiatien"]);
            string hinhAnh = Request.Form["inputimg"];

            string connectionString = Constring.stringg;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Kiểm tra sản phẩm có tồn tại trong cơ sở dữ liệu hay không
                string checkQuery = "SELECT COUNT(*) FROM SanPham WHERE MaSP = @MaSP";
                SqlCommand checkCommand = new SqlCommand(checkQuery, connection);
                checkCommand.Parameters.AddWithValue("@MaSP", maSP);
                int existingCount = (int)checkCommand.ExecuteScalar();

                if (existingCount > 0)
                {
                    // Nếu sản phẩm đã tồn tại, cập nhật thông tin
                    string updateQuery = "UPDATE SanPham SET TenSP = @TenSP, ChuDe = @ChuDe, DoTuoi = @DoTuoi, SoLuongTonKho = @TonKho, DonGia = @GiaTien, HinhAnh = @HinhAnh WHERE MaSP = @MaSP";
                    SqlCommand updateCommand = new SqlCommand(updateQuery, connection);
                    updateCommand.Parameters.AddWithValue("@TenSP", tenSP);
                    updateCommand.Parameters.AddWithValue("@ChuDe", chuDe);
                    updateCommand.Parameters.AddWithValue("@DoTuoi", doTuoi);
                    updateCommand.Parameters.AddWithValue("@TonKho", tonKho);
                    updateCommand.Parameters.AddWithValue("@GiaTien", giaTien);
                    updateCommand.Parameters.AddWithValue("@HinhAnh", hinhAnh);
                    updateCommand.Parameters.AddWithValue("@MaSP", maSP);

                    updateCommand.ExecuteNonQuery();
                }
                else
                {
                    // Nếu sản phẩm chưa tồn tại, tạo mới
                    string insertQuery = "INSERT INTO SanPham (MaSP, TenSP, ChuDe, DoTuoi, SoLuongTonKho, DonGia, HinhAnh) VALUES (@MaSP, @TenSP, @ChuDe, @DoTuoi, @TonKho, @GiaTien, @HinhAnh)";
                    SqlCommand insertCommand = new SqlCommand(insertQuery, connection);
                    insertCommand.Parameters.AddWithValue("@MaSP", maSP);
                    insertCommand.Parameters.AddWithValue("@TenSP", tenSP);
                    insertCommand.Parameters.AddWithValue("@ChuDe", chuDe);
                    insertCommand.Parameters.AddWithValue("@DoTuoi", doTuoi);
                    insertCommand.Parameters.AddWithValue("@TonKho", tonKho);
                    insertCommand.Parameters.AddWithValue("@GiaTien", giaTien);
                    insertCommand.Parameters.AddWithValue("@HinhAnh", hinhAnh);

                    insertCommand.ExecuteNonQuery();
                }

                return RedirectToPage("/BuildMode"); // Chuyển hướng sau khi xử lý thành công
            }
        }

        public IActionResult OnPostDelete()
        {
            return RedirectToPage("/Index");
            string maSP = Request.Form["inputmasp"];
            string connectionString = Constring.stringg;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Xóa sản phẩm dựa trên MaSP
                string deleteQuery = "DELETE FROM SanPham WHERE MaSP = @MaSP";
                SqlCommand deleteCommand = new SqlCommand(deleteQuery, connection);
                deleteCommand.Parameters.AddWithValue("@MaSP", maSP);
                deleteCommand.ExecuteNonQuery();

                return RedirectToPage("/Index"); // Chuyển hướng sau khi xử lý thành công
            }
        }

    }
}
