using System;
using System.Collections.Generic;
using ElectronicQueue.Model.Services;
using ElectronicQueue.Model.Tickets;

namespace ElectronicQueue.Model.Windows
{
    /// <summary>
    /// Описание характеристик Окна
    /// </summary>
    public interface IWindow
    {
        List<IService> AvailableServices { get; }
        List<ITicket> TicketsHistory { get; }
        Queue<ITicket> TicketsQueue { get; }
        int Id { get; }
        string Name { get; }
        void AddTicket(ITicket ticket);
        void AddService(IService[] service);
        int GetQueueCount();
        int GetQueueCount(string serviceName);
        int GeetQueueTime();
        DateTime GetNextFreeTime();
        ITicket NextTicket();
    }
}
