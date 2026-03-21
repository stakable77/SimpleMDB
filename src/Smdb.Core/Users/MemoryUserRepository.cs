using Smdb.Core.Db;

namespace Smdb.Core.Users;

public class MemoryUserRepository : IUserRepository
{
    private readonly MemoryDatabase _db;

    public MemoryUserRepository(MemoryDatabase db)
    {
        _db = db;
    }

    public IEnumerable<UserModel> List(int page, int size)
        => _db.Users.Skip((page - 1) * size).Take(size);

    public int Count() => _db.Users.Count;

    public UserModel? GetById(int id)
        => _db.Users.FirstOrDefault(u => u.Id == id);

    public UserModel Create(UserModel user)
    {
        user.Id = _db.Users.Count == 0 ? 1 : _db.Users.Max(u => u.Id) + 1;
        _db.Users.Add(user);
        return user;
    }

    public UserModel? Update(int id, UserModel user)
    {
        var existing = GetById(id);
        if (existing is null) return null;

        existing.Name = user.Name;
        existing.Email = user.Email;
        return existing;
    }

    public bool Delete(int id)
    {
        var existing = GetById(id);
        if (existing is null) return false;

        _db.Users.Remove(existing);
        return true;
    }
}
