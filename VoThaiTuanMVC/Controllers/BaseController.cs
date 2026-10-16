using BusinessObjects.Common;
using Microsoft.AspNetCore.Mvc;

namespace VoThaiTuanMVC.Controllers;

/// <summary>Controller cơ sở cung cấp thông tin user đang đăng nhập qua Session.</summary>
public abstract class BaseController : Controller
{
    protected short? CurrentUserId => (short?)HttpContext.Session.GetInt32("UserId");
    protected string? CurrentUserName => HttpContext.Session.GetString("UserName");
    protected string? CurrentUserEmail => HttpContext.Session.GetString("UserEmail");
    protected int? CurrentRole => HttpContext.Session.GetInt32("UserRole");

    protected bool IsAuthenticated => CurrentRole.HasValue;
    protected bool IsAdmin => CurrentRole == Roles.Admin;
    protected bool IsStaff => CurrentRole == Roles.Staff;
    protected bool IsLecturer => CurrentRole == Roles.Lecturer;

    protected bool HasRole(params int[] roles) => CurrentRole.HasValue && roles.Contains(CurrentRole.Value);
}
