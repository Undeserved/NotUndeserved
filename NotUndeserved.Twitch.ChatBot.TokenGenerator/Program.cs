using NotUndeserved.Twitch.ChatBot.TokenGenerator.Infrastructure;
using TwitchLib.Api;
using NotUndeserved.Twitch.ChatBot.TokenGenerator.Resources;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Microsoft.Extensions.DependencyInjection;
using NotUndeserved.Twitch.ChatBot.Application;
using NotUndeserved.Twitch.ChatBot.Persistence;
using Microsoft.Extensions.Configuration;
using MediatR;
using NotUndeserved.Twitch.ChatBot.Application.Settings.Commands;

List<string> authScopes = new List<string> {
            "chat:read",
            "chat:edit",
            "user:edit"
        };

TwitchAPI api = new TwitchAPI();
api.Settings.ClientId = TwitchAuthRes.ClientId;
api.Settings.Secret = TwitchAuthRes.ClientSecret;

WebServer server = new WebServer(TwitchAuthRes.RedirectUri);
//Console.WriteLine($"Please authorize here:\n{getAuthorizationCodeUrl(TwitchAuthRes.ClientId, TwitchAuthRes.RedirectUri, authScopes)}");
openBrowser(getAuthorizationCodeUrl(TwitchAuthRes.ClientId,TwitchAuthRes.RedirectUri,authScopes));

var auth = server.Listen().Result;
var accessTokenResp = api.Auth.GetAccessTokenFromCodeAsync(auth.Code, TwitchAuthRes.ClientSecret, TwitchAuthRes.RedirectUri).Result;

var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory()) 
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true) 
                .AddEnvironmentVariables()
                .Build();

var serviceProvider = new ServiceCollection()
    .AddApplication(configuration)
    .AddPersistence(configuration)
    .BuildServiceProvider();

using (var scope = serviceProvider.CreateScope()) {
    var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
    await mediator.Send(new UpdateSettingsCommand {
        ClientId = TwitchAuthRes.ClientId,
        ClientSecret = TwitchAuthRes.ClientSecret,
        OAuthToken = accessTokenResp.AccessToken,
        RefreshToken = accessTokenResp.RefreshToken,
        TokenValidUntil = DateTime.Now.AddSeconds(accessTokenResp.ExpiresIn)
    });
}

//Console.WriteLine($"Authorization success!\n\nAccess token: {accessTokenResp.AccessToken}\nRefresh token: {accessTokenResp.RefreshToken}\nExpires in: {accessTokenResp.ExpiresIn}\nScopes: {string.Join(", ", accessTokenResp.Scopes)}");
static string getAuthorizationCodeUrl(string clientId, string redirectUri, List<string> scopes) {
    var scopesStr = String.Join('+', scopes);

    return "https://id.twitch.tv/oauth2/authorize?" +
           $"client_id={clientId}&" +
           $"redirect_uri={System.Web.HttpUtility.UrlEncode(redirectUri)}&" +
           "response_type=code&" +
           $"scope={scopesStr}";
}

static void openBrowser(string url) {
    try {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) {
            Process.Start(new ProcessStartInfo { FileName = url, UseShellExecute = true });
        } else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux)) {
            Process.Start("xdg-open", url);
        } else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX)) {
            Process.Start("open", url);
        } else {
            throw new NotSupportedException("Begone, BSDoid.");
        }
    } catch (Exception ex) {
        Console.WriteLine($"Could not open browser: {ex.Message}");
    }
}