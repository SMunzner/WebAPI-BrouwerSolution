using BrouwerService.Repositories;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using Moq;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using BrouwerService.Models;
using System.Text.Json;
using BrouwerService.Controllers;

namespace BrouwerServiceIntegrationTests
{
    [TestClass]
    public class BrouwerControllerTest
    {
        HttpClient client = null!;

        //deel met mock
        Mock<IBrouwerRepository> mock = null!;
        Brouwer brouwer1 = null!;

        [TestInitialize]
        public void TestInitialize()
        {
            mock = new Mock<IBrouwerRepository>();
            var repository = mock.Object;
            var factory = new WebApplicationFactory<BrouwerController>();
            client = factory.WithWebHostBuilder(builder =>
                builder.ConfigureTestServices(services =>
                services.AddScoped<IBrouwerRepository>(_ => repository)))
                .CreateClient();
            brouwer1 = new Brouwer { Id = 1, Naam = "1", Postcode = 1000, Gemeente = "1" };
        }


        [TestMethod]
        public void DeleteMetBestaandeBrouwerGeeftOK()
        {
            mock.Setup(repo => repo.FindByIdAsync(1))
                .Returns(Task.FromResult((Brouwer?)brouwer1));
            var response = client.DeleteAsync("brouwers/1").Result;
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            mock.Verify(repo => repo.DeleteAsync(brouwer1));
        }


        [TestMethod]
        public void DeleteMetOnbestaandeBrouwerGeeftNotFound()
        {
            var response = client.DeleteAsync("brouwers/-1").Result;
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
            mock.Verify(repo => repo.DeleteAsync(It.IsAny<Brouwer>()), Times.Never);
        }

        [TestMethod]
        public void GetMetOnbestaandeBrouwerGeeftNotFound()
        {
            var response = client.GetAsync("brouwers/-1").Result;
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
            mock.Verify(repo => repo.FindByIdAsync(-1));
        }

        [TestMethod]
        public void GetMetBestaandeBrouwerGeeftOK()
        {
            //train mock
            mock.Setup(repo => repo.FindByIdAsync(1))
                .Returns(Task.FromResult<Brouwer?>(brouwer1));

            //result URL get 
            var response = client.GetAsync("brouwers/1").Result;

            //are status codes same
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            //check if method has been called
            mock.Verify(repo => repo.FindByIdAsync(1));

            
            
            //lees body van response als string
            var body = response.Content.ReadAsStringAsync().Result;

            //controleren of de JSON data properties de juiste waarden hebben en ze overeenkomen
            var document = JsonDocument.Parse(body);

            //lees waarde id property als int --> is 1?
            Assert.AreEqual(1, document.RootElement.GetProperty("id").GetInt32());

            //lees waarde property naam --> is "1"?
            Assert.AreEqual("1", document.RootElement.GetProperty("naam").GetString());
        }


        [TestMethod]
        public void PostMetCorrecteBrouwerGeeftCreated()
        {
            //result post met brouwers/brouwer1
            var response = client.PostAsJsonAsync("brouwers", brouwer1).Result;

            //are status codes same
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);

            //check als Insert utigevoerd is, geef object mee en kijk of Id 1 is en naam 1 is
            mock.Verify(repo => repo.InsertAsync(
                It.Is<Brouwer>(brouwer => brouwer.Id == 1))); 
            mock.Verify(repo => repo.InsertAsync(
                It.Is<Brouwer>(brouwer => brouwer.Naam == "1")));
        }


        [TestMethod]
        public void PostMetVerkeerdeBrouwerGeeftBadRequest()
        {
            //geeft foute postcode mee
            var response = client.PostAsJsonAsync("brouwers",
                new Brouwer() { Id = 1, Naam = "1", Postcode = -1, Gemeente = "1" }).Result;
            
            //status moet badrequest zijn
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            
            //check als een brouwer is toegevoegd --> Insert uitvoeren mag niet, daarom Times.Never
            mock.Verify(repo => repo.InsertAsync(It.IsAny<Brouwer>()), Times.Never);
        }


    }
}