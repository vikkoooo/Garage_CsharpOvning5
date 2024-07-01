using GarageApp.Model.Vehicles;

namespace GarageApp.Interfaces
{
    internal interface IHandler
    {
        void Add(Vehicle v);
        IEnumerable<Vehicle> FilterVehicles(string vehicleType, Dictionary<string, string> filters);
        IEnumerable<Vehicle> FilterVehiclesTypeOnly(string vehicleType);
        void GenerateData(int size, double ratio = 0.5);
        IEnumerable<Vehicle> GetVehicles();
        bool IsTakenRegNumber(string regNumber);
        void Remove(string regNumber);
        Vehicle? Search(string regNumber);
        bool ValidColor(string color);
        bool ValidEngines(int engines);
        bool ValidFuel(string fuel);
        bool ValidLength(double length);
        bool ValidRegNumber(string regNumber);
        bool ValidSeats(int seats);
        bool ValidVolume(int volume);
        bool ValidWheels(int wheels);
    }
}