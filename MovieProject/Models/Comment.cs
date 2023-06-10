using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieProject.Models
{
    public class Comment
    {
        public int ID { get; set; }

        public int MovieID { get; set; }
        public Movie Movie { get; set; }

        public int UserID { get; set; }
        public User User { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        [NotMapped]
        public DateTime Date { get; set; }

        [Range(0, 10)]
        public int Rating { get; set; }

        [StringLength(500)]
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }

    }
}
