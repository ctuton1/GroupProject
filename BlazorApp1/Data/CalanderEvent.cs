namespace BlazorApp1.Data
{
    public class CalanderEvent
    {
        public int EventId { get; set; }
        public string Note { get; set; }
        public DateTime EventDate { get; set; } = new DateTime(1900, 1, 1);
        public DateTime FromDate { get; set; } = new DateTime(1900, 1, 1);
        public DateTime ToDate { get; set; } = new DateTime(1900, 1, 1);      
        public string DateValue { get; set; }
        public string DayName { get; set; }
        public string Message { get; set; }

    }
}
