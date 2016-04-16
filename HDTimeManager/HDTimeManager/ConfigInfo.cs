using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;
using HDTimeManager.Annotations;
using Hearthstone_Deck_Tracker;

namespace HDTimeManager
{
    public class ConfigInfo : INotifyPropertyChanged
    {
        private static readonly string ConfigDir = Path.Combine(Config.Instance.DataDir, "HDTimeManager");
        private DateTime _dayLastUpdated;
        private ObservableCollection<TimeRangeInfo> _ranges;
        private TimeSpan _timeSpentToday;
        private Object _lock = new Object();

        private ConfigInfo()
        {
        }

        public TimeSpan TimeSpentToday
        {
            get { return _timeSpentToday; }
            set
            {
                if (value.Equals(_timeSpentToday)) return;
                _timeSpentToday = value;
                OnPropertyChanged();
            }
        }

        public DateTime DayLastUpdated
        {
            get { return _dayLastUpdated; }
            set
            {
                if (value.Equals(_dayLastUpdated)) return;
                _dayLastUpdated = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<TimeRangeInfo> Ranges
        {
            get { return _ranges; }
            set
            {
                if (Equals(value, _ranges)) return;
                _ranges = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public static ConfigInfo Load()
        {
            var x = new XmlSerializer(typeof (ConfigInfo), new[] {typeof (TimeRangeInfo)});
            if (!Directory.Exists(ConfigDir)) Directory.CreateDirectory(ConfigDir);
            if (!File.Exists(Path.Combine(ConfigDir, "times.xml"))) return new ConfigInfo();
            using (var fs = File.OpenRead(Path.Combine(ConfigDir, "times.xml")))
                return (ConfigInfo) x.Deserialize(fs);
        }

        public void Save()
        {
            //naively add a lock becuase I've not got a clue why my XML file seems to be getting messed up
            lock(_lock)
            {
                var x = new XmlSerializer(typeof (ConfigInfo), new[] {typeof (TimeRangeInfo)});
                using (var fs = File.OpenWrite(Path.Combine(ConfigDir, "times.xml")))
                    x.Serialize(fs, this);
            }
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}