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
        private bool? _shouldSave = null;
        private readonly TimeRangeInfo _original;

        public TimeRangeInfoWindow(TimeRangeInfo result = null)
        {
            InitializeComponent();
            Result = result ?? new TimeRangeInfo();
            _original = result;
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
            if (_shouldSave.HasValue)
            {
                if (!_shouldSave.Value) Result = null;
                return;
            }
            if (_original == Result && _original != null) return;
            e.Cancel = true;
            DiagResult = await this.ShowMessageAsync("Warning!", "Would you like to save your custom time constraint?",
                MessageDialogStyle.AffirmativeAndNegativeAndSingleAuxiliary,
                new MetroDialogSettings {FirstAuxiliaryButtonText = "Cancel", NegativeButtonText = "No"});
            switch (DiagResult)
            {
                case MessageDialogResult.Negative:
                    _shouldSave = false;
                    Close();
                    break;
                case MessageDialogResult.Affirmative:
                    _shouldSave = true;
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
            _shouldSave = false;
            Close();
        }

        private void OKClick(object sender, RoutedEventArgs e)
        {
            _shouldSave = true;
            Close();
        }
    }
}