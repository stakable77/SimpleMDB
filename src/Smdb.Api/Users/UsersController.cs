using Smdb.Core.Users;

namespace Smdb.Api.Users;

public class UsersController
{
    private readonly IUserService _service;

    public UsersController(IUserService service)
    {
        _service = service;
    }

    public object List(int page, int size) => _service.List(page, size);
    public object Read(int id) => _service.Get(id);
    public object Create(UserModel user) => _service.Create(user);
    public object Update(int id, UserModel user) => _service.Update(id, user);
    public object Delete(int id) => _service.Delete(id);
}
