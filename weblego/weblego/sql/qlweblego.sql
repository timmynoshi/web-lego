USE master
GO

IF EXISTS (SELECT * FROM sysdatabases WHERE name='QuanLyWebLEGO')
	DROP DATABASE QuanLyWebLEGO
GO

CREATE DATABASE QuanLyWebLEGO
GO

USE QuanLyWebLEGO
GO

CREATE TABLE NguoiDung ( MaND int IDENTITY(1,1),
						 TenND nvarchar(50),
						 DiaChi nvarchar(100),
						 SDT nvarchar(10),
						 TaiKhoan nvarchar(50),
						 MatKhau nvarchar(50),
						 QuyenHan nvarchar(10),
						 PRIMARY KEY(MaND)
						 )
GO

CREATE TABLE SanPham ( MaSP nvarchar(10),
					   TenSP nvarchar(50),
					   ChuDe nvarchar(20),
					   DoTuoi int,
					   SoLuongTonKho int,
					   DonGia int,
					   HinhAnh nvarchar(200),
					   PRIMARY KEY(MaSP)
					   )
GO

CREATE TABLE HoaDon ( MaHD nvarchar(4),
					  NgayDatHang date,
					  NgayDuKienGiao date,
					  DiaChiGiaoHang nvarchar(100),
					  MaND int,
					  PhuongThucThanhToan nvarchar(6),
					  TinhTrang bit,
					  PRIMARY KEY(MaHD),
					  FOREIGN KEY(MaND) REFERENCES NguoiDung(MaND)
					  )
GO

CREATE TABLE CTHD ( MaHD nvarchar(4),
					MaSP nvarchar(10),
					SoLuong int,
					ThanhTien int,
					PRIMARY KEY(MaHD,MaSP),
					FOREIGN KEY(MaHD) REFERENCES HoaDon(MaHD),
					FOREIGN KEY(MaSP) REFERENCES SanPham(MaSP)
					)
GO

CREATE TABLE GioHang ( MaND int,
					   MaSP nvarchar(10),
					   PRIMARY KEY(MaND, MaSP),
					   FOREIGN KEY (MaND) REFERENCES NguoiDung(MaND),
					   FOREIGN KEY (MaSP) REFERENCES SanPham(MaSP)
					   )
GO




INSERT INTO NguoiDung VALUES (N'Nguyễn Thanh Tín', N'123 Hậu Giang', '0703020165', 'admin', '123', 'admin'),
							 (N'Vương Ngọc Khôi Nguyên', N'321 Điện Biên', '1234567891', 'vnkn123', '123', 'khach')
GO

INSERT INTO SanPham VALUES ('75361','Spider Tank', 'Star Wars', 9, 2, 50, '75361.webp'), -- Star war
						   ('75375','Millennium Falcon', 'Star Wars', 18, 1, 85, '75375.webp'),
						   ('75313','AT-AT', 'Star Wars', 18, 1, 850, '75313.webp'), 
						   ('71806','Cole Elemental Earth Mech', 'Ninjago', 7, 3, 20, '71806.webp'), --Ninjago
						   ('71741','NINJAGO City Gardens', 'Ninjago', 14, 1, 350, '71741.webp'),
						   ('71783','Kais Mech Rider EVO', 'Ninjago', 7, 2, 45, '71783.webp'),
						   ('42180','Mars Crew Exploration Rover', 'Technic', 11, 2, 150, '42180.webp'), --Technic
						   ('42181','VTOL Heavy Cargo Spaceship LT81', 'Technic', 10, 1, 110, '42181.webp'),
						   ('42178','Surface Space Loader LT78', 'Technic', 8, 4, 35, '42178.webp'),
						   ('11035','Creative Houses', 'Classic', 4, 6,55, '11035.webp'), --Classic
						   ('10698','LEGO Large Creative Brick Box', 'Classic', 4, 4, 60, '10698.webp'),
						   ('11037','Creative Space Planets', 'Classic', 5, 4, 35, '11037.webp')
GO

SELECT *
FROM NguoiDung

SELECT *
FROM SanPham

SELECT TaiKhoan, SDT, DiaChi
FROM NguoiDung
WHERE TaiKhoan='admin'

SELECT * FROM NguoiDung

SELECT COUNT(*) FROM NguoiDung WHERE MaND = 2

UPDATE NguoiDung SET TenND = 'test', DiaChi = 'test', SDT = 'test', TaiKhoan = 'test', MatKhau = 'test' WHERE MaND = 2