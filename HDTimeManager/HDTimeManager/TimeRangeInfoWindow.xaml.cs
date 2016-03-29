using System.ComponentModel;
using System.Runtime.CompilerServices;
using HDTimeManager.Annotations;

namespace HDTimeManager
{
    /// <summary>
    /// Interaction logic for TimeRangeInfoWindow.xaml
    /// </summary>
    public partial class TimeRangeInfoWindow : INotifyPropertyChanged
    {
        private TimeRangeInfo _result;

        public TimeRangeInfo Result
        {
            get { return _result; }
            set
            {
                if (Equals(value, _result)) return;
                _result = value;
                OnPropertyChanged();
            }
        }

        public TimeRangeInfoWindow(TimeRangeInfo result = null)
        {
            InitializeComponent();
            Result = result ?? new TimeRangeInfo();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
