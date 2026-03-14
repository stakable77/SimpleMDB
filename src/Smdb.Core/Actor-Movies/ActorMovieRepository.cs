namespace Smdb.Core.ActorsMovies;

public class ActorMovieRepository
{
    protected readonly List<ActorMovieModel> items = new();

    public virtual List<ActorMovieModel> GetAll() => items;

    public virtual ActorMovieModel Add(ActorMovieModel model)
    {
        items.Add(model);
        return model;
    }

    public virtual ActorMovieModel? GetById(int id)
        => items.FirstOrDefault(x => x.Id == id);

    public virtual bool Delete(int id)
    {
        var item = GetById(id);
        if (item == null) return false;
        items.Remove(item);
        return true;
    }
}