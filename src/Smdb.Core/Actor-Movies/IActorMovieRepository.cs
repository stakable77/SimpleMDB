namespace Smdb.Core.ActorsMovies;

public interface IActorMovieRepository
{
    List<ActorMovieModel> GetAll();
    ActorMovieModel? GetById(int id);
    ActorMovieModel Add(ActorMovieModel model);
    bool Delete(int id);
}
