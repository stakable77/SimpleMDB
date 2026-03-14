namespace Smdb.Core.Actors;

public class DefaultActorService
{
    private readonly IActorRepository _repo;

    public DefaultActorService(IActorRepository repo)
    {
        _repo = repo;
    }

    public object List(int page, int size) => _repo.List(page, size);
    public object Get(int id) => _repo.Get(id);
    public object Create(ActorModel actor) => _repo.Create(actor);
    public object Update(int id, ActorModel actor) => _repo.Update(id, actor);
    public object Delete(int id) => _repo.Delete(id);
}
