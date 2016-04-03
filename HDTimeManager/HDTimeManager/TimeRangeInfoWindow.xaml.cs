using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
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

        public TimeRangeInfoWindow(TimeRangeInfo result = null)
        {
            InitializeComponent();
            Result = result ?? new TimeRangeInfo();
        }

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
            e.Cancel = true;
            var result = await this.ShowMessageAsync("Warning!", "Would you like to save your custom time constraint?",
                MessageDialogStyle.AffirmativeAndNegativeAndSingleAuxiliary,
                new MetroDialogSettings {FirstAuxiliaryButtonText = "Cancel", NegativeButtonText = "No"});
            switch (result)
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