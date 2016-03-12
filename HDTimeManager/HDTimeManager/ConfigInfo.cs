using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Hearthstone_Deck_Tracker;

namespace HDTimeManager
{
    internal class ConfigInfo
    {
        private static readonly string ConfigDir = Path.Combine(Config.Instance.DataDir, "HDTimeManager");
        public static ConfigInfo Load()
        {
            XmlSerializer x = new XmlSerializer(typeof (ConfigInfo), new[] {typeof (TimeRangeInfo)});
            using (FileStream fs = File.OpenRead(Path.Combine(ConfigDir, "times.xml")))
                return (ConfigInfo)x.Deserialize(fs);
        }

        public void Save()
        {
            XmlSerializer x = new XmlSerializer(typeof (ConfigInfo), new[] {typeof (TimeRangeInfo)});
            using (FileStream fs = File.OpenWrite(Path.Combine(ConfigDir, "times.xml")))
                x.Serialize(fs, this);
        }

        private ConfigInfo()
        {
        }

        public TimeSpan TimeSpentToday;
        public DateTime DayLastUpdated;
        public List<TimeRangeInfo> Ranges { get; set; }
    }
}