using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MaterialiseCloud.Sdk
{
    public class AccessApiClient
    {
        private string _host;
        private string _clientId;
        private string _clientSecret;

        public AccessApiClient(string host, string clientId, string clientSecret)
        {
            _host = host;
            _clientId = clientId;
            _clientSecret = clientSecret;
        }

        public async Task<AccessTokenResponse> GetAccessTokenAsync(string userEmail, string userPassword)
        {
            using (var client = CreateHttpClient())
            {
                var request = new Dictionary<string, string>
                {
                    {"grant_type", "password"},
                    {"username", userEmail},
                    {"password", userPassword}
                };
                var postContent = new FormUrlEncodedContent(request);

                var response = await client.PostAsync("token", postContent);

                var tokenResponse = await response.Content.ReadAsAsync<AccessTokenResponse>();
                return tokenResponse;
            }
        }

        public async Task<AccessTokenResponse> GetAccessTokenByRefreshTokenAsync(string refreshToken)
        {
            using (var client = CreateHttpClient())
            {
                var request = new Dictionary<string, string>
                {
                    {"grant_type", "refresh_token"},
                    {"refresh_token", refreshToken}
                };
                var postContent = new FormUrlEncodedContent(request);

                var response = await client.PostAsync("token", postContent);

                var tokenResponse = await response.Content.ReadAsAsync<AccessTokenResponse>();
                return tokenResponse;
            }
        }

        private HttpClient CreateHttpClient()
        {
            var client = new HttpClient();

            var authHeaderString = $"{_clientId}:{_clientSecret}";
            var authorizationHeader = Convert.ToBase64String(Encoding.Default.GetBytes(authHeaderString));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authorizationHeader);
            client.BaseAddress = new Uri(_host);

            return client;
        }
    }

    public class AccessTokenResponse
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("expires_in")]
        public int AccessTokenExpiresInSec { get; set; }

        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }

        [JsonProperty(".issued")]
        public DateTime RefreshTokenIssued { get; set; }

        [JsonProperty(".expires")]
        public DateTime RefreshTokenExpires { get; set; }
    }
}