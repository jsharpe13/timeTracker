using timeTracker.Controllers;
using timeTracker.Models;
using timeTracker.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace XUnitTestProject1
{
    public class UnitTest1
    {
        [Fact]
        public void Test_unitOfWork()
        {
            var mockContext = new Mock<ApplicationDbContext>();
            var unitOfWork = new EventUnitOfWork(mockContext.Object);

            unitOfWork.Save();

            mockContext.Verify(x => x.SaveChanges());
        }
        [Fact]
        public void Test_TimeController()
        {
            var mockContext = new Mock<ApplicationDbContext>();
            var unitOfWork = new EventUnitOfWork(mockContext.Object);
            var log = new NullLogger<TimeController>();
            var output = new TimeController(unitOfWork, log);

            Assert.NotNull(output.ViewBag);
        }
        [Fact]
        public void Test_index()
        {
            var mockContext = new Mock<ApplicationDbContext>();
            var unitOfWork = new EventUnitOfWork(mockContext.Object);
            var log = new NullLogger<TimeController>();
            var output = new TimeController(unitOfWork, log);

            var result = output.timeIndex();

            Assert.IsType<ViewResult>(result);
        }
        [Fact]
        public void Test_filter()
        {
            var mockContext = new Mock<ApplicationDbContext>();
            var unitOfWork = new EventUnitOfWork(mockContext.Object);
            var log = new NullLogger<TimeController>();
            var output = new TimeController(unitOfWork, log);

            var result = output.filter(6, 2021);

            Assert.IsType<RedirectToActionResult>(result);
        }
        [Fact]
        public void Test_Add()
        {
            var mockContext = new Mock<ApplicationDbContext>();
            var unitOfWork = new EventUnitOfWork(mockContext.Object);
            var log = new NullLogger<TimeController>();
            var output = new TimeController(unitOfWork, log);

            var result = output.AddEvent();

            Assert.IsType<ViewResult>(result);
        }
        [Fact]
        public void Test_Delete()
        {
            var mockContext = new Mock<ApplicationDbContext>();
            var unitOfWork = new EventUnitOfWork(mockContext.Object);
            var log = new NullLogger<TimeController>();
            var output = new TimeController(unitOfWork, log);
            Event ev = new Event();

            var result = output.Delete(1);

            Assert.IsType<RedirectToActionResult>(result);
        }
        [Fact]
        public void Test_new_Event()
        {
            var ev = new Event();
            ev.EventId = 99;
            ev.Name = "unit test test";
            ev.Description = "testing the unit test";
            ev.start = new DateTime(2021, 1, 15, 8, 0, 0);
            ev.end = new DateTime(2021, 1, 15, 9, 0, 0);

            var eve = ev;

            Assert.Equal(99, eve.EventId);
            Assert.Equal("unit test test", eve.Name);
            Assert.Equal("testing the unit test", eve.Description);
            Assert.Equal(new DateTime(2021, 1, 15, 8, 0, 0), eve.start);
            Assert.Equal(new DateTime(2021, 1, 15, 9, 0, 0), eve.end);
        }
        [Fact]
        public void Test_isToday()
        {
            var ev = new Event();
            ev.start = DateTime.Now.Date;

            Assert.True(ev.isToday);
        }
        [Fact]
        public void Test_isPast()
        {
            var ev = new Event();
            ev.start = new DateTime(2019, 1, 1);

            Assert.True(ev.isPast);
        }
    }
}
