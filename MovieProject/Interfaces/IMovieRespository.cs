using Microsoft.AspNetCore.Mvc;
using MovieProject.Models;

namespace MovieProject.Interfaces
{
    public interface IMovieRespository
    {
        List<Movie> GetMovies(PagingParameters pagingParameters);
        Task<Movie> GetMoviesById(int id);
        Task<ActionResult<Movie>> PostMovie(MovieComment movieComment);
    }
}
