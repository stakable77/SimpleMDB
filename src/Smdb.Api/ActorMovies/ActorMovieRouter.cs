namespace Smdb.Api.ActorsMovies;

using Shared.Http;

public class ActorsMoviesRouter : HttpRouter
{
    public ActorsMoviesRouter(ActorsMoviesController ctrl)
    {
        UseParametrizedRouteMatching();

        MapGet("/", ctrl.ReadAll);
        MapPost("/", HttpUtils.ReadRequestBodyAsText, ctrl.Create);
        MapGet("/:id", ctrl.Read);
        MapDelete("/:id", ctrl.Delete);
    }
}