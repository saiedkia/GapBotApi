namespace GapLib.Model
{
    public class Invoice : MessageBase
    {
        public int Amount { get; set; }
        public Currency Currency { get; set; }
        public string Description { get; set; }
    }
}
