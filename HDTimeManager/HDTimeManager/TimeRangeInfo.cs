using System;

namespace HDTimeManager
{
    public class TimeRangeInfo
    {
        public TimeSpan Time { get; set; }
        public TimeSpan Range { get; set; }
        public Days Active { get; set; }
        public string Message { get; set; }
        public string Name { get; set; }
        public DateTime LastTriggered { get; set; }

        public TimeRangeInfo Clone()
            =>
                new TimeRangeInfo
                {
                    Active = Active,
                    LastTriggered = LastTriggered,
                    Message = Message,
                    Name = Name,
                    Range = Range,
                    Time = Time
                };
    }
}