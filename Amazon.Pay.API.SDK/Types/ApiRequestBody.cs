using Newtonsoft.Json;

namespace Amazon.Pay.API.Types
{
    public abstract class ApiRequestBody
    {
        /// <summary>
        /// Converts this object into a JSON string that can be passed to the API.
        /// </summary>
        /// <remarks>
        /// The client class implicitly converts the object before calling the API,
        /// so this method usually doesn't need to be called manually.
        /// </remarks>
        /// <returns></returns>
        public string ToJson()
        {

            var serializerSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            };

            var jsonString = JsonConvert.SerializeObject(this, serializerSettings);
            return jsonString;
        }
    }
}
