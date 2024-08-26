namespace ClientLibary.Helpers
{
    class CustomHttpHandler : DelegatingHandler
    {
        //private GetHttpClient _httpClient;
        //private LocalStorageService _localStorageService;
        //private IUserAuthentication _userAccountServices;

        //public CustomHttpHandler(GetHttpClient httpClient, LocalStorageService localStorageService, IUserAuthentication userAccountServices)
        //{
        //    _httpClient = httpClient;
        //    _localStorageService = localStorageService;
        //    _userAccountServices = userAccountServices;
        //}

        //protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        //{
        //    var loginUrl = request.RequestUri!.AbsoluteUri.Contains("login");
        //    var registerUrl = request.RequestUri!.AbsoluteUri.Contains("register");
        //    var refreshTokenUrl = request.RequestUri!.AbsoluteUri.Contains("refresh-token");
        //    if (loginUrl || registerUrl || refreshTokenUrl)
        //    {
        //        return await base.SendAsync(request, cancellationToken);
        //    }

        //    var result = await base.SendAsync(request, cancellationToken);

        //    if (result.StatusCode == System.Net.HttpStatusCode.Unauthorized)
        //    {

        //        var localToken = await _localStorageService.GetToken();
        //        if (localToken == null)
        //        {
        //            return result;
        //        }
        //        string token = string.Empty;
        //        try
        //        {
        //            token = request.Headers.Authorization!.Parameter!;
        //        }
        //        catch (Exception)
        //        {
        //            throw;
        //        }
        //        var deserToken = Serializations.DeserializeJsonString<UserSession>(localToken);
        //        if (deserToken == null)
        //        {
        //            return result;
        //        }
        //        if (string.IsNullOrEmpty(token))
        //        {
        //            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", deserToken.Token);
        //            return await base.SendAsync(request, cancellationToken);
        //        }
        //        var newJwtToken = await GetRefreshToken(deserToken.RefreshToken!);
        //        if (newJwtToken == null) { return result; }

        //        request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", newJwtToken);
        //        return await base.SendAsync(request, cancellationToken);
        //    }
        //    return result;
        //}

        //private async Task<string> GetRefreshToken(string token)
        //{
        //    var result = await _userAccountServices.RefreshToken(new RefreshToken { Token = token });
        //    string serialToken = Serializations.SerializeObj(new UserSession { Token = result.Token, RefreshToken = result.RefreshToken });
        //    await _localStorageService.SetToken(serialToken);
        //    return result.Token!;
        //}
    }
}
