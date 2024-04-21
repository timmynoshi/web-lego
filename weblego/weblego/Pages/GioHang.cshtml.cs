using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace weblego.Pages
{
    public class GioHangModel : PageModel
    {
        public void OnGet()
        {
            DanhSachSanPham.danhSachGioHang.Clear();
            string query = "SELECT MaND, MaSP FROM GioHang WHERE MaND = @MaND";
            using (SqlConnection connection = new SqlConnection(Constring.stringg))
            {

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@MaND", QuyenHan.maND);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    SanPhamRev sanPhamrev = new SanPhamRev(
                        reader.GetInt32(0), // MaND
                        reader.GetString(1) // MaSP
                        
                    );
                    DanhSachSanPham.danhSachGioHang.Add(sanPhamrev);
                }
                reader.Close();
            }
        }

        public IActionResult OnPostRev(string productId)
        {
            Console.WriteLine(productId);
            string maSP = productId;
            using (SqlConnection connection = new SqlConnection(Constring.stringg))
            {
                connection.Open();

                // Xóa sản phẩm dựa trên MaSP
                string deleteQuery = "DELETE FROM GioHang WHERE MaSP = @MaSP";
                SqlCommand deleteCommand = new SqlCommand(deleteQuery, connection);
                deleteCommand.Parameters.AddWithValue("@MaSP", maSP);
                deleteCommand.ExecuteNonQuery();

                return RedirectToPage("/GioHang"); // Chuyển hướng sau khi xử lý thành công
            }

            return Page();
        }
    }
}
