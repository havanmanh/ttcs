CREATE DATABASE QuanLiBanHang
go

USE QuanLiBanHang;

CREATE TABLE Login
(
	UserName char(50) ,
	Pass Char(50),
	Role Char(50),
	CONSTRAINT PK_Login PRIMARY KEY (UserName, Pass, Role)
)
CREATE TABLE Ban
(
	MaBan int PRIMARY KEY IDENTITY,
	TenBan NVARCHAR(50)
);

CREATE TABLE ThucDon
(
	MaMon INT PRIMARY KEY IDENTITY,
	TenMon NVARCHAR(100) NOT NULL,
	Gia INT NOT NULL,
	loai NVARCHAR(50) NOT NULL
)

CREATE TABLE HoaDon
(
	MaHD INT PRIMARY KEY IDENTITY,
	MaBan INT NOT NULL,
	TongTien INT NOT NULL,
	ThoiGian datetime,
	CONSTRAINT FK_MaBan	FOREIGN KEY (MaBan)	REFERENCES Ban(MaBan)
);

CREATE TABLE CTHD
(
	MaHD INT NOT NULL,
	MaMon INT NOT NULL,
	SoLuong INT NOT NULL,
	ThanhGia INT NOT NULL,
	CONSTRAINT PK_CTHD PRIMARY KEY (MaHD, MaMon),
	CONSTRAINT FK_MaHD FOREIGN KEY (MaHD) REFERENCES HoaDon(MaHD),
	CONSTRAINT FK_MaMon FOREIGN KEY (MaMon) REFERENCES ThucDon(MaMon)
)

CREATE TABLE Ban_HoaDon
(
	MaBan INT NOT NULL,
	MaMon INT NOT NULL,
	SoLuong INT NOT NULL,
	CONSTRAINT PK_Ban_HoaDon PRIMARY KEY (MaBan, MaMon),
	CONSTRAINT FK_MaBan_HoaDon FOREIGN KEY (MaBan) REFERENCES Ban(MaBan),
	CONSTRAINT FK_MaMon_HoaDon FOREIGN KEY (MaMon) REFERENCES ThucDon(MaMon)
)

go
insert into Login values('employe', '123', 'employe'), ('admin', '123', 'ADMIN'), ('ADMIN', '123', 'ADMIN')
go
insert into ThucDon(TenMon, Gia, loai)
values(N'cocacola', '10000', 'drink'),
(N'nước cam', '10000', 'drink'),
(N'trà sữa', '20000', 'tea'),
(N'trà tranh', '10000', 'tea'),
(N'trà đào', '15000', 'tea'),
(N'bf người lớn', '100000', 'buffet'),
(N'bf trẻ con', '50000', 'buffet'),
(N'lẩu nướng', '150000', 'buffet'),
(N'rượu trắng', '15000', 'wine'),
(N'soju', '10000', 'wine'),
(N'rượu chuối', '10000', 'wine')

go

insert into Ban(TenBan) values ('Bàn 1'),
('Bàn 2'),
('Bàn 3'),
('Bàn 4'),
('Bàn 5'),
('Bàn 6'),
('Bàn 7'),
('Bàn 8'),
('Bàn 9'),
('Bàn 10')

go

	-- reset bảng hóa đơn của hóa đơn
		DBCC CHECKIDENT ('HoaDon', RESEED, 0)