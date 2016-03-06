using System;
using System.Windows.Controls;
using Hearthstone_Deck_Tracker.Plugins;

namespace HDTimeManager
{
    public class HearthstoneTimeManagerPlugin : IPlugin
    {
        public void OnLoad()
        {
            throw new NotImplementedException();
        }

        public void OnUnload()
        {
            throw new NotImplementedException();
        }

        public void OnButtonPress()
        {
            throw new NotImplementedException();
        }

        public void OnUpdate()
        {
            throw new NotImplementedException();
        }

        public string Name => "Hearthstone Time Manager";
        public string Description => "A small plugin to manage your daily Hearthstone time.";
        public string ButtonText => "Options";
        public string Author => "ericBG";
        public Version Version => new Version(1, 0, 0, 0);
        public MenuItem MenuItem { get; }
    }
}
