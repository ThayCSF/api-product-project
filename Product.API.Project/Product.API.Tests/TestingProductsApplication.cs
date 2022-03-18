using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Product.API.Project.Entities;
using Product.API.Project.Shared;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Product.API.Tests
{
    public class TestingProductsApplication : IClassFixture<TestingProductsFactory>

    {
        private readonly HttpClient _client;
        private readonly TestingProductsFactory _factory;

        public TestingProductsApplication(
            TestingProductsFactory factory)
        {
            _factory = factory;
            _client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        }

        [Fact(DisplayName = "Get all products")]
        public async Task Get_AllProducts_ShouldReturnListWithContent()
        {
            // Arrange
            var pageParam = new PageParameters()
            {
                PageNumber = 1, 
                PageSize = 5 
            };

            var jsonSerialized = JsonConvert.SerializeObject(pageParam);

            var httpContent = new StringContent(jsonSerialized, Encoding.UTF8, "application/json");

            var request = new HttpRequestMessage(HttpMethod.Get, "/api/products") {Content = httpContent};

            // Act
            var response = await _client.SendAsync(request); 

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact(DisplayName = "Get nonexistent product")]
        public async Task Get_NonExistentProduct_ShouldReturnEmptyList()
        {
            // Arrange
            var productId = Guid.Parse("e978f409-9955-497d-8f97-917dfc054b80");

            // Act
            var response = await _client.GetAsync($"/api/products/{productId}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact(DisplayName = "Get existent product")]
        public async Task Get_ExistentProduct_ShouldReturnContent()
        {
            // Arrange
            var productId = Guid.Parse("0f2130b5-025f-49c1-828e-d6c3f4da4dbc");

            // Act
            var response = await _client.GetAsync($"/api/products/{productId}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact(DisplayName = "Update Valid Product")]
        public async Task UpdateValidProduct_ShouldReturnOk()
        {
            // Arrange  
            var id = Guid.Parse("3cdc11db-f582-435e-3800-08da08df494e");
            var product = new Products ()
            {   
                Id = Guid.Parse("3cdc11db-f582-435e-3800-08da08df494e"),
                Name = "Updated",
                UnitValue = 19,
                Seller = "Success"
            };

            // Act
            var response = await _client.PutAsJsonAsync($"/api/products/{id}", product);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact(DisplayName = "Update Invalid Product")]
        public async Task UpdateInvalidProduct_ShouldReturnNotFound()
        {
            // Arrange
            var id = Guid.Parse("e978f409-9955-497d-8f97-917dfc054b80");
            var product = new Products()
            {
                Id= Guid.Parse("e978f409-9955-497d-8f97-917dfc054b80"),
                Name = "Invalid",
                UnitValue = 20,
                Seller = "Error"
            };

            // Act
            var response = await _client.PutAsJsonAsync($"/api/products/{id}", product);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact(DisplayName = "Add Invalid Product")]
        public async Task Add_InvalidProduct_ShouldReturnBadRequest()
        {
            // Arrange
            var product = new Products()
            {
                Name = "",
                UnitValue = 0,
                Seller = ""               
            };

            // Act
            var response = await _client.PostAsJsonAsync("/api/products", product);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact(DisplayName = "Add Valid Product")]
        public async Task AddValidProduct_ShouldReturnOk()
        {
            // Arrange
            var product = new Products()
            {
                Name = "New Product",
                UnitValue = 55,
                Seller = "Successfully added"
            };

            // Act
            var response = await _client.PostAsJsonAsync("/api/products", product);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Fact(DisplayName = "Delete Nonexistent Product")]
        public async Task Delete_NonExistentProduct_ShouldReturnNotFound()
        {
            // Arrange
            var id = Guid.Empty;

            // Act
            var response = await _client.DeleteAsync($"/api/products/{id}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact(DisplayName = "Delete Existent Product")]
        public async Task Delete_ExistentProduct_ShouldReturnOk()
        {
            // Arrange
            var productId = Guid.Parse("44c8bbf7-f606-4196-97bb-7643efdba377");

            // Act
            var response = await _client.DeleteAsync($"/api/products/{productId}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

    }
}