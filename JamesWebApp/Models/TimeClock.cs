namespace JamesWebApp.Models
{
    public class TimeClock
    {
        public int Id { get; set; }
        public string ?UserName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public TimeSpan TimeWorked { get; set; }

    }
}
