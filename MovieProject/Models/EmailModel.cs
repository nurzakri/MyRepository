using System.ComponentModel.DataAnnotations;

namespace MovieProject.Models
{
    public class EmailModel
    {
        [Required(ErrorMessage = "Field can't be empty")]
        public int MovieId { get; set; }

        [Required(ErrorMessage = "Field can't be empty")]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not valid")]
        public string From { get; set; }

        [Required(ErrorMessage = "Field can't be empty")]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not valid")]
        public string To { get; set; }


    }
}