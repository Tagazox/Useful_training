using System.Net;
using static System.Guid;
using Newtonsoft.Json;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Useful_training.Applicative.Application.UseCases.NeuralNetworks.Create.ViewModels;
using Useful_training.Applicative.Application.UseCases.NeuralNetworks.Get.ViewModels;

namespace Useful_training.Applicative.NeuralNetworkApi.Tests
{
    public class NeuralNetworkControllerTest
    {
        private readonly HttpClient HttpClient;
        private readonly string TestName;
        private readonly string RootUrl;

        public NeuralNetworkControllerTest()
        {
            RootUrl = "NeuralNetwork";
            WebApplicationFactory<Program> application = new WebApplicationFactory<Program>().WithWebHostBuilder(_ => { });
            HttpClient = application.CreateClient();
            TestName = NewGuid().ToString();
        }

        [Fact]
        public async Task PostNeuralNetworkShouldBeOk()
        {
            HttpResponseMessage httpResponseMessage = await HttpClient.PostAsync(
                $"{RootUrl}/{Method.POST}?Name={TestName}&numberOfInput=2&numberOfOutputs=1&numberOfHiddenLayer=3&numberOfNeuronByHiddenLayer=3&learnRate=0.05&momentum=0.05&typeOfNeuron=6",
                null);
            
            NeuralNetworkCreatedViewModel? deserializedResponse =
                JsonConvert.DeserializeObject<NeuralNetworkCreatedViewModel>(await httpResponseMessage.Content.ReadAsStringAsync());
            deserializedResponse.Should().NotBeNull();

            deserializedResponse?.FinalNameOfTheNeuralNetwork.Should().Contain(TestName);
        }

        [Fact]
        public async Task PostNeuralNetworkShouldThrow500()
        {
            HttpResponseMessage httpResponseMessage = await HttpClient.PostAsync(
                $"{RootUrl}/{Method.POST}?Name={TestName}&numberOfInput=2&numberOfOutputs=1&numberOfHiddenLayer=3&numberOfNeuronByHiddenLayer=3&learnRate=0.05&momentum=-0.05&typeOfNeuron=6",
                null);

            httpResponseMessage.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
        }

        [Fact]
        public async Task SearchShouldBeOk()
        {
            await PostNeuralNetworkShouldBeOk();
            HttpResponseMessage httpResponseMessage =
                await HttpClient.GetAsync($"{RootUrl}/{Method.GET}/{TestName}/0/10");

            httpResponseMessage.IsSuccessStatusCode.Should().BeTrue();
            NeuralNetworksFoundViewModel? deserializedValues =
                JsonConvert.DeserializeObject<NeuralNetworksFoundViewModel>(await httpResponseMessage.Content
                    .ReadAsStringAsync());
            deserializedValues.Should().NotBeNull();
            if (deserializedValues != null)
            {
                deserializedValues.NamesList.Should().NotBeNullOrEmpty();
                deserializedValues.NamesList.Count().Should().BeGreaterThan(0);
                deserializedValues.NamesList.Any(s => s.Contains(TestName)).Should().BeTrue();
            }
        }
    }
}