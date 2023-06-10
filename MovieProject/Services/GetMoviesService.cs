using Azure.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MovieProject.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Org.BouncyCastle.Asn1.Crmf;
using System.Text.Json.Nodes;
using static System.Net.Mime.MediaTypeNames;

namespace MovieProject.Services
{
    public class GetMoviesService : IHostedService
    {
        private readonly IServiceScopeFactory scopeFactory;
        private const string ApiKey = "eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiIyOWYyNmNlYjZiZDQzODU0MjFkMDJhZTE3YjYyMWZlYyIsInN1YiI6IjY0ODJmYjA5YmYzMWYyNTA1ZjNkYzBjYiIsInNjb3BlcyI6WyJhcGlfcmVhZCJdLCJ2ZXJzaW9uIjoxfQ.y1L_KUh00TpB_hhNxA8TNuR4bajpF-KuwfsRSFDLf0A";
        public GetMoviesService(IServiceScopeFactory scopeFactory)
        {
            this.scopeFactory = scopeFactory;
        }
        Timer _timer;
        public Task StartAsync(CancellationToken cancellationToken)
        {
            TimeSpan interval = TimeSpan.FromHours(1);
            //calculate time to run the first time & delay to set the timer
            //DateTime.Today gives time of midnight 00.00
            var nextRunTime = DateTime.Today.AddHours(1);
            var curTime = DateTime.Now;
            var firstInterval = nextRunTime.Subtract(curTime);
            Action action = () =>
            {
                var t1 = Task.Delay(firstInterval);
                t1.Wait();
                //remove inactive accounts at expected time
                GetMoviesData(null);
                //now schedule it to be called every 1 hour for future
                // timer repeates call to RemoveScheduledAccounts every 1 hour.
                _timer = new Timer(
                    GetMoviesData,
                    null,
                    (int)TimeSpan.Zero.TotalMinutes,
                    (int)interval.TotalMinutes
                );
            };

            // no need to await this call here because this task is scheduled to run much much later.
            Task.Run(action);
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        private void GetMoviesData(object state)
        {
            var url = "https://api.themoviedb.org/3/trending/movie/day?language=en-US";
            List<Movie> movies = null;
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + ApiKey);
            var task = client.GetAsync(url)
                  .ContinueWith((taskwithresponse) =>
                  {
                      var response = taskwithresponse.Result;
                      var jsonString = response.Content.ReadAsStringAsync();
                      jsonString.Wait();
                      var movieList = JObject.Parse(jsonString.Result).SelectToken("results").ToString();
                      movies = JsonConvert.DeserializeObject<List<Movie>>(movieList);
                  });
            task.Wait();
            using (var scope = scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<MovieContext>();
                dbContext.Movies.AddRange(movies);
                dbContext.SaveChanges();
            }
        }
    }
}


