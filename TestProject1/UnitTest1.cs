using System.Net;
using System.Text;
using intern_api.Controllers;
using intern_api.Models;
using FakeItEasy;
using intern_api;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Moq;
using Newtonsoft.Json;
using Xunit;

namespace TestProject1;

public class UnitTest1
{
    [Fact]
    public async Task Post_ReturnsSuccessStatusCode()
    {
        var data = new InternDto { Username = "test", Email = "test.com", Description = "testestestes", Image = "yoo" };
        var mockDbContext = new Mock<AppDbContext>();
        //var mockSet = Mock<DbSet<Intern>>;
        //mockDbContext.Setup(x => x.interns).Returns(data);
    }
}