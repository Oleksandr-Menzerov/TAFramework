using Api.Models;
using NUnit.Framework;
using RestSharp;

namespace Api.Clients
{
    /// <summary>
    /// The ProposalsApiClient class provides methods for interacting with the proposals API.
    /// It supports operations such as creating, retrieving, updating, and deleting proposals.
    /// </summary>
    public class ProposalsApiClient() : BaseApiClient("api/proposals")
    {
        /// <summary>
        /// Asynchronously creates a new proposal using the provided data transfer object (DTO).
        /// </summary>
        /// <param name="dto">The data transfer object containing the details of the proposal to create.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the created <see cref="ProposalDto"/>.</returns>
        /// <exception cref="HttpRequestException">
        /// Thrown when the API request is unsuccessful or the response data is null, including details of the error in the exception message.
        /// </exception>
        public async Task<ProposalDto> CreateProposal(CreateProposalDto dto)
        {
            var request = CreateRequest(Method.Post);
            request.AddJsonBody(dto);

            var response = await _restClient.ExecuteAsync<ProposalDto>(request);

            if (!response.IsSuccessful || response.Data == null)
            {
                throw new AssertionException($"Error creating proposal: {FormatErrorMessage(request, response)}");
            }

            return response.Data;
        }

        /// <summary>
        /// Asynchronously retrieves a single proposal by its ID.
        /// </summary>
        /// <param name="id">The ID of the proposal to retrieve.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the retrieved <see cref="ProposalDto"/>.</returns>
        /// <exception cref="HttpRequestException">
        /// Thrown when the API request is unsuccessful, including details of the error in the exception message.
        /// </exception>
        public async Task<ProposalDto?> GetProposal(long id)
        {
            var request = CreateRequest($"/{id}");
            var response = await _restClient.ExecuteAsync<ProposalDto>(request);

            if (!response.IsSuccessful)
            {
                throw new AssertionException($"Error retrieving proposal: {FormatErrorMessage(request, response)}");
            }

            return response.Data;
        }

        /// <summary>
        /// Asynchronously retrieves a list of proposals, optionally filtered by query parameters.
        /// </summary>
        /// <param name="parameters">An optional list of query parameters to filter the proposals.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the retrieved <see cref="ProposalsDto"/>.</returns>
        /// <exception cref="HttpRequestException">
        /// Thrown when the API request is unsuccessful, including details of the error in the exception message.
        /// </exception>
        public async Task<ProposalsDto?> GetProposals(List<(string Name, string Value)>? parameters = null)
        {
            var request = CreateRequest();
            if (parameters != null)
            {
                foreach (var (Name, Value) in parameters)
                {
                    request.AddQueryParameter(Name, Value, false);
                }
            }

            var response = await _restClient.ExecuteAsync<ProposalsDto>(request);

            if (!response.IsSuccessful)
            {
                throw new AssertionException($"Error retrieving proposals: {FormatErrorMessage(request, response)}");
            }

            return response.Data;
        }

        /// <summary>
        /// Asynchronously retrieves a list of proposals, optionally filtered by query parameters.
        /// </summary>
        /// <param name="parameters">An optional list of query parameters to filter the proposals.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the retrieved <see cref="ProposalsDto"/>.</returns>
        /// <exception cref="HttpRequestException">
        /// Thrown when the API request is unsuccessful, including details of the error in the exception message.
        /// </exception>
        public async Task UpdateProposal(long id, CreateProposalDto dto)
        {
            var request = CreateRequest($"/{id}", Method.Put);
            request.AddJsonBody(dto);

            var response = await _restClient.ExecuteAsync(request);

            if (!response.IsSuccessful)
            {
                throw new AssertionException($"Error updating proposal: {FormatErrorMessage(request, response)}");
            }
        }

        /// <summary>
        /// Asynchronously retrieves a list of proposals, optionally filtered by query parameters.
        /// </summary>
        /// <param name="parameters">An optional list of query parameters to filter the proposals.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the retrieved <see cref="ProposalsDto"/>.</returns>
        /// <exception cref="HttpRequestException">
        /// Thrown when the API request is unsuccessful, including details of the error in the exception message.
        /// </exception>
        public async Task DeleteProposal(long id)
        {
            var request = CreateRequest($"?id={id}", Method.Delete);

            var response = await _restClient.ExecuteAsync(request);

            if (!response.IsSuccessful)
            {
                throw new AssertionException($"Error deleting proposal: {FormatErrorMessage(request, response)}");
            }
        }
    }
}
