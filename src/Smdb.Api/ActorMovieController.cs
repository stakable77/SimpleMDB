namespace Smdb.Api.ActorsMovies;

using System.Collections;
using System.Collections.Specialized;
using System.Net;
using System.Text.Json;
using Shared.Http;
using Smdb.Core.ActorsMovies;

public class ActorsMoviesController
{
    private readonly DefaultActorMovieService service;

    public ActorsMoviesController(DefaultActorMovieService service)
    {
        this.service = service;
    }

    public async Task ReadAll(HttpListenerRequest req, HttpListenerResponse res,
    Hashtable props, Func<Task> next)
    {
        var result = await service.ReadAll();
        await JsonUtils.SendResultResponse(req, res, props, result);
        await next();
    }

    public async Task Create(HttpListenerRequest req, HttpListenerResponse res,
    Hashtable props, Func<Task> next)
    {
        var text = (string)props["req.text"]!;
        var model = JsonSerializer.Deserialize<ActorMovieModel>(text, JsonSerializerOptions.Web);

        var result = await service.Create(model!);
        await JsonUtils.SendResultResponse(req, res, props, result);
        await next();
    }

    public async Task Read(HttpListenerRequest req, HttpListenerResponse res,
    Hashtable props, Func<Task> next)
    {
        var p = (NameValueCollection)props["req.params"]!;
        int id = int.TryParse(p["id"], out int i) ? i : -1;

        var result = await service.Read(id);
        await JsonUtils.SendResultResponse(req, res, props, result);
        await next();
    }

    public async Task Delete(HttpListenerRequest req, HttpListenerResponse res,
    Hashtable props, Func<Task> next)
    {
        var p = (NameValueCollection)props["req.params"]!;
        int id = int.TryParse(p["id"], out int i) ? i : -1;

        var result = await service.Delete(id);
        await JsonUtils.SendResultResponse(req, res, props, result);
        await next();
    }
}