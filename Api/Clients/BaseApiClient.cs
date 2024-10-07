using System.Text;
using Core.Configuration;
using RestSharp;

namespace Api.Clients
{
    /// <summary>
    /// The BaseApiClient class serves as a foundational client for interacting with RESTful APIs.
    /// It provides core functionalities such as initializing the RestClient with a base URL and 
    /// formatting error messages from API requests.
    /// </summary>
    public class BaseApiClient
    {
        /// <summary>
        /// Internal instance of RestClient used to perform HTTP requests to the specified API.
        /// </summary>
        internal readonly RestClient _restClient;

        /// <summary>
        /// The base URL of the API, retrieved from the application configuration.
        /// </summary>
        internal readonly string _baseUrl;

        /// <summary>
        /// The endpoint URL of the API.
        /// </summary>
        internal readonly string _endpointUrl;

        /// <summary>
        /// The Baerer auth token.
        /// </summary>
        internal readonly string _authToken;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseApiClient"/> class.
        /// The constructor retrieves the base URL from the configuration and initializes the RestClient.
        /// <param name="endpointUrl">The <see cref="string"/> that used for endpoint calls Url building.</param>
        /// </summary>
        public BaseApiClient(string endpointUrl)
        {
            _baseUrl = ConfigurationManager.AppSettings.ApiBaseUrl;
            _restClient = new RestClient(_baseUrl);
            _endpointUrl = endpointUrl;
            _authToken = ConfigurationManager.AppSettings.AuthToken;
        }

        public RestRequest CreateRequest(string suffix, Method? method = null)
        {
            string resource = _endpointUrl + suffix;
            Method requestMethod = method ?? Method.Get;
            RestRequest request = new(resource, requestMethod);
            request.AddHeader("Authorization", $"Bearer {_authToken}");
            return request;
        }

        public RestRequest CreateRequest(Method? method = null)
        {
            return CreateRequest(string.Empty, method);
        }

        /// <summary>
        /// Formats a detailed error message containing information about the request and the response.
        /// This can be useful for logging and debugging purposes.
        /// </summary>
        /// <param name="request">The <see cref="RestRequest"/> that was sent to the API.</param>
        /// <param name="response">The <see cref="RestResponse"/> received from the API.</param>
        /// <returns>A formatted string containing details about the request and the error response.</returns>
        protected string FormatErrorMessage(RestRequest request, RestResponse response)
        {
            var requestDetails = new StringBuilder();
            requestDetails.AppendLine($"Request Method: {request.Method}");
            requestDetails.AppendLine($"Request URL: {_baseUrl}{request.Resource}");

            if (request.Parameters.Any())
            {
                requestDetails.AppendLine("Request Parameters:");
                foreach (var parameter in request.Parameters)
                {
                    requestDetails.AppendLine($"{parameter.Name}: {parameter.Value}");
                }
            }

            var errorMessage = new StringBuilder();
            errorMessage.AppendLine($"Error: {response.StatusCode} - {response.ErrorMessage}");
            errorMessage.AppendLine($"Request Details:\n{requestDetails}");

            if (response.Content != null)
            {
                errorMessage.AppendLine($"Response content:\n{response.Content}");
            }

            return errorMessage.ToString();
        }
    }
}
