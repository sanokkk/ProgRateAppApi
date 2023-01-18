using System;
using Xunit;
using Microsoft.AspNetCore.TestHost;
using Microsoft.AspNetCore.Hosting;
using System.Net.Http;
using Antoher;
using System.Threading.Tasks;
using System.Net;

namespace Another.Test
{
    public class PostTest
    {
        private readonly HttpClient _client;
        public PostTest()
        {
            var server = new TestServer(new WebHostBuilder()
                .UseEnvironment("Development").UseStartup<Startup>());
            _client = server.CreateClient();
        }

        [Theory]
        [InlineData("GET")]
        public async Task GetPostsAsync(string method)
        {
            //Arrange
            var request = new HttpRequestMessage(new HttpMethod(method), "/api/post/selectall");

            //act
            var response = await _client.SendAsync(request);

            //Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
