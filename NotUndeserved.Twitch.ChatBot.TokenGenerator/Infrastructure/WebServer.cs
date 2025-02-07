using NotUndeserved.Twitch.ChatBot.TokenGenerator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NotUndeserved.Twitch.ChatBot.TokenGenerator.Infrastructure {
    public class WebServer {
        private HttpListener listener;

        public WebServer(string uri) {
            listener = new HttpListener();
            listener.Prefixes.Add(uri);
        }

        public async Task<ApiAuthorization> Listen() {
            listener.Start();
            return await onRequest();
        }

        private async Task<ApiAuthorization> onRequest() {
            while (listener.IsListening) {
                var ctx = await listener.GetContextAsync();
                var req = ctx.Request;
                var resp = ctx.Response;

                using (var writer = new StreamWriter(resp.OutputStream)) {
                    if (req.QueryString.AllKeys.Any("code".Contains)) {
                        writer.WriteLine("Authorization started! Check your application!");
                        writer.Flush();
                        return new ApiAuthorization(req.QueryString["code"]);
                    } else {
                        writer.WriteLine("No code found in query string!");
                        writer.Flush();
                    }
                }
            }
            return null;
        }
    }
}
