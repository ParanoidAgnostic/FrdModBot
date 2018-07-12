using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace RedditWrapper.Helpers
{
    public class UnixTimestampConverter : DateTimeConverterBase
    {
        private static readonly DateTime _epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteRawValue(UnixTimestampFromDateTime((DateTime)value).ToString());
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            object value = reader.Value;
            double? valueDouble = value as double?;
            if(valueDouble.HasValue) return TimeFromUnixTimestamp(valueDouble.Value);
            long? valueLong = value as long?;
            if (valueLong.HasValue) return TimeFromUnixTimestamp(valueLong.Value);
            return null;
        }

        private static DateTime TimeFromUnixTimestamp(double unixTimestamp)
        {
            long unixTimeStampInTicks = (long)(unixTimestamp * TimeSpan.TicksPerSecond);
            return new DateTime(_epoch.Ticks + unixTimeStampInTicks);
        }

        private static DateTime TimeFromUnixTimestamp(long unixTimestamp)
        {
            long unixTimeStampInTicks = unixTimestamp * TimeSpan.TicksPerSecond;
            return new DateTime(_epoch.Ticks + unixTimeStampInTicks);
        }

        public static double UnixTimestampFromDateTime(DateTime date)
        {
            double unixTimestamp = date.Ticks - _epoch.Ticks;
            unixTimestamp /= TimeSpan.TicksPerSecond;
            return unixTimestamp;
        }
    }
}
