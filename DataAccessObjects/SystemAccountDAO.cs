using BusinessObjects;
using BusinessObjects.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccessObjects;

/// <summary>
/// Data Access Object cho bảng SystemAccount.
/// AccountID là smallint không IDENTITY nên phải tự sinh ID mới.
/// </summary>
public class SystemAccountDAO
{
    public List<SystemAccount> GetAccounts()
    {
        using var context = new FUNewsManagementContext();
        return context.SystemAccounts
            .AsNoTracking()
            .OrderBy(a => a.AccountId)
            .ToList();
    }

    public SystemAccount? GetAccountById(short id)
    {
        using var context = new FUNewsManagementContext();
        return context.SystemAccounts
            .AsNoTracking()
            .FirstOrDefault(a => a.AccountId == id);
    }

    public SystemAccount? GetAccountByEmail(string email)
    {
        using var context = new FUNewsManagementContext();
        return context.SystemAccounts
            .AsNoTracking()
            .FirstOrDefault(a => a.AccountEmail != null && a.AccountEmail == email);
    }

    public short GetNextAccountId()
    {
        using var context = new FUNewsManagementContext();
        var max = context.SystemAccounts.Any()
            ? context.SystemAccounts.Max(a => (int)a.AccountId)
            : 0;
        return (short)(max + 1);
    }

    public void SaveAccount(SystemAccount account)
    {
        using var context = new FUNewsManagementContext();
        account.AccountId = GetNextAccountId();
        context.SystemAccounts.Add(account);
        context.SaveChanges();
    }

    public void UpdateAccount(SystemAccount account)
    {
        using var context = new FUNewsManagementContext();
        context.SystemAccounts.Update(account);
        context.SaveChanges();
    }

    public void DeleteAccount(short id)
    {
        using var context = new FUNewsManagementContext();
        var account = context.SystemAccounts.FirstOrDefault(a => a.AccountId == id);
        if (account != null)
        {
            context.SystemAccounts.Remove(account);
            context.SaveChanges();
        }
    }
}
