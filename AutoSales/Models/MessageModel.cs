namespace AutoSales.Models
{
    public class MessageModel
    {
        public Guid IdMessage { get; set; }
        public string Text { get; set; } = null!;
        public Guid IdUser { get; set; }
        public Guid IdPost { get; set; }
    }
}
