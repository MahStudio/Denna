using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;
using Windows.Security.Authentication.Web.Core;
using Windows.Security.Credentials;

namespace Core.Service.Auth
{
    public static class MicrosoftLogin
    {
        public static async Task<TokenModel> AuthMicrosoft()
        {
            const string MicrosoftAccountProviderId = "https://login.microsoft.com";
            const string ConsumerAuthority = "consumers";
            const string AccountScopeRequested = "wl.signin,wl.emails";
            const string AccountClientId = "none";

            WebAccountProvider provider = await WebAuthenticationCoreManager.FindAccountProviderAsync(MicrosoftAccountProviderId, ConsumerAuthority);
            WebTokenRequest webTokenRequest = new WebTokenRequest(provider, AccountScopeRequested, AccountClientId, WebTokenRequestPromptType.ForceAuthentication);
            WebTokenRequestResult webTokenRequestResult = await WebAuthenticationCoreManager.RequestTokenAsync(webTokenRequest);
            if (webTokenRequestResult.ResponseStatus == WebTokenRequestStatus.Success)
            {
                var token = webTokenRequestResult.ResponseData.FirstOrDefault().Token;
                var restApi = new Uri(@"https://apis.live.net/v5.0/me");

                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    var infoResult = await client.GetAsync(restApi);
                    string content = await infoResult.Content.ReadAsStringAsync();

                    var jsonObject = JsonObject.Parse(content);
                    var id = jsonObject["emails"].GetObject()["preferred"].GetString();
                    string name = jsonObject["name"].GetString();

                    var usr = new TokenModel()
                    {
                        Email = id,
                        Name = name,
                        Token = token
                    };
                    return usr;
                }
            }
            else
                throw new Exception("Login failed");
        }
    }
}
