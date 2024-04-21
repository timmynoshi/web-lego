namespace weblego
{
    public static class DanSachHoaDon
    {
        public static List<HDcanduyet> danhsachHD = new List<HDcanduyet>();
        public static List<HDcanduyet> danhsachHDkhach = new List<HDcanduyet>();
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
}
