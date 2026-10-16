using BusinessObjects.Entities;
using Repositories;

namespace Services;

public class SystemAccountService : ISystemAccountService
{
    private readonly ISystemAccountRepository _accountRepository;

    public SystemAccountService(ISystemAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public List<SystemAccount> GetAccounts() => _accountRepository.GetAccounts();

    public List<SystemAccount> SearchAccounts(string? keyword)
    {
        var accounts = _accountRepository.GetAccounts();
        if (string.IsNullOrWhiteSpace(keyword))
        {
            return accounts;
        }

        keyword = keyword.Trim().ToLower();
        return accounts
            .Where(a => (a.AccountName ?? string.Empty).ToLower().Contains(keyword)
                     || (a.AccountEmail ?? string.Empty).ToLower().Contains(keyword))
            .ToList();
    }

    public SystemAccount? GetAccountById(short id) => _accountRepository.GetAccountById(id);

    public SystemAccount? GetAccountByEmail(string email) => _accountRepository.GetAccountByEmail(email);

    public SystemAccount? Authenticate(string email, string password)
    {
        var account = _accountRepository.GetAccountByEmail(email);
        if (account != null && account.AccountPassword == password)
        {
            return account;
        }
        return null;
    }

    public void SaveAccount(SystemAccount account) => _accountRepository.SaveAccount(account);

    public void UpdateAccount(SystemAccount account) => _accountRepository.UpdateAccount(account);

    public void DeleteAccount(short id) => _accountRepository.DeleteAccount(id);
}
