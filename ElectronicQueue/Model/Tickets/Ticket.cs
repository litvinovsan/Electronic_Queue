using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElectronicQueue.Model.Services;
using ElectronicQueue.Model.Услуги;

namespace ElectronicQueue.Model.Tickets
{
    public class Ticket : ITicket
    {
        private static int _id = 0;

        public Ticket(IService service, int numberInQueue, int estimationTime, DateTime planedTime)
        {
            if (numberInQueue <= 0) throw new ArgumentOutOfRangeException(nameof(numberInQueue));
            if (estimationTime <= 0) throw new ArgumentOutOfRangeException(nameof(estimationTime));

            Id = _id++;
            Service = service ?? throw new ArgumentNullException(nameof(service));
            NumberInQueue = numberInQueue;
            EstimationTime = estimationTime;

            PlanedTime = planedTime;
        }

        public int Id { get; }
        public int NumberInQueue { get; }
        public int EstimationTime { get; }
        public IService Service { get; }
        public DateTime PlanedTime { get; }
        public DateTime ProcessStart { get; set; }
        public DateTime ProcessStop { get; set; }

        public override string ToString()
        {
            return $"Талон №:      {Id}\n\r" +
                   $"Вид услуги:   {Service.Name}\n\r" +
                   $"№ в очереди:  {NumberInQueue}\n\r" +
                   $"Время приема: {PlanedTime:HH:mm}\n\r";
        }
    }
}
