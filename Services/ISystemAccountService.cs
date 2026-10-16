using BusinessObjects.Entities;

namespace Services;

public interface ISystemAccountService
{
    List<SystemAccount> GetAccounts();
    List<SystemAccount> SearchAccounts(string? keyword);
    SystemAccount? GetAccountById(short id);
    SystemAccount? GetAccountByEmail(string email);

    /// <summary>Kiểm tra đăng nhập. Trả về null nếu sai email/mật khẩu.</summary>
    SystemAccount? Authenticate(string email, string password);

    void SaveAccount(SystemAccount account);
    void UpdateAccount(SystemAccount account);
    void DeleteAccount(short id);
}
