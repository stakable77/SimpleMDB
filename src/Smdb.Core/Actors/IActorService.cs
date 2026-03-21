namespace Smdb.Core.Actors;

using Shared.Http;

public interface IActorService
{
    Result<PagedResult<ActorModel>> List(int page, int size);
    Result<ActorModel> Get(int id);
    Result<ActorModel> Create(ActorModel actor);
    Result<ActorModel> Update(int id, ActorModel actor);
    Result<bool> Delete(int id);
}
