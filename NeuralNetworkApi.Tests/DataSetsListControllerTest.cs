using System.Net;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Useful_training.Applicative.Application.UseCases.DataSetsLists.Create.ViewModels;
using Useful_training.Applicative.Application.UseCases.DataSetsLists.Get.ViewModels;
using static System.Guid;

namespace Useful_training.Applicative.NeuralNetworkApi.Tests
{
    public class DataSetsListControllerTest
    {
        private readonly HttpClient HttpClient;
        private readonly string TestName;
        private readonly string RootUrl;

        public DataSetsListControllerTest()
        {
            RootUrl = "DataSetsList";
            var application = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(_ => { });
            HttpClient = application.CreateClient();
            TestName = NewGuid().ToString();
        }

        [Fact]
        public async Task PostDataSetListShouldBeOk()
        {
            string url = $"{RootUrl}/{Method.POST}/{TestName}";
            string bodyContentWhoContainDataSetsListToPost =
                "[" +
                "{\"inputs\": [0,0],\"targetOutput\": [0]}," +
                "{\"inputs\": [1,0],\"targetOutput\": [0]}," +
                "{\"inputs\": [0,1],\"targetOutput\": [0]}," +
                "{\"inputs\": [1,1],\"targetOutput\": [1]}" +
                "]";

            DataSetListCreatedViewModel? viewModel =
                await HttpRequestHandler.SendRequestsToTheApiAndParseResponse<DataSetListCreatedViewModel>(HttpClient, url,
                    Method.POST, bodyContentWhoContainDataSetsListToPost);

            viewModel.Should().NotBeNull();
            viewModel?.DataSetsListName.Should().Contain(TestName);
        }

        [Fact]
        public async Task PostDataSetListShouldThrow500()
        {
            string url = $"{RootUrl}/{Method.POST}/{TestName}";
            string bodyContentWhoContainBadDataSetsListToPost =
                "[" +
                "{\"inputs\": [0,0,0],\"targetOutput\": [0]}," +
                "{\"inputs\": [1,0],\"targetOutput\": [0]}," +
                "{\"inputs\": [0,1],\"targetOutput\": [0]}," +
                "{\"inputs\": [1,1],\"targetOutput\": [1]}" +
                "]";

            HttpResponseMessage httpResponseMessage =
                await HttpRequestHandler.SendRequestsToTheApi(HttpClient, url, Method.PUT,
                    bodyContentWhoContainBadDataSetsListToPost);

            httpResponseMessage.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
        }

        [Fact]
        public async Task SearchShouldBeOk()
        {
            await PostDataSetListShouldBeOk();
            string url = $"{RootUrl}/{Method.GET}/{TestName}/0/10";

            DataSetsAvailableViewModel? viewModel = await 
                HttpRequestHandler.SendRequestsToTheApiAndParseResponse<DataSetsAvailableViewModel>(HttpClient, url,
                    Method.GET);
            
            viewModel.Should().NotBeNull();
            if (viewModel != null)
            {
                viewModel.NameOfTheFoundDataSets.Should().NotBeNullOrEmpty();
                viewModel.NameOfTheFoundDataSets.Any(s => s.Contains(TestName)).Should().BeTrue();
            }
        }

        [Fact]
        public async Task GetShouldBeOk()
        {
            await PostDataSetListShouldBeOk();
            string url = $"{RootUrl}/{Method.GET}/{TestName}";

            DataSetsListViewModel? viewModel = await 
                HttpRequestHandler.SendRequestsToTheApiAndParseResponse<DataSetsListViewModel>(HttpClient, url,
                    Method.GET);

            viewModel.Should().NotBeNull();
            if (viewModel != null)
            {
                viewModel.DataSets.Should().NotBeNullOrEmpty();
                viewModel.Count.Should().BeGreaterThan(0);
                viewModel.InputsLength.Should().Be(2);
                viewModel.TargetedOutputsLength.Should().Be(1);
            }
        }

        [Fact]
        public async Task GetShouldThrow404()
        {
            await PostDataSetListShouldBeOk();
            string randomTestName = NewGuid().ToString();
            string url = $"{RootUrl}/{Method.GET}/{randomTestName}";
            
            HttpResponseMessage httpResponseMessage =
                await HttpRequestHandler.SendRequestsToTheApi(HttpClient, url, Method.GET);

            httpResponseMessage.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}