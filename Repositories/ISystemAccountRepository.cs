using BusinessObjects.Entities;

namespace Repositories;

public interface ISystemAccountRepository
{
    List<SystemAccount> GetAccounts();
    SystemAccount? GetAccountById(short id);
    SystemAccount? GetAccountByEmail(string email);
    short GetNextAccountId();
    void SaveAccount(SystemAccount account);
    void UpdateAccount(SystemAccount account);
    void DeleteAccount(short id);
}
