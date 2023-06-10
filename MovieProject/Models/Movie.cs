using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MovieProject.Models
{
    public class Movie
    {
        [JsonProperty(PropertyName = "MovieId")]
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime ReleaseDate { get; set; }
        public decimal Rating
        {
            get
            {
                if (Comments != null && Comments.Count != 0)
                    return (decimal)Comments.Sum(c => c.Rating) / Comments.Count;
                else
                    return -1;
            }
        }
        public ICollection<Comment> Comments { get; set; }
    }

}

