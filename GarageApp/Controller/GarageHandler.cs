using GarageApp.Model;
using GarageApp.Model.Vehicles;
using GarageApp.Viewer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Diagnostics.Activity;

namespace GarageApp.Controller
{
	internal class GarageHandler
	{
		// Variables
		private Garage<Vehicle> garage;

		// Constructor
		public GarageHandler(int size)
		{
			garage = new Garage<Vehicle>(size); // set garage size at start
		}

		// Methods
		public void GenerateData(int size, double ratio = 0.5)
		{
			int n;

			if (size == 1) // Check edge case
			{
				n = 1;
			}
			else
			{
				n = (int)Math.Round(size * ratio, 0);
			}

			for (int i = 0; i < n; i++)
			{
				garage.Add(DummyDataGenerator.GetRandomVehicle());
			}
		}

		// add vehicle to garage
		public void Add(Vehicle v)
		{
			try
			{
				garage.Add(v);
			}
			catch (Exception)
			{
				throw;
			}
		}

		// remove vehicle from garage
		public void Remove(string regNumber)
		{
			try
			{
				garage.Remove(regNumber);
			}
			catch (Exception)
			{
				throw;
			}
		}

		// find a vehicle by reg number
		public Vehicle Search(string regNumber)
		{
			try
			{
				return garage.Search(regNumber);
			}
			catch (Exception)
			{
				throw;
			}
		}

		// list all current vehicles
		public IEnumerable<Vehicle> GetVehicles()
		{
			return garage.ToList();
		}

		public bool IsTakenRegNumber(string regNumber)
		{
			IEnumerable<Vehicle> list = GetVehicles();
			bool isTaken = list.Any(vehicle => vehicle.RegNumber == regNumber);

			return isTaken;
		}

		public bool ValidRegNumber(string regNumber)
		{
			if ((string.IsNullOrEmpty(regNumber)) || regNumber.Length != 6 || !regNumber.All(char.IsLetterOrDigit) || IsTakenRegNumber(regNumber))
			{
				return false;
			}
			return true;
		}

		public bool ValidColor(string color)
		{
			if (string.IsNullOrEmpty(color))
			{
				return false;
			}
			return true;
		}

		public bool ValidWheels(int wheels)
		{
			if (wheels > 0)
			{
				return true;
			}
			return false;
		}

		public bool ValidEngines(int engines)
		{
			if (engines > 0)
			{
				return true;
			}
			return false;
		}

		public bool ValidLength(double length)
		{
			if (length > 0)
			{
				return true;
			}
			return false;
		}

		public bool ValidSeats(int seats)
		{
			if (seats > 0)
			{
				return true;
			}
			return false;
		}

		public bool ValidFuel(string fuel)
		{
			string[] fuelTypes = { "DIESEL", "GASOLINE", "ELECTRIC", "HYBRID" };
			if (!string.IsNullOrWhiteSpace(fuel) && fuelTypes.Contains(fuel.ToUpper()))
			{
				return true;
			}
			return false;
		}

		public bool ValidVolume(int volume)
		{
			if (volume > 0)
			{
				return true;
			}
			return false;
		}

		// filter only type
		public IEnumerable<Vehicle> FilterVehiclesTypeOnly(string vehicleType)
		{
			if (vehicleType == "All")
			{
				return garage.ToList();
			}
			else
			{
				// compare the name of the class with the vehicleType input string, return only those that match
				return garage.Where(v => v.GetType().Name.Equals(vehicleType));
			}
		}

		// filter by 3 inputs
		public IEnumerable<Vehicle> FilterVehiclesAll(string vehicleType, string attributeName, string attributeValue)
		{
			var filteredVehicles = FilterVehiclesTypeOnly(vehicleType);

			switch (attributeName)
			{
				case "REGNUMBER":
					filteredVehicles = filteredVehicles.Where(v => v.RegNumber.Equals(attributeValue, StringComparison.OrdinalIgnoreCase));
					break;
				case "COLOR":
					filteredVehicles = filteredVehicles.Where(v => v.Color.Equals(attributeValue, StringComparison.OrdinalIgnoreCase));
					break;
				case "WHEELS":
					if (int.TryParse(attributeValue, out int wheels))
					{
						filteredVehicles = filteredVehicles.Where(v => v.Wheels == wheels);
					}
					break;
				default:
					break;
			}
			return filteredVehicles;
		}
	}
}
