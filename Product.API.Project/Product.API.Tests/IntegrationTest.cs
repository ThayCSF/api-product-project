using FluentAssertions;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Product.API.Tests
{
    public class IntegrationTest
    {
        [Fact(DisplayName = "Get nonexistent product")]
        public async Task Get_NonExistentProduct_ShouldReturnEmptyList()
        {
            // Arrange
            var productId = "e978f409-9955-497d-8f97-917dfc054b80";

            // Act
            var response = await Client.GetAsync($"/api/Timesheet?employeeId={productId}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact(DisplayName = "Get existent product")]
        public async Task Get_ExistentProduct_ShouldReturnListWithContent()
        {
            // Arrange
            var employeeId = "05a41567-b511-441b-b6aa-b74f41fb7a09";

            // Act
            var response = await Client.GetAsync($"/api/Timesheet?employeeId={employeeId}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact(DisplayName = "Add Invalid Product")]
        public async Task Add_InvalidProduct_ShouldReturnBadRequest()
        {
            // Arrange
            var timeEntry = new TimeEntry()
            {
                Date = DateTime.Now,
                Start = TimeSpan.FromHours(18),
                End = TimeSpan.FromHours(10),
                EmployeeId = Guid.Empty,
                ProjectId = Guid.Empty,
            };

            // Act
            var response = await Client.PostAsJsonAsync("/api/Timesheet", timeEntry);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact(DisplayName = "Add Valid Time Entry")]
        public async Task AddTimeEntry_ValidRequest_ShouldReturnOk()
        {
            // Arrange
            var timeEntry = new TimeEntry()
            {
                Date = DateTime.Now,
                Start = TimeSpan.FromHours(09),
                End = TimeSpan.FromHours(12),
                EmployeeId = Guid.Parse("996b58c5-b711-42e5-b422-cbfe8ee2af43"),
                ProjectId = Guid.Parse("d567a6ce-aff3-481b-a7d0-6e63bfc31eee"),
            };

            // Act
            var response = await Client.PostAsJsonAsync("/api/Timesheet", timeEntry);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Fact(DisplayName = "Delete Nonexistent Time Entry")]
        public async Task DeleteTimeEntry_NonExistent_ShouldReturnNotFound()
        {
            // Act
            var response = await Client.DeleteAsync($"/api/Timesheet?timeEntryId={Guid.NewGuid()}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}