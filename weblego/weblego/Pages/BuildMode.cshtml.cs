﻿using Microsoft.AspNetCore.Mvc;
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
            DanSachHoaDon.danhsachHD.Clear();
            string query = "SELECT MaHD, NgayDatHang, PhuongThucThanhToan, TinhTrang FROM HoaDon WHERE TinhTrang = 0";
            using (SqlConnection connection = new SqlConnection(Constring.stringg))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    HDcanduyet hoadon = new HDcanduyet(
                        reader.GetInt32(0), // MaSP
                        reader.GetDateTime(1), // NGayDatHAng
                        reader.GetString(2), // ChuDe
                        reader.GetBoolean(3)  // DoTuoi
                    );
                    DanSachHoaDon.danhsachHD.Add(hoadon);
                }
                reader.Close();
            }
        }

        public IActionResult OnPostUpdate()
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

                return RedirectToPage("/BuildMode"); // Chuyển hướng sau khi xử lý thành công
            }
        }

        public IActionResult OnPostUpdateND()
        {
            string maND = Request.Form["inputmand"];
            string tenND = Request.Form["inputtennd"];
            string diaChi = Request.Form["inputdiachi"];
            string sdt = Request.Form["inputsdt"];
            string taiKhoan = Request.Form["inputtaikhoan"];
            string matKhau = Request.Form["inputmatkhau"];
            string quyenHan = "khach";

            Console.WriteLine("flag 1");

            Console.WriteLine(maND);
            string connectionString = Constring.stringg;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Kiểm tra sản phẩm có tồn tại trong cơ sở dữ liệu hay không
                string checkQuery = "SELECT COUNT(*) FROM NguoiDung WHERE MaND = @MaND";
                SqlCommand checkCommand = new SqlCommand(checkQuery, connection);
                checkCommand.Parameters.AddWithValue("@MaND", maND);
                int existingCount = (int)checkCommand.ExecuteScalar();
                Console.WriteLine("flag 2");

                if (existingCount > 0)
                {
                    // Nếu sản phẩm đã tồn tại, cập nhật thông tin
                    string updateQuery = "UPDATE NguoiDung SET TenND = @TenND, DiaChi = @DiaChi, SDT = @SDT, TaiKhoan = @TaiKhoan, MatKhau = @MatKhau WHERE MaND = @MaND";
                    SqlCommand updateCommand = new SqlCommand(updateQuery, connection);
                    updateCommand.Parameters.AddWithValue("@MaND", maND);
                    updateCommand.Parameters.AddWithValue("@TenND", tenND);
                    updateCommand.Parameters.AddWithValue("@DiaChi", diaChi);
                    updateCommand.Parameters.AddWithValue("@SDT", sdt);
                    updateCommand.Parameters.AddWithValue("@TaiKhoan", taiKhoan);
                    updateCommand.Parameters.AddWithValue("@MatKhau", matKhau);
                    Console.WriteLine("flag 3");

                    updateCommand.ExecuteNonQuery();
                    Console.WriteLine("flag 4");

                }
                else
                {
                    // Nếu sản phẩm chưa tồn tại, tạo mới
                    string insertQuery = "INSERT INTO NguoiDung (TenND, DiaChi, SDT, TaiKhoan, MatKhau, QuyenHan) VALUES (@TenND, @DiaChi, @SDT, @TaiKhoan, @MatKhau, @QuyenHan)";
                    SqlCommand insertCommand = new SqlCommand(insertQuery, connection);
                    insertCommand.Parameters.AddWithValue("@TenND", tenND);
                    insertCommand.Parameters.AddWithValue("@DiaChi", diaChi);
                    insertCommand.Parameters.AddWithValue("@SDT", sdt);
                    insertCommand.Parameters.AddWithValue("@TaiKhoan", taiKhoan);
                    insertCommand.Parameters.AddWithValue("@MatKhau", matKhau);
                    insertCommand.Parameters.AddWithValue("@QuyenHan", quyenHan);

                    insertCommand.ExecuteNonQuery();
                }

                return RedirectToPage("/BuildMode"); // Chuyển hướng sau khi xử lý thành công
            }
        }

        public IActionResult OnPostDeleteND()
        {
            string maND = Request.Form["inputmand"];
            string connectionString = Constring.stringg;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Xóa sản phẩm dựa trên MaSP
                string deleteQuery = "DELETE FROM NguoiDung WHERE MaND = @MaND";
                SqlCommand deleteCommand = new SqlCommand(deleteQuery, connection);
                deleteCommand.Parameters.AddWithValue("@MaND", maND);
                deleteCommand.ExecuteNonQuery();

                return RedirectToPage("/BuildMode"); // Chuyển hướng sau khi xử lý thành công
            }
        }

        public IActionResult OnPostDuyet(int btnduyet)
        {
            int MaHD = btnduyet;
            string connectionString = Constring.stringg;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Kiểm tra sản phẩm có tồn tại trong cơ sở dữ liệu hay không
                string checkQuery = "UPDATE HoaDon SET TinhTrang=1 WHERE MaHD=@MaHD";
                SqlCommand checkCommand = new SqlCommand(checkQuery, connection);
                checkCommand.Parameters.AddWithValue("@MaHD", MaHD);
                checkCommand.ExecuteNonQuery();
                return RedirectToPage("/BuildMode");
            }

        }

        public IActionResult OnPostThongKe(DateTime dateSearch)
        {
            DateTime datetemp = dateSearch;
            Console.WriteLine(datetemp.ToString());

            DanSachHoaDon.danhsachTK.Clear();
            string query = "SELECT MaHD, MaND, NgayDatHang, DiaChiGiaoHang, PhuongThucThanhToan " +
                   "FROM HoaDon " +
                   "WHERE NgayDatHang > @datetemp";
            using (SqlConnection connection = new SqlConnection(Constring.stringg))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@datetemp", datetemp);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int maHD = reader.GetInt32(0); // MaHD
                    int maND = reader.GetInt32(1); // MaHD

                    DateTime ngayDatHang = reader.GetDateTime(2); // NgayDatHang
                    string diaChi = reader.GetString(3); // PhuongThucThanhToan
                    string phuongThucThanhToan = reader.GetString(4); // PhuongThucThanhToan

                    // Tạo đối tượng HDcanduyet và thêm vào danh sách
                    HDthongke hoadon = new HDthongke(maHD, maND, ngayDatHang, diaChi,phuongThucThanhToan);
                    DanSachHoaDon.danhsachTK.Add(hoadon);
                }
                reader.Close();
            }
            return RedirectToPage("/BuildMode");

        }
    }
}