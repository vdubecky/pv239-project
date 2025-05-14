namespace pv239_project.Middleware;

public class AuthHandler : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken
    )
    {
        var token = await SecureStorage.GetAsync("jwt_token");
        request.Headers.Add("Authorization", $"Bearer {token}");
        var response = await base.SendAsync(request, cancellationToken);

        return response;
    }
}
