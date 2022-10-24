using Figure.Application.Models;
using Figure.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Net;

namespace Figure.Tests;

public class FigureControllerTests {
    private HttpClient _httpClient;
    private JsonSerializerSettings _settings;

    private readonly string apiPath = "api/figures";

    public FigureControllerTests() {
        var webAppFactory = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder => {
                builder.UseEnvironment("Testing");
            });
        _httpClient = webAppFactory.CreateDefaultClient();


        _settings = new JsonSerializerSettings {
            TypeNameHandling = TypeNameHandling.All
        };

    }

    [Test]
    [TestCase(5,1)]
    [TestCase(3,1)]
    [TestCase(1,1)]
    public async Task GetAllFigures_PageSizeX_ShouldReturnXOrders(int pS, int pN) {
        //ARRANGE
        //ACT
        int pageSize = pS;
        int pageNumber = pN;
        var response = await _httpClient.GetAsync(apiPath + "?pageSize=" + pageSize + "&pageNumber=" + pageNumber);


        var apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync(), _settings);
        var models = JsonConvert.DeserializeObject<IEnumerable<ReadFigureModel>>(apiResponse.Content.ToString(), _settings);

        //ASSERT
        Assert.That(response != null && models != null);
        Assert.That(models.Count() == pS);
    }

    [Test]
    public async Task GetFigure_GuidDoesntExist_Returns404() {
        //ARRANGE
        //ACT
        var response = await _httpClient.GetAsync(apiPath + "/" + Guid.NewGuid());
        var apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync(), _settings);

        //ASSERT
        Assert.That(apiResponse.IsSuccess == false);
        Assert.That(apiResponse.StatusCode == HttpStatusCode.NotFound);
    }

    [Test]
    public async Task GetFigure_GuidExists_ReturnsModel() {
        //ARRANGE
        var response = await _httpClient.GetAsync(apiPath + "/?pageSize=" + 1 + "&pageNumber=" + 1);
        var apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync(), _settings);
        var models = JsonConvert.DeserializeObject<IEnumerable<ReadFigureModel>>(apiResponse.Content.ToString(), _settings);

        //ACT
        response = await _httpClient.GetAsync(apiPath + "/" + models.First().Id);
        apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync(), _settings);
        var model = JsonConvert.DeserializeObject<ReadFigureModel>(apiResponse.Content.ToString(), _settings);

        //ASSERT
        Assert.That(response != null && model != null);
        Assert.That(model.Id == models.First().Id);
    }
}

