﻿using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Skybot.UI.Models;
using Skybot.UI.Settings;

namespace Skybot.UI.Services
{
    public class AccountService : IAccountService
    {
        private readonly ISettings _settings;
        private readonly IAuthorizationService _authorizationService;

        public AccountService(ISettings settings, IAuthorizationService authorizationService)
        {
            _settings = settings;
            _authorizationService = authorizationService;
        }

        public async Task<bool> HasAccount(string phoneNumber)
        {
            var accessToken = await _authorizationService.GetToken();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");

                var response = await httpClient.GetAsync($"{_settings.SkybotAccountsUri}/api/accounts/check/{phoneNumber}");
                return response.StatusCode.Equals(HttpStatusCode.Found);
            }
        }

        public async Task SendAccessCode(string phoneNumber)
        {
            var accessCode = await GetAccessCode(phoneNumber);
            if (!string.IsNullOrEmpty(accessCode))
            {
                await SendMessage(phoneNumber, accessCode);
            }
        }

        public async Task<bool> Create(UserAccountModel userAccountModel)
        {
            if (await HasAccount(userAccountModel.PhoneNumber))
            {
                var accessToken = await _authorizationService.GetToken();
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");

                    var response = await httpClient.PutAsJsonAsync($"{_settings.SkybotAccountsUri}/api/accounts/create",
                        userAccountModel);

                    return response.StatusCode.Equals(HttpStatusCode.Created);
                }
            }

            return false;
        }

        public async Task<bool> ValidateAccessCode(VerificationCodeModel verificationCodeModel)
        {
            var accessToken = await _authorizationService.GetToken();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");

                var response = await httpClient.PostAsJsonAsync($"{_settings.SkybotAccountsUri}/api/accounts/validateaccesscode", verificationCodeModel);
                return response.StatusCode.Equals(HttpStatusCode.Accepted);
            }
        }

        private async Task<string> GetAccessCode(string phoneNumber)
        {
            var accessToken = await _authorizationService.GetToken();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");

                var response = await httpClient.PostAsJsonAsync($"{_settings.SkybotAccountsUri}/api/accounts/generateaccesscode", new VerificationCodeModel
                {
                    PhoneNumber = phoneNumber
                });

                if (response.StatusCode.Equals(HttpStatusCode.OK))
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<string>(responseContent);
                }
                return string.Empty;
            }
        }

        private async Task<bool> SendMessage(string phoneNumber, string body)
        {
            var accessToken = await _authorizationService.GetToken();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");

                var response = await httpClient.PostAsJsonAsync($"{_settings.SkybotTextoUri}/api/text/send", new
                {
                    ToNumber = phoneNumber,
                    Message = body
                });

                return response.StatusCode.Equals(HttpStatusCode.OK);
            }
        }
    }
}
