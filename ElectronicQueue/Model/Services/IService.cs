namespace ElectronicQueue.Model.Services
{
    public interface IService
    {
        int Id { get; }
        string Name { get; }
        int PlannedMinutes { get; }
    }
}
