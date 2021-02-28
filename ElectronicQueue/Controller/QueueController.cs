using System;
using System.Linq;
using ElectronicQueue.Model.Tickets;
using ElectronicQueue.Model.Windows;

namespace ElectronicQueue.Controller
{
    public class QueueController
    {
        private readonly DateTime _dayEndTime;
        private readonly IWindowsController _wndController;
        private readonly IServicesController _srvController;
        public QueueController(IWindowsController wndCtrl, IServicesController srvController)
        {
            _wndController = wndCtrl ?? throw new ArgumentNullException(nameof(wndCtrl));
            _srvController = srvController ?? throw new ArgumentNullException(nameof(srvController));

            _dayEndTime = DateTime.Now.Date + new TimeSpan(16, 00, 00);
        }
        public Ticket GetTicket(string serviceName)
        {
            // Запрашиваемая услуга
            var curentService = _srvController.Get(serviceName);
            // Окна в которых обслуживается данная услуга
            var wdws = _wndController.WndDictionary.Values.Where(x => x.AvailableServices.Contains(curentService));
            // Суммарная длинна очереди во всех окнах для данной услуги
            var windows = wdws as IWindow[] ?? wdws.ToArray();
            int currentQueueLen = windows.Sum(item => item.GetQueueCount(serviceName));
            // Окно с мин по времени очередью куда уйдет текущий талон
            var currentWnd = windows.Aggregate((a, b) => a.GetQueueTime() < b.GetQueueTime() ? a : b);
            // Плановое время приема
            DateTime plTime = currentWnd.GetNextFreeTime();

            // Если запланированное время позднее времени окончания рабочего дня
            if (plTime.CompareTo(_dayEndTime) > 0)
            {
                Console.WriteLine("Рабочий день окончен");
                return null;
            }

            // Время ожидания в минутах
            int estTime = currentWnd.GetQueueTime();

            Ticket ticket = new Ticket(curentService, ++currentQueueLen, estTime, plTime);

            _wndController.AddTicket(currentWnd.Name, ticket);

            return ticket;
        }

    }
}
