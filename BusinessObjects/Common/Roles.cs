namespace BusinessObjects.Common;

/// <summary>
/// Các role được dùng trong hệ thống.
/// Admin được cấu hình trong appsettings.json nên không nằm trong bảng SystemAccount.
/// Staff = 1, Lecturer = 2 (theo database).
/// </summary>
public static class Roles
{
    public const int Staff = 1;
    public const int Lecturer = 2;
    public const int Admin = 3;

    public static string GetRoleName(int? role) => role switch
    {
        Staff => "Staff",
        Lecturer => "Lecturer",
        Admin => "Admin",
        _ => "Unknown"
    };
}
