<?php
$serverName = "10.10.0.101"; // Địa chỉ IP hoặc tên máy chủ SQL Server
$connectionOptions = [
    "Database" => "moodle",       // Tên CSDL
    "Uid" => "sa",                // Tên đăng nhập SQL Server
    "PWD" => "Password789"        // Mật khẩu
];

// Thử kết nối
$conn = sqlsrv_connect($serverName, $connectionOptions);

if ($conn) {
    echo "<h2 style='color:green;'>✅ Kết nối SQL Server thành công!</h2>";
} else {
    echo "<h2 style='color:red;'>❌ Kết nối thất bại.</h2><pre>";
    print_r(sqlsrv_errors());
    echo "</pre>";
}
?>