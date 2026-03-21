namespace Smdb.Core.Actors;

using System.Net;
using Shared.Http;

public class DefaultActorService : IActorService
{
    private readonly IActorRepository _repo;

    public DefaultActorService(IActorRepository repo)
    {
        _repo = repo;
    }

    public Result<PagedResult<ActorModel>> List(int page, int size)
    {
        var actors = _repo.List(page, size);
        var total = _repo.Count();
        return new Result<PagedResult<ActorModel>>(new PagedResult<ActorModel>(total, actors));
    }

    public Result<ActorModel> Get(int id)
    {
        var actor = _repo.Get(id);
        if (actor is null)
            return new Result<ActorModel>(
                new Exception($"Actor with id {id} not found."),
                (int)HttpStatusCode.NotFound);
        return new Result<ActorModel>(actor);
    }

    public Result<ActorModel> Create(ActorModel actor)
    {
        var created = _repo.Create(actor);
        return new Result<ActorModel>(created, (int)HttpStatusCode.Created);
    }

    public Result<ActorModel> Update(int id, ActorModel actor)
    {
        var updated = _repo.Update(id, actor);
        if (updated is null)
            return new Result<ActorModel>(
                new Exception($"Actor with id {id} not found."),
                (int)HttpStatusCode.NotFound);
        return new Result<ActorModel>(updated);
    }

    public Result<bool> Delete(int id)
    {
        var deleted = _repo.Delete(id);
        if (!deleted)
            return new Result<bool>(
                new Exception($"Actor with id {id} not found."),
                (int)HttpStatusCode.NotFound);
        return new Result<bool>(true);
    }
}
