QUAN LY THU VIEN - BAN C# WINFORMS + POSTGRESQL + DESIGNER FORMS

1. Muc tieu
- Day la ban C# duoc sua lai de bam sat schema PostgreSQL suy ra tu Code.zip cua ban.
- Form van la WinForms Designer nen ban co the mo View Designer va keo tha sua giao dien.

2. Chuoi ket noi mac dinh
- File: App.config
- Mac dinh dang de:
  Host=localhost
  Port=5432
  Database=BaitaplonPython
  Username=postgres
  Password=25082006

Ban hay sua lai theo may cua ban neu can.

3. Cac bang da duoc map theo schema Code.zip
- nguoi_dung
- nguoi_muon
- the_loai
- nha_xuat_ban
- sach
- phieu_muon
- chi_tiet_phieu_muon
- phieu_tra
- chi_tiet_phieu_tra
- phieu_phat
- chi_tiet_phieu_phat

4. Cac man hinh da noi PostgreSQL
- Dang nhap
- Sach
- Nguoi muon
- The loai
- Nha xuat ban
- Nguoi dung
- Phieu muon
- Phieu tra
- Phieu phat
- Thong ke

5. Luu y
- Moi mat khau dang duoc dung theo dung cach app Python cua ban dang dung:
  so sanh truc tiep voi cot mat_khau_ma_hoa
- Nghia la neu SQL that cua ban dang luu plain text / hash gia lap thi C# nay se bam theo dung logic do.
- Muc phat dang bam logic trong app Python:
  + tre han: 5000 / ngay
  + lam hong: 45000 / cuon
  + lam mat: 80000 / cuon

6. Cach chay
- Tao database BaitaplonPython
- Chay file database/setup_postgresql.sql neu muon dung bo du lieu mau
- Mo solution QuanLyThuVien.sln bang Visual Studio
- Restore NuGet
- Chay

7. Tai khoan mau trong script
- quan_tri / 123456
- nhanvien01 / 123456

8. Ghi chu trung thuc
- Moi truong tao file khong co .NET SDK / Visual Studio desktop nen chua build duoc trong container.
- Mình da sua source de khop schema va ten bang/cot theo Code.zip tot nhat co the.
