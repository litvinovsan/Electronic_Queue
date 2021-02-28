using System;
using ElectronicQueue.Model.Tickets;
using ElectronicQueue.Model.Windows;
using ElectronicQueue.Model.Услуги;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ElectronicQueue.Model.Windows.Tests
{
    [TestClass()]
    public class WindowTests
    {
        [TestMethod()]
        public void GetNextFreeTimeTest()
        {
            // Arrange
            Window wnd = new Window("Окно1");
            Service srv = new Service("Услуга1", 5);
            Service srv2 = new Service("Услуга2", 15);

            // Action
            wnd.AddTicket(new Ticket(srv, 1, 5, DateTime.Now));
            wnd.AddTicket(new Ticket(srv2, 2, 15, DateTime.Now));

            // Assert
            var actual = wnd.GetNextFreeTime();
            var expected = DateTime.Now.AddMinutes(20).Minute;
            Assert.AreEqual(expected, actual.Minute);
        }
       
    }
}

namespace ElectronicQueueTests.Model.Windows
{
    [TestClass()]
    public class WindowTests
    {
        [TestMethod()]
        public void AddTicketTest()
        {
            // Arrange
            Window wnd = new Window("Окно1");
            Service srv = new Service("Услуга1", 5);
            // Action
            wnd.AddTicket(new Ticket(srv, 1, 5, DateTime.Now));
            // Assert
            var actual = wnd.GetQueueCount();
            var expected = 1;
            Assert.AreEqual(expected, actual);
        }
    }
}