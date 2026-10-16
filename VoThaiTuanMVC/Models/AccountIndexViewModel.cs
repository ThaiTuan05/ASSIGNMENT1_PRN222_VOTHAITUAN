using BusinessObjects.Entities;

namespace VoThaiTuanMVC.Models;

public class AccountIndexViewModel
{
    public List<SystemAccount> Accounts { get; set; } = new();
    public string? Search { get; set; }
    public SystemAccount Form { get; set; } = new();
    public string? OpenModal { get; set; }
}
