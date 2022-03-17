using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Product.API.Project.Entities;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

namespace Product.API.Tests
{
    public class TestingProductsApplication

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
        public async Task Get_AllProduct_ShouldReturnListWithContent()
        {
            // Arrange
            var pageNumber = 1;
            var pageSize = 2;

            // Act
            var response = await _client.GetAsync($"/api/products?pageNumber={pageNumber}pageSize={pageSize}"); 

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact(DisplayName = "Get nonexistent product")]
        public async Task Get_NonExistentProduct_ShouldReturnEmptyList()
        {
            // Arrange
            var productId = "e978f409-9955-497d-8f97-917dfc054b80";

            // Act
            var response = await _client.GetAsync($"/api/products?id={productId}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact(DisplayName = "Get existent product")]
        public async Task Get_ExistentProduct_ShouldReturnContent()
        {
            // Arrange
            var productId = "0f2130b5-025f-49c1-828e-d6c3f4da4dbc";

            // Act
            var response = await _client.GetAsync($"/api/products?id={productId}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact(DisplayName = "Update Valid Product")]
        public async Task UpdateValidProduct_ShouldReturnOk()
        {
            // Arrange  
            var id = "85a3ad21-46f2-4dc6-be04-0245beb6299d";
            var product = new Products ()
            {   
                Id = Guid.Parse(id),
                Name = "Updated",
                UnitValue = 19,
                Seller = "Success"
            };

            // Act
            var response = await _client.PutAsJsonAsync($"/api/products?id={id}", product);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact(DisplayName = "Update Invalid Product")]
        public async Task UpdateInvalidProduct_ShouldReturnNotFound()
        {
            // Arrange
            var id = Guid.NewGuid;
            var product = new Products()
            {
                Name = "Updated",
                UnitValue = 20,
                Seller = "Success"
            };

            // Act
            var response = await _client.PutAsJsonAsync($"/api/products?id={id}", product);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact(DisplayName = "Add Invalid Product")]
        public async Task Add_InvalidProduct_ShouldReturnBadRequest()
        {
            // Arrange
            var product = new Products()
            {
                Id = Guid.Empty,
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
                Name = "Tambor",
                UnitValue = 29,
                Seller = "Assa-Bloy"
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
            var id = Guid.NewGuid;

            // Act
            var response = await _client.DeleteAsync($"/api/products/{id}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact(DisplayName = "Delete Existent Product")]
        public async Task Delete_ExistentProduct_ShouldReturnOk()
        {
            // Arrange
            var productId = "dfc89fda-d579-448f-82d5-422b6b103c29";

            // Act
            var response = await _client.DeleteAsync($"/api/products/productId={productId}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

    }
}