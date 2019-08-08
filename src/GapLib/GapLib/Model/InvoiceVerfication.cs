namespace GapLib.Model
{
    // TODO: need refactoring & clean up
    public class InvoiceVerfication : MessageBase
    {
        public string RefId { get; set; }
        //public int amount { get; set; }
        //public string status { get; set; }
    }

    public class InvoiceVerficationResult
    {
        public int Amount { get; set; }
        public InvoiceStatus Status { get; set; }
    }

    public enum InvoiceStatus
    {
        Verified,
        Error
    }
}
