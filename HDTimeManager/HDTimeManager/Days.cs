using System;
using System.ComponentModel;

namespace HDTimeManager
{
    [Flags]
    public enum Days
    {
        None = 0x0,
        Monday = 0x1,
        Tuesday = 0x2,
        Wednesday = 0x4,
        Thursday = 0x8,
        Friday = 0x10,
        Saturday = 0x20,
        Sunday = 0x40,
        [Description("all days")]
        All = 0x7F
    }
}