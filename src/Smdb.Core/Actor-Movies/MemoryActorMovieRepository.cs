using Smdb.Core.Db;

namespace Smdb.Core.ActorsMovies;

public class MemoryActorMovieRepository : IActorMovieRepository
{
    private readonly MemoryDatabase _db;

    public MemoryActorMovieRepository(MemoryDatabase db)
    {
        _db = db;
    }

    public List<ActorMovieModel> GetAll() => _db.ActorMovies;

    public ActorMovieModel Add(ActorMovieModel model)
    {
        model.Id = _db.ActorMovies.Count == 0 ? 1 : _db.ActorMovies.Max(x => x.Id) + 1;
        _db.ActorMovies.Add(model);
        return model;
    }

    public ActorMovieModel? GetById(int id)
        => _db.ActorMovies.FirstOrDefault(x => x.Id == id);

    public bool Delete(int id)
    {
        var item = GetById(id);
        if (item == null) return false;
        _db.ActorMovies.Remove(item);
        return true;
    }
}
