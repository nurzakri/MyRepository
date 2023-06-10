using MovieProject.Models;

namespace MovieProject.Interfaces
{
    public interface IUserRepository
    {
        User GetUser(User user);
        void CreateUser(User user);
        User GetUserByName(string userName);
    }
}
