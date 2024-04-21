using DoAnWeb.ThanhToan;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace weblego.Pages
{
    public class ThanhCongModel : PageModel
    {
        public string ResponseCode { get; set; }
        public void OnGet([FromQuery(Name = "responseCode")] string responseCode)
        {
            ResponseCode = responseCode;
            if (ResponseCode == "00")
            {
                Console.WriteLine(QuyenHan.diaChi);
                DateTime ngayDatHang = DateTime.Now;
                DateTime ngayDuKienGiao = ngayDatHang.AddDays(3);
              
                string insertHoaDonQuery = "INSERT INTO HoaDon (MaND, ThanhTien, DiaChiGiaoHang, NgayDatHang, NgayDuKienGiao, PhuongThucThanhToan, TinhTrang) " +
                    "VALUES (@MaND, @ThanhTien, @DiaChiGiaoHang, @NgayDatHang, @NgayDuKienGiao, @PhuongThucThanhToan, @TinhTrang); " +
                    "SELECT SCOPE_IDENTITY() AS MaHD"; // Thêm câu lệnh SELECT để lấy mã hóa đơn mới

                int maHD; // Khai báo biến để lưu mã hóa đơn

                using (SqlConnection connection = new SqlConnection(Constring.stringg))
                {
                    connection.Open();
                    SqlCommand insertHoaDonCommand = new SqlCommand(insertHoaDonQuery, connection);
                    insertHoaDonCommand.Parameters.AddWithValue("@MaND", QuyenHan.maND);
                    insertHoaDonCommand.Parameters.AddWithValue("@ThanhTien", PaymentInformationModel.Amount);
                    insertHoaDonCommand.Parameters.AddWithValue("@DiaChiGiaoHang", QuyenHan.diaChi);
                    insertHoaDonCommand.Parameters.AddWithValue("@NgayDatHang", ngayDatHang);
                    insertHoaDonCommand.Parameters.AddWithValue("@NgayDuKienGiao", ngayDuKienGiao);
                    insertHoaDonCommand.Parameters.AddWithValue("@PhuongThucThanhToan", "onl");
                    insertHoaDonCommand.Parameters.AddWithValue("@TinhTrang", 0);

                    // Thực hiện lệnh INSERT và lấy mã hóa đơn mới
                    maHD = Convert.ToInt32(insertHoaDonCommand.ExecuteScalar());
                }

                // Thêm chi tiết hóa đơn vào bảng CTHD
                foreach (var item in DanhSachSanPham.danhSachGioHang)
                {
                    string insertCTHDQuery = "INSERT INTO CTHD (MaHD, MaSP, SoLuong) VALUES (@MaHD, @MaSP, @SoLuong)";
                    using (SqlConnection connection = new SqlConnection(Constring.stringg))
                    {
                        connection.Open();
                        SqlCommand insertCTHDCommand = new SqlCommand(insertCTHDQuery, connection);
                        insertCTHDCommand.Parameters.AddWithValue("@MaHD", maHD);
                        insertCTHDCommand.Parameters.AddWithValue("@MaSP", item.MaSP);
                        insertCTHDCommand.Parameters.AddWithValue("@SoLuong", 1);
                        insertCTHDCommand.ExecuteNonQuery();
                    }
                }
                DanhSachSanPham.danhSachGioHang.Clear();
                string deleteGioHangQuery = "DELETE FROM GioHang WHERE MaND = @MaND";
                using (SqlConnection connection = new SqlConnection(Constring.stringg))
                {
                    connection.Open();
                    SqlCommand deleteGioHangCommand = new SqlCommand(deleteGioHangQuery, connection);
                    deleteGioHangCommand.Parameters.AddWithValue("@MaND", QuyenHan.maND);
                    deleteGioHangCommand.ExecuteNonQuery();
                }
                // Sau khi thực hiện xong, bạn có thể thực hiện các hành động khác, ví dụ: chuyển hướng người dùng đến trang cảm ơn hoặc trang khác
            }



            
        }    
        }
    }