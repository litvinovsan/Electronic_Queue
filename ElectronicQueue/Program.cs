using System;
using System.Collections.Generic;
using ElectronicQueue.Controller;
using ElectronicQueue.Model.Tickets;

namespace ElectronicQueue
{

    class Program
    {
        static void Main(string[] args)
        {
            #region Инициализация

            // Cоздаём услуги
            ServicesController servicesCntr = new ServicesController();

            servicesCntr.Add(Services.Name1, 5);
            servicesCntr.Add(Services.Name2, 7);
            servicesCntr.Add(Services.Name3, 10);
            servicesCntr.Add(Services.Name4, 15);

            // Создаём окна
            WindowsController windowsCntr = new WindowsController();
            windowsCntr.Add(Windows.Num1);
            windowsCntr.Add(Windows.Num2);
            windowsCntr.Add(Windows.Num3);

            // Устанавливаем доступные услуги для Окна 1. Окно должно знать какие услуги оно обслуживает
            windowsCntr.AddServices(Windows.Num1, new[] { servicesCntr.Get(Services.Name1), servicesCntr.Get(Services.Name2) });

            // Устанавливаем доступные услуги для Окна 2
            windowsCntr.AddServices(Windows.Num2, new[] { servicesCntr.Get(Services.Name1), servicesCntr.Get(Services.Name2), servicesCntr.Get(Services.Name3) });

            // Устанавливаем доступные услуги для Окна 3
            windowsCntr.AddServices(Windows.Num3, new[] { servicesCntr.Get(Services.Name4) });

            #endregion

            QueueController controller = new QueueController(windowsCntr, servicesCntr);

            List<Ticket> tickets = new List<Ticket>();
            for (int i = 0; i < 10; i++)
            {
                tickets.Add(controller.GetTicket(Services.Name1));
                tickets.Add(controller.GetTicket(Services.Name3));
                tickets.Add(controller.GetTicket(Services.Name2));
                tickets.Add(controller.GetTicket(Services.Name4));
                tickets.Add(controller.GetTicket(Services.Name1));
                tickets.Add(controller.GetTicket(Services.Name1));
                tickets.Add(controller.GetTicket(Services.Name1));
            }
            Console.WriteLine("\r\r");

            foreach (var item in tickets)
            {
                Console.WriteLine(item.ToString());
                Console.WriteLine(" ");
            }

            // Внутренняя очередь в окнах заполняется равномерно.
            Console.WriteLine($"{Windows.Num1}. Кол-во талонов:{windowsCntr[Windows.Num1].GetQueueCount()}. Суммарное время: {windowsCntr[Windows.Num1].GetQueueTime()} минут");
            Console.WriteLine($"{Windows.Num2}. Кол-во талонов:{windowsCntr[Windows.Num2].GetQueueCount()}. Суммарное время: {windowsCntr[Windows.Num2].GetQueueTime()} минут");
            Console.WriteLine($"{Windows.Num3}. Кол-во талонов:{windowsCntr[Windows.Num3].GetQueueCount()}. Суммарное время: {windowsCntr[Windows.Num3].GetQueueTime()} минут");
            Console.WriteLine();

            // Метод NextTicket берет следующий Талон из очереди
            Console.WriteLine("После обработки 3х талонов первым окном:");
            windowsCntr[Windows.Num1].NextTicket();
            windowsCntr[Windows.Num1].NextTicket();
            windowsCntr[Windows.Num1].NextTicket();

            Console.WriteLine();
            Console.WriteLine($"{Windows.Num1}. Кол-во талонов:{windowsCntr[Windows.Num1].GetQueueCount()}. Суммарное время: {windowsCntr[Windows.Num1].GetQueueTime()}");
            Console.WriteLine($"{Windows.Num1}, история. Кол-во талонов:{windowsCntr[Windows.Num1].TicketsHistory.Count}.");

            Console.ReadLine();

        }
    }

    /// <summary>
    /// Названия существующих окон
    /// </summary>
    public struct Windows
    {
        public const string Num1 = "Окно 1";
        public const string Num2 = "Окно 2";
        public const string Num3 = "Окно 3";
    }

    /// <summary>
    /// Наименования существующих Услуг
    /// </summary>
    public struct Services
    {
        public const string Name1 = "Услуга 1";
        public const string Name2 = "Услуга 2";
        public const string Name3 = "Услуга 3";
        public const string Name4 = "Услуга 4";
    }
}
