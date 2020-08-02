using IdentityModel.Client;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace AvailableGroups.Helpers
{
    public class ApiService : IApiService
    {
        private readonly IHttpClientFactory _clientFactory;

        public ApiService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<string> GetApiDataAsync(string protectedApiUrl, string access_Token)
        {
            try
            {
                var client = _clientFactory.CreateClient();
                client.SetBearerToken(access_Token);

                var response = await client.GetAsync(protectedApiUrl);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
                throw new ApplicationException($"Status code: {response.StatusCode}, Error: {response.ReasonPhrase}");
            }
            catch (Exception e)
            {
                throw new ApplicationException($"Exception {e}");
            }
        }

    }
}
