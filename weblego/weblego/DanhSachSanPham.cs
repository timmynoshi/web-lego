namespace weblego
{
    public class SanPham
    {
        public string MaSP { get; set; }
        public string TenSP { get; set; }
        public string ChuDe { get; set; }
        public int DoTuoi { get; set; }
        public int SoLuongTonKho { get; set; }
        public int DonGia { get; set; }
        public string HinhAnh { get; set; }

        public SanPham(string maSP, string tenSP, string chuDe, int doTuoi, int soLuongTonKho, int donGia, string hinhAnh)
        {
            MaSP = maSP;
            TenSP = tenSP;
            ChuDe = chuDe;
            DoTuoi = doTuoi;
            SoLuongTonKho = soLuongTonKho;
            DonGia = donGia;
            HinhAnh = hinhAnh;
        }
    }

    public class SanPhamRev
    {
        public int MaND { get; set; }
        public string MaSP { get; set; }
        

        public SanPhamRev(int maND, string maSP)
        {
            MaND = maND;
            MaSP = maSP;
            
        }
    }



    public static class DanhSachSanPham
    {
        public static List<SanPham> danhSachSanPham = new List<SanPham>();
        public static List<SanPhamRev> danhSachGioHang = new List<SanPhamRev>();
    }
}
