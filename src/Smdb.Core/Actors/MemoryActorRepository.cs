using Smdb.Core.Db;

namespace Smdb.Core.Actors;

public class MemoryActorRepository : IActorRepository
{
    private readonly MemoryDatabase _db;

    public MemoryActorRepository(MemoryDatabase db)
    {
        _db = db;
    }

    public List<ActorModel> List(int page, int size)
        => _db.Actors.Skip((page - 1) * size).Take(size).ToList();

    public int Count() => _db.Actors.Count;

    public ActorModel? Get(int id)
        => _db.Actors.FirstOrDefault(a => a.Id == id);

    public ActorModel Create(ActorModel actor)
    {
        actor.Id = _db.Actors.Count == 0 ? 1 : _db.Actors.Max(a => a.Id) + 1;
        _db.Actors.Add(actor);
        return actor;
    }

    public ActorModel? Update(int id, ActorModel actor)
    {
        var existing = Get(id);
        if (existing is null) return null;

        existing.Name = actor.Name;
        existing.Age = actor.Age;
        return existing;
    }

    public bool Delete(int id)
    {
        var actor = Get(id);
        if (actor is null) return false;

        _db.Actors.Remove(actor);
        return true;
    }
}
