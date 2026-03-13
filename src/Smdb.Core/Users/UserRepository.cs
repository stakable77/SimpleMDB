namespace Smdb.Core.Users;

public interface IUserRepository
{
    IEnumerable<UserModel> List(int page, int size);
    UserModel? GetById(int id);
    UserModel Create(UserModel user);
    UserModel? Update(int id, UserModel user);
    bool Delete(int id);
}
