namespace GapLib.Model
{
    public class Invoice
    {
        public string chat_id { get; set; }
        public int amount { get; set; }
        public Currency currency { get; set; }
        public string description { get; set; }
    }
}
