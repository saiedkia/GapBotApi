namespace GapLib.Model
{
    // TODO: need refactoring & clean up
    public class InvoiceVerfication
    {
        public string chat_id { get; set; }
        public string ref_id { get; set; }
        //public int amount { get; set; }
        //public string status { get; set; }
    }

    public class InvoiceVerficationResult
    {
        public int amount { get; set; }
        public InvoiceStatus status { get; set; }
    }

    public enum InvoiceStatus
    {
        Verified,
        Error
    }
}
