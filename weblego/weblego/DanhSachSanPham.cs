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



    public static class DanhSachSanPham
    {
        public static List<SanPham> danhSachSanPham = new List<SanPham>();
    }
}
