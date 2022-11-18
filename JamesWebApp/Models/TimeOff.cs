using System.Configuration;

namespace JamesWebApp.Models
{
    public class TimeOff
    {
        public int Id { get; set; }
        public string ?UserName { get; set; }
        public double PaidTimeOff { get; set; }
        public DateTime DateOne { get; set; }
        public DateTime ?DateTwo { get; set; }
        public int Vacation { get; set; }
    }
}
