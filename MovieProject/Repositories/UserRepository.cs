using Microsoft.EntityFrameworkCore;
using MovieProject.Interfaces;
using MovieProject.Models;

namespace MovieProject.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly MovieContext _dbContext;
        public UserRepository(MovieContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void CreateUser(User user)
        {
            _dbContext.Users.Add(new User { UserName = user.UserName, Password = user.Password }) ; 
            _dbContext.SaveChanges();
        }

        public User GetUser(User user)
        {
            return _dbContext.Users.Where(a=> a.UserName == user.UserName && a.Password == user.Password).FirstOrDefault();
        }

        public User GetUserByName(string userName)
        {
            return _dbContext.Users.Where(a => a.UserName == userName).FirstOrDefault();
        }
    }
}
