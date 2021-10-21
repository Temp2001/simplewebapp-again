using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shouldly;
using SimpleWebAppMVC.Controllers;
using SimpleWebAppMVC.Data;
using System;
using Xunit;

namespace SimpleWebAppMVC.Test
{
    public class TasksApiControllerTests
    {
        protected DbContextOptions<AppDbContext> Options;

        public TasksApiControllerTests() => Options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

        [Fact]
        public void Get_WithEmptyString_ShouldReturnNotFound()
        {
            string input = string.Empty;

            using AppDbContext dbContext = new AppDbContext(Options, isTest: true);

            TasksApiController sut = new TasksApiController(dbContext);

            IActionResult result = sut.Get(input);

            result.ShouldBeOfType<NotFoundResult>();
        }

        [Fact]
        public void Get_WithValidInput_ShouldInput()
        {
            string input = "1";

            using AppDbContext dbContext = new AppDbContext(Options, isTest: true);

            SetupContext(dbContext);

            TasksApiController sut = new TasksApiController(dbContext);

            var result = sut.Get(input) as JsonResult;
            var task = result.Value as Models.Task;

            task.Id.ShouldBe(input);
        }


        private static void SetupContext(AppDbContext context)
        {
            context.Tasks.Add(new Models.Task { Id = "1", Date = new DateTime(1970, 1, 1), Description = "Task description", Title = "Task title here", Status = "N/A" });
            context.Tasks.Add(new Models.Task { Id = "2", Date = new DateTime(1970, 1, 10), Description = "Second task description", Title = "Second rask title here", Status = "Started" });
            context.SaveChanges();
        }
    }
}
