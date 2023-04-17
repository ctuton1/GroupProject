namespace Api
{
    public class Events
    {
        public int eventID { get; set; }
        public string eventName { get; set; }
        public DateTime eventDate { get; set; }
        public string eventDescription { get; set; }
        public int eventOwner { get; set; } //The user ID of the owner of the event

    }
}
