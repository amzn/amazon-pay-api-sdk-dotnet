using Newtonsoft.Json;
using System;

namespace Amazon.Pay.API.Converters
{
    /// <summary>
    /// JSON Converter class for decimals.
    /// </summary>
    /// <remarks>
    /// Removes fractional part from decimals if not required. Important for Japanse Yen transactions as API may throw an exception otherwise.
    /// </remarks>
    internal class DecimalJsonConverter : JsonConverter
    {
        public DecimalJsonConverter()
        {
        }

        public override bool CanRead
        {
            get
            {
                return false;
            }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(decimal));
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            decimal decimalValue = (decimal)value;
            if (decimalValue == Math.Truncate(decimalValue))
            {
                writer.WriteRawValue(JsonConvert.ToString(Convert.ToInt64(value)));
            }
            else
            {
                writer.WriteRawValue(JsonConvert.ToString(value));
            }
        }
    }
}