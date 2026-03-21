namespace Smdb.Core.ActorsMovies;

using System.Net;
using Shared.Http;

public class DefaultActorMovieService
{
    private readonly IActorMovieRepository _repo;

    public DefaultActorMovieService(IActorMovieRepository repo)
    {
        _repo = repo;
    }

    public Result<List<ActorMovieModel>> GetAll()
        => new Result<List<ActorMovieModel>>(_repo.GetAll());

    public Result<ActorMovieModel> Create(ActorMovieModel model)
        => new Result<ActorMovieModel>(_repo.Add(model), (int)HttpStatusCode.Created);

    public Result<ActorMovieModel> Get(int id)
    {
        var item = _repo.GetById(id);
        if (item is null)
            return new Result<ActorMovieModel>(
                new Exception($"ActorMovie with id {id} not found."),
                (int)HttpStatusCode.NotFound);
        return new Result<ActorMovieModel>(item);
    }

    public Result<bool> Delete(int id)
    {
        var deleted = _repo.Delete(id);
        if (!deleted)
            return new Result<bool>(
                new Exception($"ActorMovie with id {id} not found."),
                (int)HttpStatusCode.NotFound);
        return new Result<bool>(true);
    }
}
