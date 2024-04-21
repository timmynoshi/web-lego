namespace weblego
{
    public static class DanSachHoaDon
    {
        public static List<HDcanduyet> danhsachHD = new List<HDcanduyet>();
        public static List<HDcanduyet> danhsachHDkhach = new List<HDcanduyet>();
        public static List<HDthongke> danhsachTK = new List<HDthongke>();
    }

    public class HDcanduyet
    {
        public int MaHD { get; set; }
        public DateTime NgayDatHang { get; set; }
        public string PhuongThucThanhToan { get; set; }
        public bool TinhTrang { get; set; }
        public HDcanduyet(int maHD, DateTime ngayDatHang, string phuongThucThanhToan, bool tinhTrang)
        {
            MaHD = maHD;
            NgayDatHang = ngayDatHang;
            PhuongThucThanhToan = phuongThucThanhToan;
            TinhTrang = tinhTrang;

        }
    }

    public class HDthongke
    {
        public int MaHD { get; set; }
        public int MaND { get; set; }
        public DateTime NgayDatHang { get; set; }
        public string DiaChi {  get; set; }
        public string PhuongThucThanhToan { get; set; }

        public HDthongke(int maHD, int maND, DateTime ngayDatHang, string diaChi, string phuongThucThanhToan)
        {
            MaHD = maHD;
            MaND = maND;
            NgayDatHang = ngayDatHang;
            DiaChi = diaChi;
            PhuongThucThanhToan = phuongThucThanhToan;

        }
    }
}
