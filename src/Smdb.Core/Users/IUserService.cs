namespace Smdb.Core.Users;

public interface IUserService
{
    IEnumerable<UserModel> List(int page, int size);
    UserModel? Get(int id);
    UserModel Create(UserModel user);
    UserModel? Update(int id, UserModel user);
    bool Delete(int id);
}
