namespace Smdb.Core.ActorsMovies;

public class DefaultActorMovieService
{
    private readonly MemoryActorMovieRepository repo;

    public DefaultActorMovieService(MemoryActorMovieRepository repo)
    {
        this.repo = repo;
    }

    public Task<List<ActorMovieModel>> ReadAll()
        => Task.FromResult(repo.GetAll());

    public Task<ActorMovieModel> Create(ActorMovieModel model)
        => Task.FromResult(repo.Add(model));

    public Task<ActorMovieModel?> Read(int id)
        => Task.FromResult(repo.GetById(id));

    public Task<bool> Delete(int id)
        => Task.FromResult(repo.Delete(id));
}