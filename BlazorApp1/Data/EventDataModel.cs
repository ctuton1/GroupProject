namespace BlazorApp1.Data
{
    public class EventDataModel
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime Date { get; set; }

        public TimeSpan? Time { get; set; }

        public string Description { get; set; }

        public int EventOwnerId { get; set; }

    }
}
