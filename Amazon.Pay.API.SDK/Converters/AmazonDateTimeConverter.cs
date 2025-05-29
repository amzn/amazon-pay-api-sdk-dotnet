using System;
using Newtonsoft.Json;
using System.Globalization;

namespace Amazon.Pay.API.Converters
{
    internal class AmazonDateTimeConverter : JsonConverter
    {
        // Amazon Pay required format for PSP Dispute APIs
        private const string AmazonDateTimeFormat = "yyyyMMddTHHmmssZ";

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value is DateTime dateTime)
            {
                // Convert to UTC, remove milliseconds, and format as required
                string formattedDate = dateTime.ToUniversalTime().ToString(AmazonDateTimeFormat, CultureInfo.InvariantCulture);
                writer.WriteValue(formattedDate);
            }
            else
            {
                throw new JsonSerializationException("Expected DateTime object value.");
            }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            string dateStr = reader.Value?.ToString();

            if (!string.IsNullOrEmpty(dateStr) &&
                DateTime.TryParseExact(dateStr, AmazonDateTimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out DateTime result))
            {
                return result;
            }

            throw new JsonSerializationException($"Invalid date format: {dateStr}");
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(DateTime) || objectType == typeof(DateTime?);
        }
    }
}
