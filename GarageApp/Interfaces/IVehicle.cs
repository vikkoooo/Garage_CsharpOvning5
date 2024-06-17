namespace GarageApp.Interfaces
{
	internal interface IVehicle
	{
		string Color { get; }
		string RegNumber { get; }
		int Wheels { get; }

		string ToString();
	}
}