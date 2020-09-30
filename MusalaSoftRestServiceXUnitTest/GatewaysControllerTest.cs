using Xunit;
using System.Net;
using Newtonsoft.Json;
using System.Net.Http;
using FluentAssertions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using MusalaSoftRestServiceTest.Models;

namespace MusalaSoftRestServiceXUnitTest
{
    public class GatewaysControllerTest : IClassFixture<WebApplicationFactory<MusalaSoftRestServiceTest.Startup>>
    {
        private readonly HttpClient client;

        private HttpClient GetClient()
        {
            return client;
        }

        public GatewaysControllerTest(WebApplicationFactory<MusalaSoftRestServiceTest.Startup> fixture)
        {
            client = fixture.CreateClient();
        }

        [Fact]
        public async Task Get_Should_Retrieve_Periphericals()
        {
            var response = await GetClient().GetAsync("api/Gateways");
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var gateways = JsonConvert.DeserializeObject<Gateway[]>(await response.Content.ReadAsStringAsync());
            foreach (var gateway in gateways)
            {
                gateway.Peripherals.Should().HaveCountLessOrEqualTo(10);
            }
        }
    }
}
