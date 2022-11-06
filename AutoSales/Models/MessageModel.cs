namespace AutoSales.Models
{
    public class MessageModel
    {
        public Guid IdMessage { get; set; }
        public string Text { get; set; } = null!;
        public string IdUser { get; set; }
        public Guid IdPost { get; set; }
    }
}
