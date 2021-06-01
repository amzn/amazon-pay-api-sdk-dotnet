using System;

namespace Amazon.Pay.API
{
    public class ApiUrlBuilder
    {
        private ApiConfiguration config;

        public ApiUrlBuilder(ApiConfiguration config)
        {
            this.config = config;
        }

        public Uri BuildFullApiPath(string service = "", string resource = "", string resourceIdentifier = "", string method = "")
        {
            string url = GetApiEndPointBaseUrl().ToString(); // example: https://pay-api.amazon.eu/sandbox/

            // add the version hard-coded via the Constant as SDK version and API versions must match to ensure all objects are in sync
            url += $"v{Constants.ApiVersion}/"; // example: https://pay-api.amazon.eu/sandbox/v1/

            if (!string.IsNullOrEmpty(service))
            {
                url += $"{service}/"; // example: https://pay-api.amazon.eu/sandbox/v2/in-store/
            }

            if (!string.IsNullOrEmpty(resource))
            {
                url += $"{resource}/"; // example: https://pay-api.amazon.eu/sandbox/v1/checkoutSessions/
            }

            if (!string.IsNullOrEmpty(resourceIdentifier))
            {
                url += $"{resourceIdentifier}/"; // example: https://pay-api.amazon.eu/sandbox/v1/checkoutSessions/08eca0c9-214c-4246-a42f-af408861ce20
            }

            if (!string.IsNullOrEmpty(method))
            {
                url += $"{method}/"; // example: https://pay-api.amazon.eu/sandbox/v1/chargePermissions/S02-1239862-3042151/close
            }

            return new Uri(url);
        }

        /// <summary>
        /// Get the base URL for the API, e.g. the part within this application's execution that is static.
        /// </summary>
        public Uri GetApiEndPointBaseUrl()
        {
            string regionDomain = config.Region.ToDomain();
            string environment = config.Environment.ToString().ToLower();
            string serviceURL;

            if (config.PublicKeyId.ToUpper().StartsWith("LIVE") || config.PublicKeyId.ToUpper().StartsWith("SANDBOX"))
            {
                serviceURL = $"https://pay-api.amazon.{regionDomain}/";
            }
            else
            {
                serviceURL = $"https://pay-api.amazon.{regionDomain}/{environment}/";
            }
            return new Uri(serviceURL);
        }
    }
}
