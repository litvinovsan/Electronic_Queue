using System;
using ElectronicQueue.Model.Services;

namespace ElectronicQueue.Model.Услуги
{
    /// <summary>
    /// Базовый класс Услуги.
    /// </summary>
    public class Service : IService, IEquatable<Service>
    {
        private static int _id = 0;
        public int Id { get; }
        public string Name { get; }
        public int PlannedMinutes { get; }

        public Service(string name, int minutes)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException(nameof(name));
            if (minutes <= 0) throw new ArgumentOutOfRangeException(nameof(minutes));

            Id = _id++;
            Name = name;
            PlannedMinutes = minutes;
        }

        public bool Equals(Service other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id == other.Id && string.Equals(Name, other.Name) && PlannedMinutes == other.PlannedMinutes;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Service) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Id;
                hashCode = (hashCode * 397) ^ (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ PlannedMinutes;
                return hashCode;
            }
        }

        public static bool operator ==(Service left, Service right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Service left, Service right)
        {
            return !Equals(left, right);
        }
    }
}
