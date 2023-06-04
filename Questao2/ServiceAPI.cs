using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Questao2
{
    public class ServiceAPI : HttpClient
    {
        #region Properties
        private MediaTypeWithQualityHeaderValue _mediaTypeWithQualityHeaderValue;
        #endregion
        public ServiceAPI(MediaTypeWithQualityHeaderValue mediaTypeWithQualityHeaderValue)
        {
            _mediaTypeWithQualityHeaderValue = mediaTypeWithQualityHeaderValue;
        }

        public new async Task<string> GetAsync(string url)
        {
            string result = "";
            using (var httpClientHandler = new HttpClientHandler())
            {
                httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                using (var _httpClient = new HttpClient(httpClientHandler, false))
                {
                    _httpClient.DefaultRequestHeaders.Accept.Clear();
                    _httpClient.DefaultRequestHeaders.Accept.Add(_mediaTypeWithQualityHeaderValue);

                    var httpResponse = await _httpClient.GetAsync(url);
                    if (httpResponse.IsSuccessStatusCode)
                    {
                        result = httpResponse.Content.ReadAsStringAsync().Result;
                    }

                    return result;
                }
            }
        }
    }
}
