namespace Smdb.Api.Users;

using System.Collections;
using System.Collections.Specialized;
using System.Net;
using System.Text.Json;
using Shared.Http;
using Smdb.Core.Users;

public class UsersController
{
    private readonly IUserService _service;

    public UsersController(IUserService service)
    {
        _service = service;
    }

    // GET /api/v1/users?page=1&size=10
    public async Task ReadUsers(HttpListenerRequest req, HttpListenerResponse res,
        Hashtable props, Func<Task> next)
    {
        int page = int.TryParse(req.QueryString["page"], out int p) ? p : 1;
        int size = int.TryParse(req.QueryString["size"], out int s) ? s : 10;

        var result = _service.List(page, size);
        await JsonUtils.SendPagedResultResponse(req, res, props, result, page, size);
        await next();
    }

    // POST /api/v1/users
    public async Task CreateUser(HttpListenerRequest req,
        HttpListenerResponse res, Hashtable props, Func<Task> next)
    {
        var text = (string)props["req.text"]!;
        var user = JsonSerializer.Deserialize<UserModel>(text, JsonSerializerOptions.Web);

        var result = _service.Create(user!);
        await JsonUtils.SendResultResponse(req, res, props, result);
        await next();
    }

    // GET /api/v1/users/:id
    public async Task ReadUser(HttpListenerRequest req, HttpListenerResponse res,
        Hashtable props, Func<Task> next)
    {
        var uParams = (NameValueCollection)props["req.params"]!;
        int id = int.TryParse(uParams["id"], out int i) ? i : -1;

        var result = _service.Get(id);
        await JsonUtils.SendResultResponse(req, res, props, result);
        await next();
    }

    // PUT /api/v1/users/:id
    public async Task UpdateUser(HttpListenerRequest req,
        HttpListenerResponse res, Hashtable props, Func<Task> next)
    {
        var uParams = (NameValueCollection)props["req.params"]!;
        int id = int.TryParse(uParams["id"], out int i) ? i : -1;

        var text = (string)props["req.text"]!;
        var user = JsonSerializer.Deserialize<UserModel>(text, JsonSerializerOptions.Web);

        var result = _service.Update(id, user!);
        await JsonUtils.SendResultResponse(req, res, props, result);
        await next();
    }

    // DELETE /api/v1/users/:id
    public async Task DeleteUser(HttpListenerRequest req,
        HttpListenerResponse res, Hashtable props, Func<Task> next)
    {
        var uParams = (NameValueCollection)props["req.params"]!;
        int id = int.TryParse(uParams["id"], out int i) ? i : -1;

        var result = _service.Delete(id);
        await JsonUtils.SendResultResponse(req, res, props, result);
        await next();
    }
}
