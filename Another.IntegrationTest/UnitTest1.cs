using Antoher;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using NUnit.Framework;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Another.IntegrationTest
{
    public class Tests
    {
        private readonly HttpClient _client;
        public Tests()
        {
            var server = new TestServer(new WebHostBuilder()
                .UseEnvironment("Development").UseStartup<Startup>());
            _client = server.CreateClient();
        }

        [Xunit.Theory]
        [InlineData("GET")]
        public async Task GetPostsAsync(string method)
        {
            //Arrange
            var request = new HttpRequestMessage(new HttpMethod(method), "/api/post/selectall");

            //act
            var response = await _client.SendAsync(request);

            //Assert
            response.EnsureSuccessStatusCode();
            Assert.Equals(HttpStatusCode.OK, response.StatusCode);
        }
    }
}