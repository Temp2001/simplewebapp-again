using SimpleWebAppMVC.Helpers;
using SimpleWebAppMVC.Test.Setup;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using NSubstitute;
using Shouldly;
using System.Net;
using SimpleWebAppMVC.Models;

namespace SimpleWebAppMVC.Test.Controllers
{
    public class TasksApiControllerIntegrationTests : TestFixture
    {
        public TasksApiControllerIntegrationTests(CustomWebApplicationFactory factory) : base(factory)
        {
        }

        [Fact]
        public async void Post_WithValidInput_ShouldReturn201()
        {
            var response = await Client.PostAsync("api/task", HttpContentHelper.GetJsonContent(
                new Task
                {
                    Id = "e62ce113-14e3-4d22-ad91-5d60138faf7d",
                    Date = new DateTime(),
                    Description = "MyDesc",
                    Title = "MuchTitle",
                    Status = "N/A"
                }
                ));

            response.StatusCode.ShouldBe(HttpStatusCode.Created);
        }

    }
}
