using System;
using System.Collections.Generic;
using System.Linq;
using ElectronicQueue.Model.Services;
using ElectronicQueue.Model.Tickets;

namespace ElectronicQueue.Model.Windows
{
    public class Window : IWindow
    {
        private static int _id;

        public List<IService> AvailableServices { get; }
        public List<ITicket> TicketsHistory { get; }
        public Queue<ITicket> TicketsQueue { get; }

        public int Id { get; }
        public string Name { get; }

        public Window(string name)
        {
            Id = ++_id;
            Name = name;
            AvailableServices = new List<IService>();
            TicketsQueue = new Queue<ITicket>();
            TicketsHistory = new List<ITicket>();
        }

        public void AddTicket(ITicket ticket)
        {
            if (ticket == null) throw new ArgumentNullException(nameof(ticket));

            var curentTicket = ticket;
            curentTicket.ProcessStart = DateTime.Now;
            TicketsQueue.Enqueue(curentTicket);
        }

        /// <summary>
        /// Список Услуг, доступных в данном окне
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        public void AddService(IService[] service)
        {
            if (service == null) { return; }

            foreach (var item in service)
            {
                AvailableServices.Add(item);
            }
        }

        public int GetQueueCount()
        {
            return TicketsQueue.Count;
        }
        public int GetQueueCount(string serviceName)
        {
            return TicketsQueue.Count(x => x.Service.Name.Equals(serviceName));
        }

        /// <summary>
        /// Возвращает Планируемое время приема
        /// </summary>
        /// <returns></returns>
        public DateTime GetNextFreeTime()
        {
            var totalQueueTime = GeetQueueTime();
            var futureServiceTime = DateTime.Now.AddMinutes(totalQueueTime);
            return futureServiceTime;
        }

        /// <summary>
        /// Извлекает из локальной очереди Окна талон, сохраняет его в лог окна.
        /// </summary>
        /// <returns></returns>
        public ITicket NextTicket()
        {
            var curentTicket = TicketsQueue.Dequeue();
            curentTicket.ProcessStop = DateTime.Now;
            TicketsHistory.Add(curentTicket);
            return curentTicket;
        }

        /// <summary>
        /// Возвращает ожидаемую длительность очереди в текущем окне в минутах
        /// </summary>
        /// <returns></returns>
        public int GeetQueueTime()
        {
            var result = TicketsQueue.Sum(x => x.Service.PlannedMinutes);
            return result;
        }
    }
}
