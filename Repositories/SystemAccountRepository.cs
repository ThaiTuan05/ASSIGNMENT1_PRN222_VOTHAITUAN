using BusinessObjects.Entities;
using DataAccessObjects;

namespace Repositories;

public class SystemAccountRepository : ISystemAccountRepository
{
    private readonly SystemAccountDAO _accountDAO = new();

    public List<SystemAccount> GetAccounts() => _accountDAO.GetAccounts();

    public SystemAccount? GetAccountById(short id) => _accountDAO.GetAccountById(id);

    public SystemAccount? GetAccountByEmail(string email) => _accountDAO.GetAccountByEmail(email);

    public short GetNextAccountId() => _accountDAO.GetNextAccountId();

    public void SaveAccount(SystemAccount account) => _accountDAO.SaveAccount(account);

    public void UpdateAccount(SystemAccount account) => _accountDAO.UpdateAccount(account);

    public void DeleteAccount(short id) => _accountDAO.DeleteAccount(id);
}
