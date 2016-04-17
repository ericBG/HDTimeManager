using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using HDTimeManager.Annotations;
using MahApps.Metro.Controls.Dialogs;

namespace HDTimeManager
{
    /// <summary>
    ///     Interaction logic for TimeRangeInfoWindow.xaml
    /// </summary>
    public partial class TimeRangeInfoWindow : INotifyPropertyChanged
    {
        private TimeRangeInfo _result;
        private bool _shouldExit = false;
        private readonly TimeRangeInfo _original;

        public TimeRangeInfoWindow(TimeRangeInfo result = null)
        {
            InitializeComponent();
            Result = result ?? new TimeRangeInfo();
            _original = result?.Clone();
        }

        public MessageDialogResult? DiagResult { get; set; }

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

        public event PropertyChangedEventHandler PropertyChanged;

        protected override async void OnClosing(CancelEventArgs e)
        {
            if (_shouldExit) return;
            if (_original == Result && _original != null) return;
            e.Cancel = true;
            DiagResult = await this.ShowMessageAsync("Warning!", "Would you like to save your custom time constraint?",
                MessageDialogStyle.AffirmativeAndNegativeAndSingleAuxiliary,
                new MetroDialogSettings {FirstAuxiliaryButtonText = "Cancel", NegativeButtonText = "No"});
            switch (DiagResult)
            {
                case MessageDialogResult.Negative:
                case MessageDialogResult.Affirmative:
                    _shouldExit = true;
                    Close();
                    break;
                case MessageDialogResult.FirstAuxiliary:
                    break;
            }
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void CancelClick(object sender, RoutedEventArgs e)
        {
            _shouldExit = false;
            Close();
        }

        private void OKClick(object sender, RoutedEventArgs e)
        {
            _shouldExit = true;
            Close();
        }
    }
}