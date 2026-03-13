namespace Smdb.Core.Users;

public class DefaultUserService : IUserService
{
    private readonly IUserRepository _repo;

    public DefaultUserService(IUserRepository repo)
    {
        _repo = repo;
    }

    public IEnumerable<UserModel> List(int page, int size)
        => _repo.List(page, size);

    public UserModel? Get(int id)
        => _repo.GetById(id);

    public UserModel Create(UserModel user)
        => _repo.Create(user);

    public UserModel? Update(int id, UserModel user)
        => _repo.Update(id, user);

    public bool Delete(int id)
        => _repo.Delete(id);
}
