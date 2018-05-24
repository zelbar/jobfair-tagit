using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace JobFair.Tagit
{
    /// <summary>
    /// Tagit Web API Service
    /// Allows access to product name and info
    /// identified by their GS1 company prefix and Item Reference
    /// </summary>
    public class TagitWebApiService
    {
        private readonly HttpClient _client;

        /// <summary>
        /// Creates an Web API Service instance
        /// </summary>
        /// <param name="authenticationHeader">
        /// HTTP Basic authentication (authorization) header
        /// Base64 encoded username and password
        /// </param>
        public TagitWebApiService(string authenticationHeader)
        {
            _client = new HttpClient();
            _client.DefaultRequestHeaders.Authorization = 
                new AuthenticationHeaderValue("Basic", authenticationHeader);
        }

        /// <summary>
        /// Fetches product info
        /// </summary>
        /// <param name="companyPrefix">GS1 Company Prefix</param>
        /// <param name="itemReference">Product or product type identifier</param>
        /// <returns></returns>
        async public Task<string> GetProductInfo(string companyPrefix, string itemReference)
        {
            HttpResponseMessage response = null;
            try
            {
                response = await _client.GetAsync("http://jobfair.tagitsolutions.com/q934dr4/" +
                    companyPrefix + "/" + itemReference);
            }
            catch (HttpRequestException exception)
            {
                Console.Error.WriteLine("API Request Failed :(");
                Console.Error.WriteLine(exception.Message);
            }
            finally
            {
                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    Console.WriteLine("Company and/or product not found");
                }
            }
            
            return await response.Content.ReadAsStringAsync();
        }
    }
}