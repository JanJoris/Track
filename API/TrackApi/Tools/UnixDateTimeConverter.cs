﻿using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace TrackApi.Tools
{
    public class UnixDateTimeConverter : DateTimeConverterBase
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(double.Parse(reader.Value.ToString())).ToLocalTime();
            var time = dtDateTime.TimeOfDay.ToString();
            return dtDateTime.TimeOfDay.ToString().Remove(dtDateTime.TimeOfDay.ToString().IndexOf(":00", StringComparison.Ordinal),3);
        }
    }
}
