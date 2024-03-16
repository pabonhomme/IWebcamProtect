using IWebcamProtect.Authentication.External;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Google.Apis.Auth;

namespace IWebcamProtect.Authentication.Google
{
    public class GoogleAuthProviderApi : ExternalAuthProviderApiBase
    {
        public override async Task<ExternalAuthUserInfo> GetUserInfo(string accessCode)
        {
            using (var client = new HttpClient())
            {
                var settings = new GoogleJsonWebSignature.ValidationSettings()
                {
                    Audience = new List<string> { ProviderInfo.ClientId }
                };

                var payload = await GoogleJsonWebSignature.ValidateAsync(accessCode, settings);

                return new ExternalAuthUserInfo
                {
                    EmailAddress = payload.Email,
                    Name = payload.GivenName,
                    Surname = payload.FamilyName,
                    Provider = "ExternalAuth",
                    ProviderKey = payload.Subject,
                    Picture = payload.Picture
                };
            }
        }
    }
}
