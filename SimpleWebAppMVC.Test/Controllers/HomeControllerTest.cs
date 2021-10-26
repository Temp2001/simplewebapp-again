using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NSubstitute;
using Shouldly;
using SimpleWebAppMVC.Controllers;
using SimpleWebAppMVC.Data;
using SimpleWebAppMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

// sut = System Under Test

namespace SimpleWebAppMVC.Test
{
    public class HomeControllerTest
    {
        protected DbContextOptions<AppDbContext> Options;

        public HomeControllerTest() => Options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

        [Fact]
        public void SocialMedia_WithInputFour_ShouldReturnFourSocialMedia()
        {
            int feedCount = 4;

            var configuration = Substitute.For<IConfiguration>();

            var socialService = Substitute.For<ISocialService>();

            socialService.GetSocialData().ReturnsForAnyArgs(new List<SocialData>()
            {
                new SocialData(), new SocialData(), new SocialData(), new SocialData()
            });

            var sut = new HomeController(socialService, configuration); 

            var result = sut.SocialMedia(feedCount) as ViewResult;


            var socialFeed = result.ViewData["social-data"] as List<SocialData>;

            socialFeed.Count().ShouldBe(feedCount);
        }

        [Fact]
        public void About_NoInput_ShouldReturnCorrectAppName()
        {
            string expected = "MyAppName";

            var configuration = Substitute.For<IConfiguration>();

            var socialService = Substitute.For<ISocialService>();

            var sut = new HomeController(socialService, configuration);

            var result = sut.About();

            result.AppName.ShouldBe(expected);

        }

        private static void SetupContext(AppDbContext context)
        {
            context.Tasks.Add(new Models.Task { Id = "1", Date = new DateTime(1970, 1, 1), Description = "Task description", Title = "Task title here", Status = "N/A" });
            context.Tasks.Add(new Models.Task { Id = "2", Date = new DateTime(1970, 1, 10), Description = "Second task description", Title = "Second rask title here", Status = "Started" });
            context.SaveChanges();
        }
    }
}
