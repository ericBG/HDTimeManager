using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using Hearthstone_Deck_Tracker.Plugins;
using Hearthstone_Deck_Tracker.API;
using MahApps.Metro.Controls.Dialogs;

namespace HDTimeManager
{
    public class HearthstoneTimeManagerPlugin : IPlugin
    {
        private static Days Today => (Days)((int) Math.Pow(2, (int)DateTime.Today.DayOfWeek));
        internal ConfigInfo Config;
        public void OnLoad()
        {
            Config = ConfigInfo.Load();
            GameEvents.OnGameEnd.Add(CheckTime);
        }

        private void CheckTime()
        {
            TimeSpan timeSpent = Core.Game.CurrentGameStats.EndTime - Core.Game.CurrentGameStats.StartTime;
            if (Config.DayLastUpdated != DateTime.Today)
            {
                Config.DayLastUpdated = DateTime.Today;
                Config.TimeSpentToday = timeSpent;
            }
            else Config.TimeSpentToday += timeSpent;
            Config.Save();
            foreach (TimeRangeInfo info in Config.Ranges.Where(i => i.Active.HasFlag(Today)))
            {
                if (Config.TimeSpentToday > (info.Time - info.Range))
                    Core.MainWindow.ShowMessageAsync("WARNING", info.Message);
            }
        }

        public void OnUnload()
        {
        }

        public void OnButtonPress() => new OptionsWindow(Config).Show();

        public void OnUpdate()
        {
        }

        public string Name => "Hearthstone Time Manager";
        public string Description => "A small plugin to manage your daily Hearthstone time.";
        public string ButtonText => "Options";
        public string Author => "ericBG";
        public Version Version => new Version(1, 0, 0, 0);
        public MenuItem MenuItem { get; }
    }
}
