using DoAnWeb.ThanhToan;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Data.SqlClient;
using System.Globalization;

namespace weblego.Pages
{
    public class GioHangModel : PageModel
    {
      

        public int TongGiaTri;
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
            TongGiaTri = TinhTong();
            int tonggiatrivnd = TongGiaTri * 23000;
            PaymentInformationModel.Amount = tonggiatrivnd;
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

        }

        public void OnPost(string PhuongThucThanhToan)
        {
            
            
            Console.WriteLine(QuyenHan.diaChi);
            DateTime ngayDatHang = DateTime.Now;
            DateTime ngayDuKienGiao = ngayDatHang.AddDays(3);
            string phuongThucThanhToan = PhuongThucThanhToan;
            Console.WriteLine("MaND" + QuyenHan.maND + "Tien" + TongGiaTri.ToString());
            Console.WriteLine(phuongThucThanhToan);
            Console.WriteLine(QuyenHan.diaChi);
            Console.WriteLine(ngayDatHang + "" + ngayDuKienGiao);
            string insertHoaDonQuery = "INSERT INTO HoaDon (MaND, ThanhTien, DiaChiGiaoHang, NgayDatHang, NgayDuKienGiao, PhuongThucThanhToan, TinhTrang) " +
                "VALUES (@MaND, @ThanhTien, @DiaChiGiaoHang, @NgayDatHang, @NgayDuKienGiao, @PhuongThucThanhToan, @TinhTrang); " +
                "SELECT SCOPE_IDENTITY() AS MaHD"; // Thêm câu lệnh SELECT để lấy mã hóa đơn mới

            int maHD; // Khai báo biến để lưu mã hóa đơn

            using (SqlConnection connection = new SqlConnection(Constring.stringg))
            {
                connection.Open();
                SqlCommand insertHoaDonCommand = new SqlCommand(insertHoaDonQuery, connection);
                insertHoaDonCommand.Parameters.AddWithValue("@MaND", QuyenHan.maND);
                insertHoaDonCommand.Parameters.AddWithValue("@ThanhTien", TongGiaTri);
                insertHoaDonCommand.Parameters.AddWithValue("@DiaChiGiaoHang", QuyenHan.diaChi);
                insertHoaDonCommand.Parameters.AddWithValue("@NgayDatHang", ngayDatHang);
                insertHoaDonCommand.Parameters.AddWithValue("@NgayDuKienGiao", ngayDuKienGiao);
                insertHoaDonCommand.Parameters.AddWithValue("@PhuongThucThanhToan", phuongThucThanhToan);
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

    

        public static int TinhTong()
        {
            int temp = 0;
            foreach(var sanPham in DanhSachSanPham.danhSachGioHang)
            {
                foreach(var i in DanhSachSanPham.danhSachSanPham)
                {
                    if(sanPham.MaSP==i.MaSP)
                        temp += i.DonGia;
                }    
            }
            return temp; 
        }
    }
}
