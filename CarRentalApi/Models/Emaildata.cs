namespace CarRentalApi.Models
{
    public class Emaildata
    {
        public string? tomail{ get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool? isHtml { get; set; }
    }
}
