namespace Smdb.Core.Users;

using System.Net;
using Shared.Http;

public class DefaultUserService : IUserService
{
    private readonly IUserRepository _repo;

    public DefaultUserService(IUserRepository repo)
    {
        _repo = repo;
    }

    public Result<PagedResult<UserModel>> List(int page, int size)
    {
        var users = _repo.List(page, size);
        var total = _repo.Count();
        return new Result<PagedResult<UserModel>>(new PagedResult<UserModel>(total, users));
    }

    public Result<UserModel> Get(int id)
    {
        var user = _repo.GetById(id);
        if (user is null)
            return new Result<UserModel>(
                new Exception($"User with id {id} not found."),
                (int)HttpStatusCode.NotFound);
        return new Result<UserModel>(user);
    }

    public Result<UserModel> Create(UserModel user)
    {
        var created = _repo.Create(user);
        return new Result<UserModel>(created, (int)HttpStatusCode.Created);
    }

    public Result<UserModel> Update(int id, UserModel user)
    {
        var updated = _repo.Update(id, user);
        if (updated is null)
            return new Result<UserModel>(
                new Exception($"User with id {id} not found."),
                (int)HttpStatusCode.NotFound);
        return new Result<UserModel>(updated);
    }

    public Result<bool> Delete(int id)
    {
        var deleted = _repo.Delete(id);
        if (!deleted)
            return new Result<bool>(
                new Exception($"User with id {id} not found."),
                (int)HttpStatusCode.NotFound);
        return new Result<bool>(true);
    }
}
