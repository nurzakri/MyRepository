namespace MovieProject.Models
{
    public class EmailData
    {
        // Receiver
        public string To { get; set; }


        // Sender
        public string? From { get; set; }

        public string? DisplayName { get; set; }

        public string? ReplyTo { get; set; }

        public string? ReplyToName { get; set; }

        // Content
        public string Subject { get; set; }

        public string? Body { get; set; }

    }
}