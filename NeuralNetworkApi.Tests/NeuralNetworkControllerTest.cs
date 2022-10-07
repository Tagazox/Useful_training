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
            string url = $"{RootUrl}/{Method.POST}?Name={TestName}&numberOfInput=2&numberOfOutputs=1&numberOfHiddenLayer=3&numberOfNeuronByHiddenLayer=3&learnRate=0.05&momentum=0.05&typeOfNeuron=6"; 
           
            NeuralNetworkCreatedViewModel? viewModel = await 
                HttpRequestHandler.SendRequestsToTheApiAndParseResponse<NeuralNetworkCreatedViewModel>(HttpClient, url,
                    Method.POST);
            
            viewModel?.FinalNameOfTheNeuralNetwork.Should().Contain(TestName);
        }

        [Fact]
        public async Task PostNeuralNetworkShouldThrow500()
        {
            string urlWithBadMomentum = $"{RootUrl}/{Method.POST}?Name={TestName}&numberOfInput=2&numberOfOutputs=1&numberOfHiddenLayer=3&numberOfNeuronByHiddenLayer=3&learnRate=0.05&momentum=-0.05&typeOfNeuron=6"; 

            HttpResponseMessage? httpResponseMessage = await 
                HttpRequestHandler.SendRequestsToTheApi(HttpClient, urlWithBadMomentum,
                    Method.POST);

            httpResponseMessage.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
        }

        [Fact]
        public async Task SearchShouldBeOk()
        {
            string url = $"{RootUrl}/{Method.GET}/{TestName}/0/10";
            await PostNeuralNetworkShouldBeOk();
            NeuralNetworksFoundViewModel? viewModel = await 
                HttpRequestHandler.SendRequestsToTheApiAndParseResponse<NeuralNetworksFoundViewModel>(HttpClient, url,
                    Method.GET);
            
            viewModel.Should().NotBeNull();
            if (viewModel != null)
            {
                viewModel.NamesList.Should().NotBeNullOrEmpty();
                viewModel.NamesList.Count().Should().BeGreaterThan(0);
                viewModel.NamesList.Any(s => s.Contains(TestName)).Should().BeTrue();
            }
        }
    }
}