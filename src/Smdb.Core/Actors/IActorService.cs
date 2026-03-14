Task<PagedResult<Actor>> ReadActors(int page, int size);
Task<Actor> CreateActor(Actor actor);
Task<Actor?> ReadActor(int id);
Task<Actor?> UpdateActor(int id, Actor actor);
Task<bool> DeleteActor(int id);