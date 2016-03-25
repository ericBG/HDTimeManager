using System.Windows;

namespace HDTimeManager
{
    /// <summary>
    /// Interaction logic for OptionsWindow.xaml
    /// </summary>
    public partial class OptionsWindow
    {
        public ConfigInfo Config { get; }

        internal OptionsWindow(ConfigInfo c)
        {
            Config = c;
            InitializeComponent();
        }

        private void Add(object sender, RoutedEventArgs e)
        {
            TimeRangeInfoWindow tsiw = new TimeRangeInfoWindow();
            tsiw.Show();
            if (tsiw.Result != null) Config.Ranges.Add(tsiw.Result);
        }
    }
}
