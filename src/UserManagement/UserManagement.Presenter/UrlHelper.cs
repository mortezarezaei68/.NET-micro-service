using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Primitives;

namespace UserManagement.Presenter;

public static class UrlHelper
{

    public static Dictionary<string,StringValues> AddOrReplaceQueryParameters(this HttpContext c)
    {
        var request = c.Request;
        UriBuilder uriBuilder = new UriBuilder
        {
            Scheme = request.Scheme,
            Host = request.Host.Host,
            Port = request.Host.Port ?? 0,
            Path = request.Path.ToString(),
            Query = request.QueryString.ToString()
        };

        return QueryHelpers.ParseQuery(uriBuilder.Query);

        // foreach (var (p,v) in pvs)
        // {
        //     queryParams.Remove(p);
        //     queryParams.Add(p, v);
        // }
        //
        // uriBuilder.Query = "";
        // var allQPs = queryParams.ToDictionary(k => k.Key, k => k.Value.ToString());
        // var url = QueryHelpers.AddQueryString(uriBuilder.ToString(),allQPs);
        //
        // return url;
    }
}