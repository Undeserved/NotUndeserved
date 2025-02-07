using NotUndeserved.Twitch.ChatBot.TokenGenerator.Infrastructure;
using TwitchLib.Api.Core.Enums;
using TwitchLib.Api;
using NotUndeserved.Twitch.ChatBot.TokenGenerator.Resources;
using TwitchLib.Api.Helix.Models.Users.GetUsers;

List<string> authScopes = new List<string> {
            "chat:read",
            "chat:edit",
            "user:edit"
        };

TwitchAPI api = new TwitchAPI();
api.Settings.ClientId = TwitchAuthRes.ClientId;
api.Settings.Secret = TwitchAuthRes.ClientSecret;

WebServer server = new WebServer(TwitchAuthRes.RedirectUri);
Console.WriteLine($"Please authorize here:\n{getAuthorizationCodeUrl(TwitchAuthRes.ClientId, TwitchAuthRes.RedirectUri, authScopes)}");

var auth = server.Listen().Result;
var accessTokenResp = api.Auth.GetAccessTokenFromCodeAsync(auth.Code, TwitchAuthRes.ClientSecret, TwitchAuthRes.RedirectUri).Result;
Console.WriteLine($"Authorization success!\n\nAccess token: {accessTokenResp.AccessToken}\nRefresh token: {accessTokenResp.RefreshToken}\nExpires in: {accessTokenResp.ExpiresIn}\nScopes: {string.Join(", ", accessTokenResp.Scopes)}");
static string getAuthorizationCodeUrl(string clientId, string redirectUri, List<string> scopes) {
    var scopesStr = String.Join('+', scopes);

    return "https://id.twitch.tv/oauth2/authorize?" +
           $"client_id={clientId}&" +
           $"redirect_uri={System.Web.HttpUtility.UrlEncode(redirectUri)}&" +
           "response_type=code&" +
           $"scope={scopesStr}";
}