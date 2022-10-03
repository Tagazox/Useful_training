using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;

namespace NeuralNetworkApi.Tests
{
    public class NeuralNetworkControllerTest
    {
        HttpClient client;
        TestServer testServer;
        IWebHostBuilder builder;
        string testName;
        string RootUrl;

        public NeuralNetworkControllerTest()
        {
            RootUrl = "NeuralNetwork";
            var application = new WebApplicationFactory<Program>()
        .WithWebHostBuilder(builder =>
        {
        });
            client = application.CreateClient();
            testName = Guid.NewGuid().ToString();
        }
        [Fact]
        public async Task PostNeuralNetworkShouldBeOk()
        {
            var httpResponseMessage = await client.PostAsync($"{RootUrl}/{Method.POST}?Name={testName}&numberOfInput=2&numberOfOutputs=1&numberOfHiddenLayer=3&numberOfNeuronByHiddenLayer=3&learnRate=0.05&momentum=0.05&typeOfNeuron=6", null);

            httpResponseMessage.IsSuccessStatusCode.Should().BeTrue();
        }
        [Fact]
        public async Task PostNeuralNetworkShouldThrow500()
        {
            var httpResponseMessage = await client.PostAsync($"{RootUrl}/{Method.POST}?Name={testName}&numberOfInput=2&numberOfOutputs=1&numberOfHiddenLayer=3&numberOfNeuronByHiddenLayer=3&learnRate=0.05&momentum=-0.05&typeOfNeuron=6", null);

            httpResponseMessage.StatusCode.Should().Be(System.Net.HttpStatusCode.InternalServerError);
        }
        [Fact]
        public async Task SearchShouldBeOk()
        {
            await PostNeuralNetworkShouldBeOk();

            var httpResponseMessage = await client.GetAsync($"{RootUrl}/{Method.GET}/{testName}/0/10");


            httpResponseMessage.IsSuccessStatusCode.Should().BeTrue();
            var values = JsonConvert.DeserializeObject<string[]>(await httpResponseMessage.Content.ReadAsStringAsync());
            values.Should().NotBeNullOrEmpty();
            values.Count().Should().BeGreaterThan(0);
            values.Any(s => s.Contains(testName)).Should().BeTrue();
        }

    }
}