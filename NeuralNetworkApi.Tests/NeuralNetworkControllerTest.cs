using Microsoft.AspNetCore;
using Newtonsoft.Json;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Useful_training.Applicative.NeuralNetworkApi.Tests
{
    public class NeuralNetworkControllerTest
    {
		readonly HttpClient HttpClient;
		readonly string TestName;
		readonly string RootUrl;

        public NeuralNetworkControllerTest()
        {
            RootUrl = "NeuralNetwork";
            var application = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>{});
            HttpClient = application.CreateClient();
            TestName = Guid.NewGuid().ToString();
        }
        [Fact]
        public async Task PostNeuralNetworkShouldBeOk()
        {
            var httpResponseMessage = await HttpClient.PostAsync($"{RootUrl}/{Method.POST}?Name={TestName}&numberOfInput=2&numberOfOutputs=1&numberOfHiddenLayer=3&numberOfNeuronByHiddenLayer=3&learnRate=0.05&momentum=0.05&typeOfNeuron=6", null);

            httpResponseMessage.IsSuccessStatusCode.Should().BeTrue();
        }
        [Fact]
        public async Task PostNeuralNetworkShouldThrow500()
        {
            var httpResponseMessage = await HttpClient.PostAsync($"{RootUrl}/{Method.POST}?Name={TestName}&numberOfInput=2&numberOfOutputs=1&numberOfHiddenLayer=3&numberOfNeuronByHiddenLayer=3&learnRate=0.05&momentum=-0.05&typeOfNeuron=6", null);

            httpResponseMessage.StatusCode.Should().Be(System.Net.HttpStatusCode.InternalServerError);
        }
        [Fact]
        public async Task SearchShouldBeOk()
        {
            await PostNeuralNetworkShouldBeOk();

            var httpResponseMessage = await HttpClient.GetAsync($"{RootUrl}/{Method.GET}/{TestName}/0/10");


            httpResponseMessage.IsSuccessStatusCode.Should().BeTrue();
            var values = JsonConvert.DeserializeObject<string[]>(await httpResponseMessage.Content.ReadAsStringAsync());
            values.Should().NotBeNullOrEmpty();
            values.Count().Should().BeGreaterThan(0);
            values.Any(s => s.Contains(TestName)).Should().BeTrue();
        }

    }
}