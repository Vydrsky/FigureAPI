using Figure.Application.Models;
using Figure.DataAccess.Entities;
using Figure.Infrastructure;
using Figure.Tests.Builders;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Net;
using System.Text;

namespace Figure.Tests;

public class OrdersControllerTests {

    private HttpClient _httpClient;
    private JsonSerializerSettings _settings;

    private readonly string apiPath = "api/orders";

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
        int pageSize = 5;
        int pageNumber = 1;
        var response = await _httpClient.GetAsync(apiPath +"?pageSize=" + pageSize + "&pageNumber=" + pageNumber);


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
        var response = await _httpClient.GetAsync(apiPath + "/archive?pageSize=" + pageSize + "&pageNumber=" + pageNumber);
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
        var response = await _httpClient.GetAsync(apiPath + "/dearchive?pageSize=" + pageSize + "&pageNumber=" + pageNumber);
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
        var response = await _httpClient.GetAsync(apiPath + "/" + Guid.NewGuid());
        var apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync(), _settings);

        //ASSERT
        Assert.That(apiResponse.IsSuccess == false);
        Assert.That(apiResponse.StatusCode == HttpStatusCode.NotFound);
    }

    [Test]
    public async Task GetOrder_GuidExists_ReturnsModel() {
        //ARRANGE
        var response = await _httpClient.GetAsync(apiPath + "/dearchive?pageSize=" + 1 + "&pageNumber=" + 1);
        var apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync(), _settings);
        var models = JsonConvert.DeserializeObject<IEnumerable<ReadOrderModel>>(apiResponse.Content.ToString(), _settings);

        //ACT
        response = await _httpClient.GetAsync(apiPath + "/" + models.First().Id);
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
            .AddSurname("TEST")
            .AddEmail("TEST")
            .AddPhoneNumber("TEST")
            .AddDescription("TEST")
            .Build();
        
        StringContent content = new StringContent(JsonConvert.SerializeObject(order, _settings), Encoding.UTF8, "application/json");

        //ACT
        var response = await _httpClient.PostAsync(apiPath + "/", content);

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
        var response = await _httpClient.PostAsync(apiPath + "/", content);

        //ASSERT
        Assert.That(response != null);
        Assert.That(response.IsSuccessStatusCode == false);
    }

    [Test]
    public async Task UpdateOrder_CorrectUpdateData_ShouldReturnSuccess() {
        //ARRANGE
        var response = await _httpClient.GetAsync(apiPath + "?pageSize=" + 1 + "&pageNumber=" + 1);
        var apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync(), _settings);
        var models = JsonConvert.DeserializeObject<IEnumerable<ReadOrderModel>>(apiResponse.Content.ToString(), _settings);

        OrderBuilder builder = new();

        Order order = builder
            .AddId(models.FirstOrDefault().Id)
            .AddName("TEST")
            .AddSurname("TEST")
            .AddEmail("TEST")
            .AddPhoneNumber("TEST")
            .AddDescription("TEST")
            .Build();

        StringContent content = new StringContent(JsonConvert.SerializeObject(order, _settings), Encoding.UTF8, "application/json");
        //ACT
        response = await _httpClient.PutAsync(apiPath + "/", content);

        //ASSERT
        Assert.That(response != null);
        Assert.That(response.IsSuccessStatusCode == true);
    }

    [Test]
    public async Task UpdateOrder_EntityToUpdateDoesntExist_ShouldFail() {
        //ARRANGE
        OrderBuilder builder = new();

        Order order = builder
            .AddId(Guid.NewGuid())
            .AddName("TEST")
            .AddSurname("TEST")
            .AddEmail("TEST")
            .AddPhoneNumber("TEST")
            .AddDescription("TEST")
            .Build();

        StringContent content = new StringContent(JsonConvert.SerializeObject(order, _settings), Encoding.UTF8, "application/json");
        //ACT
        var response = await _httpClient.PutAsync(apiPath + "/", content);
        var apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync(), _settings);

        //ASSERT
        Assert.That(response != null);
        Assert.That(apiResponse.StatusCode == HttpStatusCode.NotFound);
    }

    [Test]
    public async Task PatchOrder_PatchDocumentIsCorrect_ShouldReturnSuccess() {
        //ARRANGE
        var response = await _httpClient.GetAsync(apiPath + "?pageSize=" + 1 + "&pageNumber=" + 1);
        var apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync(), _settings);
        var models = JsonConvert.DeserializeObject<IEnumerable<ReadOrderModel>>(apiResponse.Content.ToString(), _settings);

        JsonPatchDocument<Order> patchDocument = new JsonPatchDocument<Order>();
        patchDocument.Replace(e => e.Name,"PATCH TEST");

        StringContent content = new StringContent(JsonConvert.SerializeObject(patchDocument), Encoding.UTF8, "application/json-patch+json");
        
        //ACT
        response = await _httpClient.PatchAsync(apiPath + "/" + models.First().Id, content);
        apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync(), _settings);

        //ASSERT
        Assert.That(response != null);
        Assert.That(apiResponse.StatusCode == HttpStatusCode.NoContent);
    }

    [Test]
    public async Task PatchOrder_PatchDocumentIsEmpty_ShouldReturnBadRequest() {
        //ARRANGE
        var response = await _httpClient.GetAsync(apiPath + "?pageSize=" + 1 + "&pageNumber=" + 1);
        var apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync(), _settings);
        var models = JsonConvert.DeserializeObject<IEnumerable<ReadOrderModel>>(apiResponse.Content.ToString(), _settings);

        StringContent content = new StringContent(JsonConvert.SerializeObject(new JsonPatchDocument<Order>()), Encoding.UTF8, "application/json-patch+json");

        //ACT
        response = await _httpClient.PatchAsync(apiPath + "/" + models.First().Id, content);
        apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync(), _settings);

        //ASSERT
        Assert.That(response != null);
        Assert.That(apiResponse.StatusCode == HttpStatusCode.BadRequest);
    }

    [Test]
    public async Task ArchiveOrder_OrderExists_ShouldReturnSuccess() {
        //ARRANGE
        var response = await _httpClient.GetAsync(apiPath + "?pageSize=" + 1 + "&pageNumber=" + 1);
        var apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync(), _settings);
        var models = JsonConvert.DeserializeObject<IEnumerable<ReadOrderModel>>(apiResponse.Content.ToString(), _settings);

        //ACT
        response = await _httpClient.PatchAsync(apiPath + "/archive/" + models.First().Id, null);
        apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync(), _settings);

        //ASSERT
        Assert.That(response != null);
        Assert.That(apiResponse.StatusCode == HttpStatusCode.OK);
    }

    [Test]
    public async Task ArchiveOrder_OrderDoesntExist_ShouldReturnNotFound() {
        //ARRANGE

        //ACT
        var response = await _httpClient.PatchAsync(apiPath + "/archive/" + Guid.NewGuid(), null);
        var apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync(), _settings);

        //ASSERT
        Assert.That(response != null);
        Assert.That(apiResponse.StatusCode == HttpStatusCode.NotFound);
    }

    [Test]
    public async Task DeArchiveOrder_OrderExists_ShouldReturnSuccess() {
        //ARRANGE
        var response = await _httpClient.GetAsync(apiPath + "?pageSize=" + 1 + "&pageNumber=" + 1);
        var apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync(), _settings);
        var models = JsonConvert.DeserializeObject<IEnumerable<ReadOrderModel>>(apiResponse.Content.ToString(), _settings);

        //ACT
        response = await _httpClient.PatchAsync(apiPath + "/dearchive/" + models.First().Id, null);
        apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync(), _settings);

        //ASSERT
        Assert.That(response != null);
        Assert.That(apiResponse.StatusCode == HttpStatusCode.OK);
    }

    [Test]
    public async Task DeArchiveOrder_OrderDoesntExist_ShouldReturnNotFound() {
        //ARRANGE

        //ACT
        var response = await _httpClient.PatchAsync(apiPath + "/dearchive/" + Guid.NewGuid(), null);
        var apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync(), _settings);

        //ASSERT
        Assert.That(response != null);
        Assert.That(apiResponse.StatusCode == HttpStatusCode.NotFound);
    }

    [Test]
    public async Task DeleteOrder_CorrectlyDeletesOrder() {
        //ARRANGE
        OrderBuilder builder = new OrderBuilder();

        Order order = builder
            .AddName("TEST")
            .AddSurname("TEST")
            .AddEmail("TEST")
            .AddPhoneNumber("TEST")
            .AddDescription("TEST")
            .Build();

        StringContent content = new StringContent(JsonConvert.SerializeObject(order, _settings), Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync(apiPath + "/", content);
        var apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync(), _settings);
        var idString = apiResponse.Content;

        response = await _httpClient.GetAsync(apiPath + "/" + idString);
        apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync(), _settings);
        var model = JsonConvert.DeserializeObject<ReadOrderModel>(apiResponse.Content.ToString(), _settings);

        //ACT
        response = await _httpClient.DeleteAsync(apiPath + "/" + model.Id);
        apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync(), _settings);

        //ASSERT
        Assert.That(response != null);
        Assert.That(apiResponse.StatusCode == HttpStatusCode.NoContent);
    }
}