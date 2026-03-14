using Smdb.Core.Db;

namespace Smdb.Core.ActorsMovies;

public class MemoryActorMovieRepository : ActorMovieRepository
{
    private readonly MemoryDatabase db;

    public MemoryActorMovieRepository(MemoryDatabase db)
    {
        this.db = db;
    }

    public override List<ActorMovieModel> GetAll()
        => db.ActorMovies;

    public override ActorMovieModel Add(ActorMovieModel model)
    {
        model.Id = db.ActorMovies.Count + 1;
        db.ActorMovies.Add(model);
        return model;
    }

    public override ActorMovieModel? GetById(int id)
        => db.ActorMovies.FirstOrDefault(x => x.Id == id);

    public override bool Delete(int id)
    {
        var item = GetById(id);
        if (item == null) return false;
        db.ActorMovies.Remove(item);
        return true;
    }
}