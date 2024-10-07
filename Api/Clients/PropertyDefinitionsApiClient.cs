using Api.Models;
using RestSharp;

namespace Api.Clients
{
    /// <summary>
    /// The PropertyDefinitionsApiClient class provides methods for interacting with the property definitions
    /// API. It allows you to retrieve a list of property definitions or a single property definition by ID.
    /// </summary>
    public class PropertyDefinitionsApiClient() : BaseApiClient("api/property-definitions")
    {
        /// <summary>
        /// Asynchronously retrieves a list of all property definitions from the API.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of <see cref="PropertyDefinitionDto"/>.</returns>
        /// <exception cref="HttpRequestException">
        /// Thrown when the API request is unsuccessful, including details of the error in the exception message.
        /// </exception>
        public async Task<List<PropertyDefinitionDto>> GetPropertyDefinitions()
        {
            var request = CreateRequest();
            var response = await _restClient.ExecuteAsync<List<PropertyDefinitionDto>>(request);

            if (!response.IsSuccessful)
            {
                throw new HttpRequestException($"Error retrieving property definitions: {FormatErrorMessage(request, response)}");
            }

            return response.Data ?? [];
        }

        /// <summary>
        /// Asynchronously retrieves a single property definition by its ID.
        /// </summary>
        /// <param name="id">The ID of the property definition to retrieve.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a <see cref="PropertyDefinitionDto"/>.</returns>
        /// <exception cref="HttpRequestException">
        /// Thrown when the API request is unsuccessful or the response data is null, including details of the error in the exception message.
        /// </exception>
        public async Task<PropertyDefinitionDto> GetPropertyDefinition(long id)
        {
            var request = CreateRequest($"/{id}");
            var response = await _restClient.ExecuteAsync<PropertyDefinitionDto>(request);

            if (!response.IsSuccessful || response.Data == null)
            {
                throw new HttpRequestException($"Error retrieving property definition: {FormatErrorMessage(request, response)}");
            }

            return response.Data;
        }
    }
}
