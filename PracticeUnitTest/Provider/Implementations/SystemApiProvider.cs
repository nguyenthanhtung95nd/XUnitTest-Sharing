using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PracticeUnitTest.Provider.Entity;
using System.Net.Http;
using System.Threading.Tasks;

namespace PracticeUnitTest.Provider.Implementations
{
    public class SystemApiProvider : ISystemApiProvider
    {
        private const string SystemApiUrl = "api/sys_api";
        private readonly ILogger<SystemApiProvider> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        public SystemApiProvider(
            IHttpClientFactory httpClientFactory, 
            ILogger<SystemApiProvider> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        public async Task<SystemApiResponse> GetSysInfoAsync(SystemApiRequest request)
        {
            var httpClient = _httpClientFactory.CreateClient(typeof(SystemApiProvider).FullName);
            var response = await httpClient.GetAsync(SystemApiUrl);
            var result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<SystemApiResponse>(result);
        }
    }
}