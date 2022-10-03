using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Useful_training.Core.Neural_network.ValueObject;

namespace NeuralNetworkApi.Tests
{
    public class DataSetsListControllerTest
    {
        HttpClient client;
        string testName;
        string RootUrl;
        public DataSetsListControllerTest()
        {
            RootUrl = "DataSetsList";
            var application = new WebApplicationFactory<Program>()
        .WithWebHostBuilder(builder =>
        {
        });
            client = application.CreateClient();
            testName = Guid.NewGuid().ToString();
        }
        [Fact]
        public async Task PostDataSetListShouldBeOk()
        {
            HttpContent body = new StringContent("[{\"inputs\": [0,0],\"targetOutput\": [0]},{\"inputs\": [1,0],\"targetOutput\": [0]},{\"inputs\": [0,1],\"targetOutput\": [0]},{\"inputs\": [1,1],\"targetOutput\": [1]}]	");
            body.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            var httpResponseMessage = await client.PostAsync($"{RootUrl}/{Method.POST}/{testName}", body);

            httpResponseMessage.IsSuccessStatusCode.Should().BeTrue();
        }
        [Fact]
        public async Task PostDataSetListShouldThrow500()
        {
            HttpContent body = new StringContent("[{\"inputs\": [0,0,0],\"targetOutput\": [0]},{\"inputs\": [1,0],\"targetOutput\": [0]},{\"inputs\": [0,1],\"targetOutput\": [0]},{\"inputs\": [1,1],\"targetOutput\": [1]}]	");
            body.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            var httpResponseMessage = await client.PostAsync($"{RootUrl}/{Method.POST}/{testName}", body);

            httpResponseMessage.StatusCode.Should().Be(System.Net.HttpStatusCode.InternalServerError);
        }
        [Fact]
        public async Task SearchShouldBeOk()
        {
            await PostDataSetListShouldBeOk();

            var httpResponseMessage = await client.GetAsync($"{RootUrl}/{Method.GET}/{testName}/0/10");

            var values = JsonConvert.DeserializeObject<string[]>(await httpResponseMessage.Content.ReadAsStringAsync());
            httpResponseMessage.IsSuccessStatusCode.Should().BeTrue();
            values.Should().NotBeNullOrEmpty();
            values.Count().Should().BeGreaterThan(0);
            values.Any(s => s.Contains(testName)).Should().BeTrue();
        }

        [Fact]
        public async Task GetShouldBeOk()
        {
            await PostDataSetListShouldBeOk();

            var httpResponseMessage = await client.GetAsync($"{RootUrl}/{Method.GET}/{testName}");

            List<DataSet> dataSet = JsonConvert.DeserializeObject<List<DataSet>>(await httpResponseMessage.Content.ReadAsStringAsync());
            httpResponseMessage.IsSuccessStatusCode.Should().BeTrue();
            dataSet.Should().NotBeNullOrEmpty();
            dataSet.Count().Should().BeGreaterThan(0);
            dataSet.First().Inputs.Count.Should().Be(2);
        }
        [Fact]
        public async Task GetShouldThrow404()
        {
            await PostDataSetListShouldBeOk();
            testName= Guid.NewGuid().ToString();
            var httpResponseMessage = await client.GetAsync($"{RootUrl}/{Method.GET}/{testName}");

            httpResponseMessage.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
        }
    }
}