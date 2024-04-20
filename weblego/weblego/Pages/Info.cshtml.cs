using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace weblego.Pages
{
    public class InfoModel : PageModel
    {
        [BindProperty]
        public string TenND { get; set; }
        [BindProperty]
        public string TaiKhoan { get; set; }
        [BindProperty]
        public string SDT { get; set; }
        [BindProperty]
        public string DiaChi { get; set; }


        public void OnGet()
        {
            TaiKhoan = QuyenHan.tentaikhoan;
            string query = "SELECT TenND, TaiKhoan, SDT, DiaChi FROM NguoiDung WHERE TaiKhoan=@TaiKhoan";

            using (SqlConnection connection = new SqlConnection(Constring.stringg))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@TaiKhoan", TaiKhoan);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    TenND = reader.GetString(0);
                    TaiKhoan = reader.GetString(1);
                    SDT = reader.GetString(2);
                    DiaChi = reader.GetString(3);
                }

                reader.Close();
            }
            Console.WriteLine(TenND, DiaChi, SDT, TaiKhoan);
        }

        public IActionResult OnPost()
        {

            Console.WriteLine(TenND, DiaChi, SDT, TaiKhoan);
            string query = "UPDATE NguoiDung SET TenND = @TenND, SDT = @SDT, DiaChi = @DiaChi WHERE TaiKhoan = @TaiKhoan";

            using (SqlConnection connection = new SqlConnection(Constring.stringg))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@TenND", TenND);
                command.Parameters.AddWithValue("@SDT", SDT);
                command.Parameters.AddWithValue("@DiaChi", DiaChi);
                command.Parameters.AddWithValue("@TaiKhoan", TaiKhoan);


                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                connection.Close();
                Console.WriteLine(TenND, DiaChi, SDT, TaiKhoan);
                if (rowsAffected > 0)
                {
                    Console.WriteLine("Insert thành công!");

                    return RedirectToPage("/Info");
                }
                else
                {
                    Console.WriteLine("Insert không thành công!");

                    // Xử lý khi cập nhật không thành công
                    return Page();
                }
            }
        }
    }
}
