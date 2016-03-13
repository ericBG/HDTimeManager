using System;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace HDTimeManager
{
    //because fucking fuck you too Microsoft
    //love you stackoverflow <3 http://stackoverflow.com/questions/637933/how-to-serialize-a-timespan-to-xml
    //implicit operators are awesome
    public struct TimeSpan : IXmlSerializable, IComparable<TimeSpan>
    {
        private System.TimeSpan _value;
        public static implicit operator TimeSpan(System.TimeSpan value) => new TimeSpan {_value = value};
        public static implicit operator System.TimeSpan(TimeSpan value) => value._value;
        public static TimeSpan operator +(TimeSpan a, TimeSpan b) => a._value + b._value;
        public static TimeSpan operator -(TimeSpan a, TimeSpan b) => a._value - b._value;
        public static bool operator >(TimeSpan a, TimeSpan b) => a._value > b._value;
        public static bool operator <(TimeSpan a, TimeSpan b) => a._value < b._value;
        public XmlSchema GetSchema() => null;

        public void ReadXml(XmlReader reader) => _value = XmlConvert.ToTimeSpan(reader.ReadElementContentAsString());

        public void WriteXml(XmlWriter writer) => writer.WriteValue(XmlConvert.ToString(_value));

        public int CompareTo(TimeSpan other) => _value.CompareTo(other._value);
    }
}