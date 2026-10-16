# PRN222 - Assignment 01 (ASP.NET Core MVC)

**Sinh viên:** VoThaiTuan - **Lớp:** QE190154
**Solution:** `VoThaiTuan_QE190154_A01.sln`
**Project MVC:** `VoThaiTuanMVC`

## 1. Kiến trúc (3-Layers + Repository + Singleton)

```
VoThaiTuan_QE190154_A01.sln
├── BusinessObjects      (Entities + FUNewsManagementContext - EF Core)
├── DataAccessObjects    (CategoryDAO, NewsArticleDAO, SystemAccountDAO, TagDAO)
├── Repositories         (interface + impl, gọi DAO)
├── Services             (interface + impl, business logic / LINQ / report)
└── VoThaiTuanMVC        (ASP.NET Core MVC: Controllers + Views)
```

- Controller **không** truy cập DB trực tiếp; mọi truy cập đi qua `Service -> Repository -> DAO -> DbContext`.
- Repository/Service được đăng ký theo **Singleton pattern** trong `Program.cs`.
- Connection string đọc từ `appsettings.json` (key `ConnectionStrings:DefaultConnectionString`).
- Tài khoản Admin lấy từ `appsettings.json` (key `AdminAccount`).

## 2. Yêu cầu môi trường

- .NET SDK 8.0 (project target `net8.0`).
- SQL Server (LocalDB đã có sẵn: `(localdb)\MSSQLLocalDB`).

## 3. Tạo database

Chạy script `Database/FUNewsManagement.sql` (bản gốc nằm ở `E:\GAME\FUNewsManagement.sql`):

```powershell
sqlcmd -S "(localdb)\MSSQLLocalDB" -E -i "Database\FUNewsManagement.sql"
```

> Script gốc có 1 dòng cuối `ALTER DATABASE [FUNewsManagementFall2024]` bị sai tên DB; bản trong `Database/` đã được bỏ dòng lỗi đó.

## 4. Chạy ứng dụng

```powershell
cd VoThaiTuanMVC
dotnet run
```

Mở trình duyệt: **http://localhost:5012** (mặc định vào trang **Login**).

## 5. Tài khoản demo

| Role | Email | Password |
|------|-------|----------|
| Admin | admin@FUNewsManagementSystem.org | @@abc123@@ |
| Staff | IsabellaDavid@FUNewsManagement.org | @1 |
| Lecturer | EmmaWilliam@FUNewsManagement.org | @1 |

## 6. Chức năng theo role

- **Khách (không đăng nhập):** xem news có `NewsStatus = active`, tìm kiếm, lọc theo category, xem chi tiết.
- **Admin:** quản lý account (CRUD + search, popup dialog); tạo báo cáo thống kê news theo StartDate–EndDate (theo CreatedDate, sắp xếp giảm dần).
- **Staff:** quản lý category (CRUD + search; **không xoá được** category đang thuộc news article nào); quản lý news (CRUD + tags + search, popup dialog); cập nhật profile của mình; xem lịch sử news do mình tạo.
- **Lecturer:** chỉ xem news active.

Tạo/Sửa dùng **popup dialog (Bootstrap modal)**; Xoá luôn có **xác nhận**.

## 7. Giao diện

Thiết kế theo phong cách **trang báo**: topbar (ngày + trạng thái đăng nhập), masthead có logo, thanh điều hướng (sticky, đổi theo role), **bài nổi bật (hero)** + lưới bài viết có thumbnail gradient theo category, sidebar "Browse by Category" và "Popular Tags"; trang chi tiết bài báo có tiêu đề serif, byline, lead, tags và sidebar "Related Stories". Các trang quản trị dùng page-header + bảng trong card, popup dialog và badge trạng thái. Toàn bộ style nằm ở `VoThaiTuanMVC/wwwroot/css/site.css`.

## 8. Ghi chú kỹ thuật

- Entities sinh bằng EF Core scaffold:
  ```powershell
  dotnet ef dbcontext scaffold "Server=(localdb)\MSSQLLocalDB;Database=FUNewsManagement;Trusted_Connection=True;TrustServerCertificate=True" Microsoft.EntityFrameworkCore.SqlServer --output-dir Entities --context-dir . --context FUNewsManagementContext --data-annotations
  ```
- `FUNewsManagementContext.OnConfiguring` đọc connection string từ `appsettings.json` (không hard-code).
- Quan hệ NewsArticle - Tag là many-to-many qua bảng `NewsTag`.
