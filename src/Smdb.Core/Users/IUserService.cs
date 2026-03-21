namespace Smdb.Core.Users;

using Shared.Http;

public interface IUserService
{
    Result<PagedResult<UserModel>> List(int page, int size);
    Result<UserModel> Get(int id);
    Result<UserModel> Create(UserModel user);
    Result<UserModel> Update(int id, UserModel user);
    Result<bool> Delete(int id);
}
