namespace Smdb.Core.Users;

public interface IUserService
{
    Task<PagedResult<User>> ReadUsers(int page, int size);
    Task<User> CreateUser(User user);
    Task<User?> ReadUser(int id);
    Task<User?> UpdateUser(int id, User user);
    Task<bool> DeleteUser(int id);
}