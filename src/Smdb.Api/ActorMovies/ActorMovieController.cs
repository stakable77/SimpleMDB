namespace Smdb.Api.ActorsMovies;

using System.Collections;
using System.Collections.Specialized;
using System.Net;
using System.Text.Json;
using Shared.Http;
using Smdb.Core.ActorsMovies;

public class ActorsMoviesController
{
    private readonly DefaultActorMovieService _service;

    public ActorsMoviesController(DefaultActorMovieService service)
    {
        _service = service;
    }

    // GET /api/v1/actors-movies
    public async Task ReadAll(HttpListenerRequest req, HttpListenerResponse res,
        Hashtable props, Func<Task> next)
    {
        var result = _service.GetAll();
        await JsonUtils.SendResultResponse(req, res, props, result);
        await next();
    }

    // POST /api/v1/actors-movies
    public async Task Create(HttpListenerRequest req, HttpListenerResponse res,
        Hashtable props, Func<Task> next)
    {
        var text = (string)props["req.text"]!;
        var model = JsonSerializer.Deserialize<ActorMovieModel>(text, JsonSerializerOptions.Web);

        var result = _service.Create(model!);
        await JsonUtils.SendResultResponse(req, res, props, result);
        await next();
    }

    // GET /api/v1/actors-movies/:id
    public async Task Read(HttpListenerRequest req, HttpListenerResponse res,
        Hashtable props, Func<Task> next)
    {
        var p = (NameValueCollection)props["req.params"]!;
        int id = int.TryParse(p["id"], out int i) ? i : -1;

        var result = _service.Get(id);
        await JsonUtils.SendResultResponse(req, res, props, result);
        await next();
    }

    // DELETE /api/v1/actors-movies/:id
    public async Task Delete(HttpListenerRequest req, HttpListenerResponse res,
        Hashtable props, Func<Task> next)
    {
        var p = (NameValueCollection)props["req.params"]!;
        int id = int.TryParse(p["id"], out int i) ? i : -1;

        var result = _service.Delete(id);
        await JsonUtils.SendResultResponse(req, res, props, result);
        await next();
    }
}
