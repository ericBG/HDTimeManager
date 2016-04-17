using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using HDTimeManager.Annotations;

namespace HDTimeManager
{
    /// <summary>
    ///     Interaction logic for TimeSpanSelector.xaml
    /// </summary>
    public partial class TimeSpanSelector : INotifyPropertyChanged
    {
        public static readonly DependencyProperty SpanProperty = DependencyProperty.Register(
            "Span", typeof (System.TimeSpan), typeof (TimeSpanSelector),
            new FrameworkPropertyMetadata(default(System.TimeSpan),
                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, TryingWayyyTooHard));

        public static readonly DependencyProperty MinutesIntervalProperty = DependencyProperty.Register(
            "MinutesInterval", typeof (int), typeof (TimeSpanSelector), new PropertyMetadata(5));

        public static readonly DependencyProperty HoursIntervalProperty = DependencyProperty.Register(
            "HoursInterval", typeof (int), typeof (TimeSpanSelector), new PropertyMetadata(1));

        public TimeSpanSelector()
        {
            InitializeComponent();
        }

        public System.TimeSpan Span
        {
            get { return (System.TimeSpan) GetValue(SpanProperty); }
            set { SetValue(SpanProperty, value); }
        }

        public int Hours
        {
            get { return Span.Hours; }
            set
            {
                if (Equals(Span.Hours, value)) return;
                Span = new System.TimeSpan(value, Minutes, 0);
                OnPropertyChanged();
            }
        }

        public int Minutes
        {
            get { return Span.Minutes; }
            set
            {
                if (Equals(Span.Minutes, value)) return;
                Span = new System.TimeSpan(Hours, value, 0);
                OnPropertyChanged();
                if (value == 60) OnPropertyChanged(nameof(Hours));
            }
        }

        [DefaultValue(5)]
        [Bindable(true)]
        public int MinutesInterval
        {
            get { return (int) GetValue(MinutesIntervalProperty); }
            set { SetValue(MinutesIntervalProperty, value); }
        }

        [DefaultValue(1)]
        [Bindable(true)]
        public int HoursInterval
        {
            get { return (int) GetValue(HoursIntervalProperty); }
            set { SetValue(HoursIntervalProperty, value); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private static void TryingWayyyTooHard(DependencyObject dependencyObject,
            DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            ((TimeSpanSelector) dependencyObject).OnPropertyChanged(nameof(Span));
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}