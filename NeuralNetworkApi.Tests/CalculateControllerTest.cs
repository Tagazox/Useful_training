using Microsoft.AspNetCore;
using Newtonsoft.Json;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Useful_training.Applicative.NeuralNetworkApi.Tests
{
    public class CalculateControllerTest
    {
		readonly HttpClient HttpClient;
		readonly string TestName;
		readonly string RootUrl;

        public CalculateControllerTest()
        {
            RootUrl = "Calculate";
            var application = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>{});
            HttpClient = application.CreateClient();
            TestName = Guid.NewGuid().ToString();
        }
        private void CreateNeuralNetwork()
		{
            HttpClient.PostAsync($"NeuralNetwork/{Method.POST}?Name={TestName}&numberOfInput=2&numberOfOutputs=1&numberOfHiddenLayer=3&numberOfNeuronByHiddenLayer=3&learnRate=0.05&momentum=0.05&typeOfNeuron=6", null);
        }
        private async Task<string> GetOneDummyNeuralNetwork()
		{
            var httpResponseMessage = await HttpClient.GetAsync($"NeuralNetwork/{Method.GET}/ /0/10");
            var values = JsonConvert.DeserializeObject<string[]>(await httpResponseMessage.Content.ReadAsStringAsync());
            return values.First();
        }

        [Fact]
        public async Task CalculateGetShouldBeOk()
        {
            CreateNeuralNetwork();
            string neuralNetworkName =await GetOneDummyNeuralNetwork();
            var httpResponseMessage = await HttpClient.GetAsync($"{RootUrl}/{Method.GET}/{neuralNetworkName}/%20?Inputs=1&Inputs=0");

            httpResponseMessage.IsSuccessStatusCode.Should().BeTrue();
        }
        [Fact]
        public async Task CalculateGetShouldThrow404()
        {
            string NonExistantTestName = Guid.NewGuid().ToString();
            var httpResponseMessage = await HttpClient.GetAsync($"{RootUrl}/{Method.GET}/{NonExistantTestName}/%20?Inputs=1&Inputs=0");

            httpResponseMessage.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
        }
    }
}