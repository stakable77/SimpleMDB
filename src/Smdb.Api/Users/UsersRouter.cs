using Shared.Http;
using Smdb.Core.Users;

namespace Smdb.Api.Users;

public class UsersRouter : HttpRouter
{
    private readonly UsersController _ctrl;

    public UsersRouter(UsersController ctrl)
    {
        _ctrl = ctrl;
    }

    public void MapRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/", (int page, int size) => _ctrl.List(page, size));
        app.MapGet("/{id}", (int id) => _ctrl.Read(id));
        app.MapPost("/", (UserModel user) => _ctrl.Create(user));
        app.MapPut("/{id}", (int id, UserModel user) => _ctrl.Update(id, user));
        app.MapDelete("/{id}", (int id) => _ctrl.Delete(id));
    }
}