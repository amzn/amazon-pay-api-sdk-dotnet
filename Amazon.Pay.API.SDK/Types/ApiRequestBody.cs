using Newtonsoft.Json;
using System.Text.RegularExpressions;

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
                NullValueHandling = NullValueHandling.Ignore,
                TypeNameHandling = TypeNameHandling.None
            };

            var jsonString = JsonConvert.SerializeObject(this, serializerSettings);

            // remove empty objects from the JSON string
            var regex = new Regex(",?\"[a-z]([a-z]|[A-Z])+\":{}");
            jsonString = regex.Replace(jsonString, string.Empty);

            // remove potential clutter
            var regex2 = new Regex("{,\"");
            jsonString = regex2.Replace(jsonString, "{\"");

            return jsonString;
        }
    }
}
