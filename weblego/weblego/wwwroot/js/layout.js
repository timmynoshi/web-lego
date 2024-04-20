$(document).ready(function () {
    $("#btnlogin").click(function () {
        // Chuyển hướng trang đến trang login
        window.location.href = "/Login"; // Đường dẫn đến trang login
    });

    $("#btnlogout").click(function () {
        // Gửi yêu cầu đăng xuất đến máy chủ
        $.get("/Logout", function () {
            // Nếu đăng xuất thành công, chuyển hướng đến trang khác hoặc thực hiện các hành động khác
            window.location.href = '/'; // Ví dụ: chuyển hướng đến trang chính
        });
    });

    $("#btnbuild").click(function () {
        // Chuyển hướng đến trang BuildMode khi nút được click
        window.location.href = "/BuildMode";
    });

    $("#dropbox_theme").click(function () {
        window.location.href = "/tesstttt"; // Chuyển hướng về trang chính
    });

    $("#btninfo").click(function () {
        window.location.href = "/Info"; // Chuyển hướng về trang chính
    });

    $("#dropbox_age").click(function () {
        window.location.href = "/Index"; // Chuyển hướng về trang chính
    });
});

