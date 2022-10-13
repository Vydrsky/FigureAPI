using Figure.Application.Models.Order;
using Figure.DataAccess.Entities;
using Figure.Infrastructure;
using Figure.Tests.Builders;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Net;
using System.Text;

namespace Figure.Tests;

public class OrdersControllerTests {

    private HttpClient _httpClient;
    private JsonSerializerSettings _settings;

    public OrdersControllerTests() {
        var webAppFactory = new WebApplicationFactory<Program>();
        _httpClient = webAppFactory.CreateDefaultClient();

        _settings = new JsonSerializerSettings {
            TypeNameHandling = TypeNameHandling.All
        };
    }

    [Test]
    public async Task GetAllOrders_PageSize5_ShouldReturn5Orders() {
        //ARRANGE
        //ACT
        int pageSize = 100;
        int pageNumber = 1;
        var response = await _httpClient.GetAsync("api/orders?pageSize=" + pageSize + "&pageNumber=" + pageNumber);


        var apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync(), _settings);
        var models = JsonConvert.DeserializeObject<IEnumerable<ReadOrderModel>>(apiResponse.Content.ToString(), _settings);

        //ASSERT
        Assert.That(response != null && models != null);
        Assert.That(models.Count() == 5);
    }

    [Test]
    public async Task GetArchivedOrders_EveryOrderIsFlaggedAsArchived() {
        //ARRANGE
        int pageSize = 100;
        int pageNumber = 1;

        //ACT
        var response = await _httpClient.GetAsync("api/orders/archive?pageSize=" + pageSize + "&pageNumber=" + pageNumber);
        var apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync(), _settings);
        var models = JsonConvert.DeserializeObject<IEnumerable<ReadOrderModel>>(apiResponse.Content.ToString(), _settings);

        //ASSERT
        Assert.That(response != null && models != null);
        Assert.That(models.All(m => m.IsArchived == true));
    }

    
    [Test]
    public async Task GetDeArchivedOrders_EveryOrderIsFlaggedAsNotArchived() {
        //ARRANGE
        int pageSize = 100;
        int pageNumber = 1;

        //ACT
        var response = await _httpClient.GetAsync("api/orders/dearchive?pageSize=" + pageSize + "&pageNumber=" + pageNumber);
        var apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync(), _settings);
        var models = JsonConvert.DeserializeObject<IEnumerable<ReadOrderModel>>(apiResponse.Content.ToString(), _settings);

        //ASSERT
        Assert.That(response != null && models != null);
        Assert.That(models.All(m => m.IsArchived == false));
    }

    [Test]
    public async Task GetOrder_GuidDoesntExist_Returns404() {
        //ARRANGE
        //ACT
        var response = await _httpClient.GetAsync("api/orders/" + Guid.NewGuid());
        var apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync(), _settings);

        //ASSERT
        Assert.That(apiResponse.IsSuccess == false);
        Assert.That(apiResponse.StatusCode == HttpStatusCode.NotFound);
    }

    [Test]
    public async Task GetOrder_GuidExists_ReturnsModel() {
        //ARRANGE
        var response = await _httpClient.GetAsync("api/orders/dearchive?pageSize=" + 1 + "&pageNumber=" + 1);
        var apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync(), _settings);
        var models = JsonConvert.DeserializeObject<IEnumerable<ReadOrderModel>>(apiResponse.Content.ToString(), _settings);

        //ACT
        response = await _httpClient.GetAsync("api/orders/" + models.First().Id);
        apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync(), _settings);
        var model = JsonConvert.DeserializeObject<ReadOrderModel>(apiResponse.Content.ToString(), _settings);

        //ASSERT
        Assert.That(response != null && model != null);
        Assert.That(model.Id == models.First().Id);
    }

    [Test]
    public async Task PostOrder_CorrectInputData_ShouldCreateOrder() {
        //ARRANGE
        OrderBuilder builder = new();

        Order order = builder
            .AddName("TEST")
            .AddEmail("TEST")
            .AddPhoneNumber("TEST")
            .AddDescription("TEST")
            .Build();
        
        StringContent content = new StringContent(JsonConvert.SerializeObject(order, _settings), Encoding.UTF8, "application/json");

        //ACT
        var response = await _httpClient.PostAsync("api/orders/", content);

        //ASSERT
        Assert.That(response != null);
        Assert.That(response.IsSuccessStatusCode == true);
    }

    [Test]
    public async Task PostOrder_WrongData_ShouldReturn400() {
        //ARRANGE
        OrderBuilder builder = new();

        Order order = builder
            .Build();

        StringContent content = new StringContent(JsonConvert.SerializeObject(order, _settings), Encoding.UTF8, "application/json");

        //ACT
        var response = await _httpClient.PostAsync("api/orders/", content);

        //ASSERT
        Assert.That(response != null);
        Assert.That(response.IsSuccessStatusCode == false);
    }
}