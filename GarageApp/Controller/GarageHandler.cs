using GarageApp.Interfaces;
using GarageApp.Model;
using GarageApp.Model.Vehicles;
using GarageApp.Viewer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageApp.Controller
{
	internal class GarageHandler : IHandler
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
			
				garage.Add(v);
			
			
		}

		// remove vehicle from garage
		public void Remove(string regNumber)
		{
		
				garage.Remove(regNumber);
			
			
		}

		// find a vehicle by reg number
		public Vehicle? Search(string regNumber)
		{
				return garage.FirstOrDefault(v => v.RegNumber.Equals(regNumber));
		}

		// list all current vehicles
		public IEnumerable<Vehicle> GetVehicles()
		{
			return garage.ToList();
		}

		// Checking if reg number already exists in our collection
		public bool IsTakenRegNumber(string regNumber)
		{
			IEnumerable<Vehicle> list = GetVehicles();
			return list.Any(vehicle => vehicle.RegNumber == regNumber); // returns true when we have a match 
		}

		// Functions checking validity, could have more "rules". 
		// Checking reg number rules, the length and it contains the correct type of characters
		public bool ValidRegNumber(string regNumber)
		{
			// if some of the cases is true, we have invalid regnumber
			if (string.IsNullOrEmpty(regNumber) || regNumber.Length != 6 || !regNumber.All(char.IsLetterOrDigit) || IsTakenRegNumber(regNumber))
			{
				return false;
			}
			return true;
		}

		// Currently accepts any string input as color
		public bool ValidColor(string color)
		{
			if (string.IsNullOrEmpty(color))
			{
				return false;
			}
			return true;
		}

		// Will accept any type of wheels as long as its not negative
		public bool ValidWheels(int wheels)
		{
			if (wheels >= 0)
			{
				return true;
			}
			return false;
		}

		// Must have at least one engine
		public bool ValidEngines(int engines)
		{
			if (engines > 0)
			{
				return true;
			}
			return false;
		}

		// Must be at least 1 cm
		public bool ValidLength(double length)
		{
			if (length > 0)
			{
				return true;
			}
			return false;
		}

		// Must have at least 1 seat
		public bool ValidSeats(int seats)
		{
			if (seats > 0)
			{
				return true;
			}
			return false;
		}

		// Currently hardcoded fueltypes. Perhaps there is a better solution for this,
		// but a GUI application would use radioboxes which will be cleaner.
		public bool ValidFuel(string fuel)
		{
			string[] fuelTypes = { "Diesel", "Gasoline", "Electric", "Hybrid" };
			if (!string.IsNullOrWhiteSpace(fuel) && fuelTypes.Contains(fuel, StringComparer.OrdinalIgnoreCase)) // note StringComparer here
			{
				return true;
			}
			return false;
		}

		// Engine volume minimum of 1 cc
		public bool ValidVolume(int volume)
		{
			if (volume > 0)
			{
				return true;
			}
			return false;
		}

		// filter only by type "step 1"
		public IEnumerable<Vehicle> FilterVehiclesTypeOnly(string vehicleType)
		{
			if (vehicleType == "All") // basically means no filter off step one
			{
				return garage.ToList(); // return entire collection, all vehicle types
			}
			else
			{
				// compare the name of the class with the vehicleType input string, return only those that match
				return garage.Where(vehicle => vehicle.GetType().Name.Equals(vehicleType));
			}
		}

		// filter by any inputs
		public IEnumerable<Vehicle> FilterVehicles(string vehicleType, Dictionary<string, string> filters)
		{
			var filteredVehicles = FilterVehiclesTypeOnly(vehicleType); // step 1: remove all that is not the correct type.

			// step 2: loop through all the pairs of filters and continue to narrow down the filteredVehicles list
			foreach (var filter in filters)
			{
				// KEY = Attribute name ex. (RegNumber, Color, Wheels)
				// VALUE = Attribute value ex. (HEJ123, Green, 4)
				switch (filter.Key)
				{
					case SearchConstants.RegNumber:
						filteredVehicles = filteredVehicles.Where(vehicle => vehicle.RegNumber
															.Equals(filter.Value, StringComparison.OrdinalIgnoreCase)); // note StringComparison instead of StringComparer
						break;
					case "COLOR":
						filteredVehicles = filteredVehicles.Where(vehicle => vehicle.Color
															.Equals(filter.Value, StringComparison.OrdinalIgnoreCase));
						break;
					case "WHEELS":
						if (int.TryParse(filter.Value, out int filterWheels))
						{
							filteredVehicles = filteredVehicles.Where(vehicle => vehicle.Wheels == filterWheels);
						}
						else
						{
							throw new ArgumentException($"Invalid wheels value: {filter.Value}");
						}
						break;
					default:
						throw new ArgumentException($"Invalid attribute name: {filter.Key}");
				}
			}
			return filteredVehicles;
		}
	}
}
