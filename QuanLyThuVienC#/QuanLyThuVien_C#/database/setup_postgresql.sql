-- Tao database truoc:
--   CREATE DATABASE "BaitaplonPython";
-- Sau do ket noi vao database do va chay file nay.

DROP TABLE IF EXISTS chi_tiet_phieu_phat CASCADE;
DROP TABLE IF EXISTS phieu_phat CASCADE;
DROP TABLE IF EXISTS chi_tiet_phieu_tra CASCADE;
DROP TABLE IF EXISTS phieu_tra CASCADE;
DROP TABLE IF EXISTS chi_tiet_phieu_muon CASCADE;
DROP TABLE IF EXISTS phieu_muon CASCADE;
DROP TABLE IF EXISTS sach CASCADE;
DROP TABLE IF EXISTS nha_xuat_ban CASCADE;
DROP TABLE IF EXISTS the_loai CASCADE;
DROP TABLE IF EXISTS nguoi_muon CASCADE;
DROP TABLE IF EXISTS nguoi_dung CASCADE;

CREATE TABLE nguoi_dung (
    id SERIAL PRIMARY KEY,
    ten_dang_nhap VARCHAR(50) NOT NULL UNIQUE,
    mat_khau_ma_hoa VARCHAR(255) NOT NULL,
    ho_ten VARCHAR(150) NOT NULL,
    vai_tro VARCHAR(50) NOT NULL,
    dang_hoat_dong BOOLEAN NOT NULL DEFAULT TRUE,
    ngay_tao TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE nguoi_muon (
    id SERIAL PRIMARY KEY,
    ma_nguoi_muon VARCHAR(30) NOT NULL UNIQUE,
    ho_ten VARCHAR(150) NOT NULL,
    so_dien_thoai VARCHAR(30),
    email VARCHAR(150),
    dia_chi VARCHAR(255)
);

CREATE TABLE the_loai (
    id SERIAL PRIMARY KEY,
    ten_the_loai VARCHAR(150) NOT NULL UNIQUE
);

CREATE TABLE nha_xuat_ban (
    id SERIAL PRIMARY KEY,
    ten_nha_xuat_ban VARCHAR(150) NOT NULL UNIQUE
);

CREATE TABLE sach (
    id SERIAL PRIMARY KEY,
    ma_sach VARCHAR(30) NOT NULL UNIQUE,
    ten_sach VARCHAR(255) NOT NULL,
    tac_gia VARCHAR(150) NOT NULL,
    the_loai_id INT REFERENCES the_loai(id) ON DELETE SET NULL,
    nha_xuat_ban_id INT REFERENCES nha_xuat_ban(id) ON DELETE SET NULL,
    nam_xuat_ban INT,
    vi_tri_ke VARCHAR(50),
    tong_so_luong INT NOT NULL DEFAULT 0 CHECK (tong_so_luong >= 0),
    so_luong_con INT NOT NULL DEFAULT 0 CHECK (so_luong_con >= 0),
    dang_hoat_dong BOOLEAN NOT NULL DEFAULT TRUE
);

CREATE TABLE phieu_muon (
    id SERIAL PRIMARY KEY,
    ma_phieu_muon VARCHAR(30) NOT NULL UNIQUE,
    nguoi_muon_id INT NOT NULL REFERENCES nguoi_muon(id) ON DELETE RESTRICT,
    nguoi_lap_id INT REFERENCES nguoi_dung(id) ON DELETE SET NULL,
    ngay_muon DATE NOT NULL,
    ngay_hen_tra DATE NOT NULL,
    trang_thai VARCHAR(30) NOT NULL DEFAULT 'dang_muon'
);

CREATE TABLE chi_tiet_phieu_muon (
    id SERIAL PRIMARY KEY,
    phieu_muon_id INT NOT NULL REFERENCES phieu_muon(id) ON DELETE CASCADE,
    sach_id INT NOT NULL REFERENCES sach(id) ON DELETE RESTRICT,
    so_luong INT NOT NULL CHECK (so_luong > 0)
);

CREATE TABLE phieu_tra (
    id SERIAL PRIMARY KEY,
    ma_phieu_tra VARCHAR(30) NOT NULL UNIQUE,
    phieu_muon_id INT NOT NULL REFERENCES phieu_muon(id) ON DELETE RESTRICT,
    nguoi_xu_ly_id INT REFERENCES nguoi_dung(id) ON DELETE SET NULL,
    ngay_tra DATE NOT NULL,
    so_ngay_tre INT NOT NULL DEFAULT 0,
    ghi_chu TEXT
);

CREATE TABLE chi_tiet_phieu_tra (
    id SERIAL PRIMARY KEY,
    phieu_tra_id INT NOT NULL REFERENCES phieu_tra(id) ON DELETE CASCADE,
    chi_tiet_phieu_muon_id INT NOT NULL REFERENCES chi_tiet_phieu_muon(id) ON DELETE RESTRICT,
    so_luong_tra INT NOT NULL DEFAULT 0 CHECK (so_luong_tra >= 0),
    so_luong_hong INT NOT NULL DEFAULT 0 CHECK (so_luong_hong >= 0),
    so_luong_mat INT NOT NULL DEFAULT 0 CHECK (so_luong_mat >= 0),
    ghi_chu_tinh_trang TEXT
);

CREATE TABLE phieu_phat (
    id SERIAL PRIMARY KEY,
    ma_phieu_phat VARCHAR(30) NOT NULL UNIQUE,
    nguoi_muon_id INT NOT NULL REFERENCES nguoi_muon(id) ON DELETE RESTRICT,
    phieu_muon_id INT NOT NULL REFERENCES phieu_muon(id) ON DELETE RESTRICT,
    phieu_tra_id INT REFERENCES phieu_tra(id) ON DELETE SET NULL,
    tong_tien NUMERIC(14,2) NOT NULL DEFAULT 0,
    trang_thai_thanh_toan VARCHAR(30) NOT NULL DEFAULT 'chua_thanh_toan',
    nguoi_lap_id INT REFERENCES nguoi_dung(id) ON DELETE SET NULL,
    ngay_tao TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    ngay_thanh_toan TIMESTAMP
);

CREATE TABLE chi_tiet_phieu_phat (
    id SERIAL PRIMARY KEY,
    phieu_phat_id INT NOT NULL REFERENCES phieu_phat(id) ON DELETE CASCADE,
    sach_id INT REFERENCES sach(id) ON DELETE SET NULL,
    ly_do VARCHAR(100) NOT NULL,
    so_luong INT NOT NULL DEFAULT 1 CHECK (so_luong > 0),
    don_gia_phat NUMERIC(14,2) NOT NULL DEFAULT 0,
    thanh_tien NUMERIC(14,2) NOT NULL DEFAULT 0,
    ghi_chu TEXT
);

INSERT INTO nguoi_dung (ten_dang_nhap, mat_khau_ma_hoa, ho_ten, vai_tro, dang_hoat_dong) VALUES
('quan_tri', '123456', 'Quản trị hệ thống', 'quan_tri', TRUE),
('nhanvien01', '123456', 'Nhân viên thư viện', 'nhan_vien', TRUE);

INSERT INTO nguoi_muon (ma_nguoi_muon, ho_ten, so_dien_thoai, email, dia_chi) VALUES
('NM001', 'Lê Văn An', '0901000001', 'an@gmail.com', 'Đà Nẵng'),
('NM002', 'Phạm Thị Bình', '0901000002', 'binh@gmail.com', 'Huế'),
('NM003', 'Nguyễn Văn Cường', '0901000003', 'cuong@gmail.com', 'Quảng Nam'),
('NM004', 'Trần Thị Dung', '0901000004', 'dung@gmail.com', 'Đà Nẵng');

INSERT INTO the_loai (ten_the_loai) VALUES
('Công nghệ thông tin'),
('Khoa học'),
('Kinh tế'),
('Văn học');

INSERT INTO nha_xuat_ban (ten_nha_xuat_ban) VALUES
('Nhà xuất bản Giáo Dục'),
('Nhà xuất bản Trẻ'),
('Nhà xuất bản Kim Đồng'),
('Nhà xuất bản Lao Động');

INSERT INTO sach (ma_sach, ten_sach, tac_gia, the_loai_id, nha_xuat_ban_id, nam_xuat_ban, vi_tri_ke, tong_so_luong, so_luong_con, dang_hoat_dong) VALUES
('S001', 'Lập trình Python cơ bản', 'Nguyễn Văn A', 1, 1, 2022, 'A1', 10, 10, TRUE),
('S002', 'Cơ sở dữ liệu', 'Trần Văn B', 1, 1, 2021, 'A2', 7, 7, TRUE),
('S003', 'Tắt đèn', 'Ngô Tất Tố', 4, 2, 2019, 'B1', 6, 6, TRUE),
('S004', 'Vật lý đại cương', 'Lê Văn C', 2, 1, 2020, 'C1', 4, 4, TRUE),
('S005', 'Kinh tế vi mô', 'Phạm Văn D', 3, 4, 2023, 'D1', 5, 5, TRUE);

INSERT INTO phieu_muon (ma_phieu_muon, nguoi_muon_id, nguoi_lap_id, ngay_muon, ngay_hen_tra, trang_thai) VALUES
('PM001', 1, 1, CURRENT_DATE - INTERVAL '7 days', CURRENT_DATE + INTERVAL '7 days', 'dang_muon'),
('PM002', 3, 2, CURRENT_DATE - INTERVAL '15 days', CURRENT_DATE - INTERVAL '2 days', 'dang_muon');

INSERT INTO chi_tiet_phieu_muon (phieu_muon_id, sach_id, so_luong) VALUES
(1, 1, 1),
(2, 2, 1);

UPDATE sach SET so_luong_con = so_luong_con - 1 WHERE id IN (1, 2);
