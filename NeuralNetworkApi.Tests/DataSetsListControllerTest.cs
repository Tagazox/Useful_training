using System.Net;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Useful_training.Applicative.Application.UseCases.DataSetsLists.Create.ViewModels;
using Useful_training.Applicative.Application.UseCases.DataSetsLists.Get.ViewModels;
using static System.Guid;

namespace Useful_training.Applicative.NeuralNetworkApi.Tests;

public class DataSetsListControllerTest
{
    private readonly HttpClient _httpClient;
    private readonly string _testName;
    private readonly string _rootUrl;

    public DataSetsListControllerTest()
    {
        _rootUrl = "DataSetsList";
        var application = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(_ => { });
        _httpClient = application.CreateClient();
        _testName = NewGuid().ToString();
    }

    [Fact]
    public async Task PostDataSetListShouldBeOk()
    {
        string url = $"{_rootUrl}/{Method.Post}/{_testName}";
        const string bodyContentWhoContainDataSetsListToPost =
            "[" +
            "{\"inputs\": [0,0],\"targetOutputs\": [0]}," +
            "{\"inputs\": [1,0],\"targetOutputs\": [0]}," +
            "{\"inputs\": [0,1],\"targetOutputs\": [0]}," +
            "{\"inputs\": [1,1],\"targetOutputs\": [1]}" +
            "]";

        DataSetListCreatedViewModel? viewModel =
            await HttpRequestHandler.SendRequestsToTheApiAndParseResponse<DataSetListCreatedViewModel>(_httpClient, url,
                Method.Post, bodyContentWhoContainDataSetsListToPost);

        viewModel.Should().NotBeNull();
        viewModel?.DataSetsListName.Should().Contain(_testName);
    }

    [Fact]
    public async Task PostDataSetListShouldThrow500Case1()
    {
        string url = $"{_rootUrl}/{Method.Post}/{_testName}";
        const string bodyContentWhoContainBadDataSetsListToPost = 
            "[" +
            "{\"inputs\": [0,0,0],\"targetOutputs\": [0]}," +
            "{\"inputs\": [1,0],\"targetOutputs\": [0]}," +
            "{\"inputs\": [0,1],\"targetOutputs\": [0]}," +
            "{\"inputs\": [1,1],\"targetOutputs\": [1]}" +
            "]";

        HttpResponseMessage httpResponseMessage =
            await HttpRequestHandler.SendRequestsToTheApi(_httpClient, url, Method.Put,
                bodyContentWhoContainBadDataSetsListToPost);

        httpResponseMessage.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
    }

    [Fact]
    public async Task PostDataSetListShouldThrow500Case2()
    {
        string url = $"{_rootUrl}/{Method.Post}/{_testName}";
        const string bodyContentWhoContainBadDataSetsListToPost = 
            "[" +
            "{\"inputs\": [],\"targetOutputs\": [0]}" +
            "]";

        HttpResponseMessage httpResponseMessage =
            await HttpRequestHandler.SendRequestsToTheApi(_httpClient, url, Method.Put,
                bodyContentWhoContainBadDataSetsListToPost);

        httpResponseMessage.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
    }
    [Fact]
    public async Task SearchShouldBeOk()
    {
        await PostDataSetListShouldBeOk();
        string url = $"{_rootUrl}/{Method.Get}/{_testName}/0/10";

        DataSetsAvailableViewModel? viewModel = await
            HttpRequestHandler.SendRequestsToTheApiAndParseResponse<DataSetsAvailableViewModel>(_httpClient, url,
                Method.Get);

        viewModel.Should().NotBeNull();
        if (viewModel != null)
        {
            viewModel.NameOfTheFoundDataSets.Should().NotBeNullOrEmpty();
            viewModel.NameOfTheFoundDataSets.Any(s => s.Contains(_testName)).Should().BeTrue();
        }
    }

    [Fact]
    public async Task GetShouldBeOk()
    {
        PostDataSetListShouldBeOk().Wait();
        string url = $"{_rootUrl}/{Method.Get}/{_testName}";

        DataSetsListViewModel? viewModel = await
            HttpRequestHandler.SendRequestsToTheApiAndParseResponse<DataSetsListViewModel>(_httpClient, url,
                Method.Get);

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
        string url = $"{_rootUrl}/{Method.Get}/{randomTestName}";

        HttpResponseMessage httpResponseMessage =
            await HttpRequestHandler.SendRequestsToTheApi(_httpClient, url, Method.Get);

        httpResponseMessage.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}