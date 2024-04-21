using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace weblego.Pages
{
    public class OrderModel : PageModel
    {
        public void OnGet()
        {
            DanSachHoaDon.danhsachHDkhach.Clear();
            string query = "SELECT MaHD, NgayDatHang, PhuongThucThanhToan, TinhTrang FROM HoaDon WHERE MaND = @MaND";
            using (SqlConnection connection = new SqlConnection(Constring.stringg))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@MaND", QuyenHan.maND);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int maHD = reader.GetInt32(0); // MaHD
                    DateTime ngayDatHang = reader.GetDateTime(1); // NgayDatHang
                    string phuongThucThanhToan = reader.GetString(2); // PhuongThucThanhToan
                    bool tinhTrang = reader.GetBoolean(3); // TinhTrang

                    // Tạo đối tượng HDcanduyet và thêm vào danh sách
                    HDcanduyet hoadon = new HDcanduyet(maHD, ngayDatHang, phuongThucThanhToan, tinhTrang);
                    DanSachHoaDon.danhsachHDkhach.Add(hoadon);
                }
                reader.Close();
            }
        }

    }
}
