using Amazon.Pay.API.Types;
using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace Amazon.Pay.API.WebStore.Types
{
    public class RecurringMetadata
    {
        public RecurringMetadata()
        {
            Frequency = new Frequency();
            Amount = new Price();
        }

        [OnSerializing]
        internal void OnSerializing(StreamingContext content)
        {
            if (Frequency != null && Frequency.Unit == null && Frequency.Value == null)
            {
                Frequency = null;
            }

            // skip 'chargeAmount' if there wasn't provided anything
            if (Amount != null && Amount.Amount == 0 && Amount.CurrencyCode == null)
            {
                Amount = null;
            }
        }

        [OnSerialized]
        internal void OnSerialized(StreamingContext content)
        {
            if (Frequency == null)
            {
                Frequency = new Frequency();
            }

            if (Amount == null)
            {
                Amount = new Price();
            }
        }

        /// <summary>
        /// Recurring metadata charge frequency
        /// </summary>
        [JsonProperty(PropertyName = "frequency")]
        public Frequency Frequency { get; internal set; }

        /// <summary>
        /// Optional metadata transaction amount
        /// </summary>
        [JsonProperty(PropertyName = "amount")]
        public Price Amount { get; internal set; }
    }
}
