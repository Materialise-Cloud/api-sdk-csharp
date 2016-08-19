using System;
using System.Threading.Tasks;

namespace MaterialiseCloud.Sdk
{
    public class TokenProvider
    {
        private AccessApiClient _accessApiClient;

        private string _username;
        private string _userPassword;

        private string _accessToken;
        private string _refreshToken;
        private DateTime _accesTokenExpirationTimestamp;
        private double _requestTimeCorrection;

        public TokenProvider(AccessApiClient accessApiClient, string username, string userPassword)
        {
            _accessApiClient = accessApiClient;

            _username = username;
            _userPassword = userPassword;
        }

        public async Task<string> GetAccessTokenAsync()
        {
            if (String.IsNullOrEmpty(_accessToken))
            {
                await RequestAndAssignTokensAsync();
            }

            if (IsAccessTokenExpired())
            {
                await RefreshAccessTokenAsync();
            }

            return _accessToken;
        }

        private bool IsAccessTokenExpired()
        {
            var timestamp = DateTime.UtcNow.AddSeconds(_requestTimeCorrection);
            var deltaInSeconds = 20;

            var isExpired = timestamp >= _accesTokenExpirationTimestamp.AddSeconds(-deltaInSeconds);

            return isExpired;
        }

        private async Task RequestAndAssignTokensAsync()
        {
            var tokenResponse = await _accessApiClient.GetAccessTokenAsync(_username, _userPassword);

            SetTokenData(tokenResponse);
        }

        private async Task RefreshAccessTokenAsync()
        {
            var tokenResponse = await _accessApiClient.GetAccessTokenByRefreshTokenAsync(_refreshToken);

            SetTokenData(tokenResponse);
        }

        private void SetTokenData(AccessTokenResponse tokenResponse)
        {
            _accessToken = tokenResponse.AccessToken;

            _requestTimeCorrection = tokenResponse.RefreshTokenIssued.Subtract(DateTime.UtcNow).TotalSeconds;

            _accesTokenExpirationTimestamp = tokenResponse.RefreshTokenIssued.AddSeconds(tokenResponse.AccessTokenExpiresInSec);

            _refreshToken = tokenResponse.RefreshToken;
        }
    }
}