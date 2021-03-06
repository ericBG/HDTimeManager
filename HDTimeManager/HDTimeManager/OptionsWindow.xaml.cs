﻿using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using HDTimeManager.Annotations;
using MahApps.Metro.Controls.Dialogs;

namespace HDTimeManager
{
    /// <summary>
    ///     Interaction logic for OptionsWindow.xaml
    /// </summary>
    public partial class OptionsWindow : INotifyPropertyChanged
    {
        private IList _selected;

        internal OptionsWindow(ConfigInfo c)
        {
            Config = c;
            InitializeComponent();
            PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName != "Selected") return;
                SelectedInfo = Selected.Cast<TimeRangeInfo>().ToList();
            };
            Selected = new List<TimeRangeInfo>();
        }

        private List<TimeRangeInfo> SelectedInfo { get; set; }
        public ConfigInfo Config { get; }

        public IList Selected
        {
            get { return _selected; }
            set
            {
                if (Equals(value, _selected)) return;
                _selected = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void Add(object sender, RoutedEventArgs e)
        {
            var tsiw = new TimeRangeInfoWindow();
            tsiw.ShowDialog();
            if (tsiw.DiagResult == MessageDialogResult.Negative) return;
            Config.Ranges.Add(tsiw.Result);
            Config.Save();
        }

        private async void Remove(object sender, RoutedEventArgs e)
        {
            if (SelectedInfo.Count == 0)
            {
                 await this.ShowMessageAsync("Warning!", "You must select a time constraint in order to delete one.",
                    settings: new MetroDialogSettings {AffirmativeButtonText = "OK"});
                return;
            }
            bool plural = SelectedInfo.Count > 1;
            var result = await this.ShowMessageAsync("Delete Item",
                $"Are you sure you would like to delete th{(plural ? "ese" : "is")} item{(plural ? "s" : "")}?", MessageDialogStyle.AffirmativeAndNegative);
            if (result == MessageDialogResult.Negative) return;
            foreach (var range in SelectedInfo)
                Config.Ranges.Remove(range);
            Config.Save();
        }

        private async void Edit(object sender, RoutedEventArgs e)
        {
            if (SelectedInfo.Count == 0)
            {
                await this.ShowMessageAsync("Warning!", "You must select a time constraint in order to edit one.",
                   settings: new MetroDialogSettings { AffirmativeButtonText = "OK" });
                return;
            }
            if (SelectedInfo.Count > 1)
            {
                await this.ShowMessageAsync("Warning!", "Editing many time constraints is not supported. Please select just one.", 
                    settings: new MetroDialogSettings { AffirmativeButtonText= "OK" });
                return;
            }
            var window = new TimeRangeInfoWindow(SelectedInfo[0].Clone());
            window.ShowDialog();
            if (window.DiagResult == MessageDialogResult.Negative) return;
            Config.Ranges[Config.Ranges.IndexOf(SelectedInfo[0])] = window.Result;
            Config.Save();
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}