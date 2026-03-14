namespace Smdb.Core.Actors;

public interface IActorRepository
{
    List<ActorModel> List(int page, int size);
    ActorModel? Get(int id);
    ActorModel Create(ActorModel actor);
    ActorModel? Update(int id, ActorModel actor);
    bool Delete(int id);
}
