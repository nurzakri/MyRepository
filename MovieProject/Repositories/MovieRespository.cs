using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieProject.Interfaces;
using MovieProject.Models;
namespace MovieProject.Repositories
{
    public class MovieRespository : IMovieRespository
    {
        private readonly MovieContext _dbContext;
        public MovieRespository(MovieContext dbContext)
        {
            _dbContext = dbContext;
        }
        public List<Movie> GetMovies(PagingParameters pagingParameters)
        {
           return _dbContext.Movies
                  .Skip((pagingParameters.PageNumber.Value - 1) * pagingParameters.PageSize.Value)
                  .Take(pagingParameters.PageSize.Value)
                  .ToList();
        }

        public async Task<Movie> GetMoviesById(int id)
        {
            return await _dbContext.Movies
                                   .Include(x => x.Comments)
                                   .Where(a=>a.Id == id).FirstOrDefaultAsync();

        }
        public async Task<ActionResult<Movie>> PostMovie(MovieComment movieComment)
        {
            var movie = GetMoviesById(movieComment.MovieId);
            if (movie != null)
            {
                Comment comment = new Comment();
                comment.Rating = movieComment.Rating;
                comment.Content = movieComment.Content; 
                comment.UserID = movieComment.UserId;
                comment.MovieID = movieComment.MovieId;
                _dbContext.Comments.Add(comment);
                _dbContext.SaveChanges();
                return await movie;
            }
            return null;
        }
    }
}
