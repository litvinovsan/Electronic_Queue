using System;
using ElectronicQueue.Model.Services;
using ElectronicQueue.Model.Услуги;

namespace ElectronicQueue.Model.Tickets
{
    /// <summary>
    /// Описание Талончика
    /// </summary>
    public interface ITicket
    {
        int Id { get; }
        int NumberInQueue { get; }
        int EstimationTime { get; }
        IService Service { get; }
        DateTime PlanedTime { get; }
        DateTime ProcessStart { get; set; }
        DateTime ProcessStop { get; set; }
    }
}
