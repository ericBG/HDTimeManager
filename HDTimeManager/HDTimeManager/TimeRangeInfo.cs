namespace HDTimeManager
{
    public class TimeRangeInfo
    {
        public TimeSpan Time { get; set; }
        public TimeSpan Range { get; set; }
        public Days Active { get; set; }
        public string Message { get; set; }
        public string Name { get; set; }
    }
}