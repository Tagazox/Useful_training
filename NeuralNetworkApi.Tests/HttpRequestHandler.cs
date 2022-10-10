global using Xunit;
using FluentAssertions;
using Newtonsoft.Json;

namespace Useful_training.Applicative.NeuralNetworkApi.Tests;

internal static class HttpRequestHandler
{
    internal static async Task<HttpResponseMessage> SendRequestsToTheApi(HttpClient client,string url, Method method, string postBodyContent = "")
    {
        HttpContent body = new StringContent(postBodyContent);
        body.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
        return method == Method.Get ? await client.GetAsync(url) : await client.PostAsync(url, body);
    }        
    internal static async Task<T?> SendRequestsToTheApiAndParseResponse<T>(HttpClient client,string url, Method method, string postBodyContent = "")
    {
        HttpContent body = new StringContent(postBodyContent);
        body.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
        HttpResponseMessage httpResponseMessage = method == Method.Get ? await client.GetAsync(url) : await client.PostAsync(url, body);
        httpResponseMessage.IsSuccessStatusCode.Should().BeTrue();

        return JsonConvert.DeserializeObject<T>(await httpResponseMessage.Content
            .ReadAsStringAsync());
    }    
}