using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieProject.Interfaces;
using MovieProject.Models;
using MovieProject.Repositories;

namespace MovieProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MovieController : ControllerBase
    {
        private IMovieRespository _movieRepository;
        private IUserRepository _userRepository;
        private IEmailService _emailService;

        public MovieController(IMovieRespository movieRespository, IUserRepository userRepository, IEmailService emailService)
        {
            _movieRepository = movieRespository;
            _userRepository = userRepository;
            _emailService = emailService;
        }
        [HttpGet("GetMovies")]
        public ActionResult<List<Movie>> GetMovies([FromBody] PagingParameters pagingParameters)
        {
            return _movieRepository.GetMovies(pagingParameters);
        }
        // GET: api/Movies/5
        [HttpGet("GetMovie")]

        public async Task<ActionResult<Movie>> GetMovie(int id)
        {

            var movie = await _movieRepository.GetMoviesById(id);

            if (movie == null)
            {
                return NotFound();
            }

            return movie;
        }
        // POST: api/Movie/PostMovie
        [HttpPost("PostMovie")]
        public async Task<ActionResult<Movie>> PostMovie([FromBody] MovieComment movieComment)
        {
            movieComment.UserId = _userRepository.GetUserByName(User.Identity.GetUserId()).Id;
            var movie = await _movieRepository.PostMovie(movieComment);
            if (movie == null)
            {
                return NotFound();
            }

            return movie;
        }
        // POST: api/Movie/SendEmailMovie
        [HttpPost("SendEmailMovie")]
        public async Task<ActionResult> SendEmailMovie([FromBody] EmailModel sendMovie)
        {
            var movie = await _movieRepository.GetMoviesById(sendMovie.MovieId);
            if (movie == null)
            {
                return BadRequest("Something wrong happend!");
            }
            EmailData emailData = new EmailData()
            {
                From = sendMovie.From,
                To = sendMovie.To,
                Body = "Movie Advise",
                Subject = "Hey, there is a beautiful movie " + movie.Title + " I advise it"
            };
            bool mailSent = await _emailService.SendAsync(emailData);
            if (mailSent)
            {
            return Ok("Mail Sent");
            }
            return BadRequest("Check your NetworkCredential");
        }
    }
}
