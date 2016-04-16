using System;
using System.Linq;
using System.Reflection;
using System.Windows.Controls;
using System.Windows.Interop;
using Hearthstone_Deck_Tracker;
using Hearthstone_Deck_Tracker.API;
using Hearthstone_Deck_Tracker.Plugins;
using MahApps.Metro.Controls.Dialogs;
using Core = Hearthstone_Deck_Tracker.API.Core;

namespace HDTimeManager
{
    public class HearthstoneTimeManagerPlugin : IPlugin
    {
        private static readonly MethodInfo FlashWindowInfo = typeof (User32).GetMethod("FlashWindow",
            BindingFlags.NonPublic | BindingFlags.Static);

        internal ConfigInfo Config;
        private static Days Today => (Days) ((int) Math.Pow(2, (int) DateTime.Today.DayOfWeek));

        public void OnLoad()
        {
            Config = ConfigInfo.Load();
            GameEvents.OnGameEnd.Add(CheckTime);
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
            foreach (var info in Config.Ranges.Where(i => i.Active.HasFlag(Today) && Config.TimeSpentToday > (i.Time - i.Range) && i.LastTriggered != DateTime.Today))
            {
                FlashWindowInfo.Invoke(null, new object[] {new WindowInteropHelper(Core.MainWindow).Handle, false});
                Core.MainWindow.ShowMessageAsync("WARNING", info.Message);
                info.LastTriggered = DateTime.Today;
            }
        }
    }
}