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
		public GarageHandler()
		{
			new ConsoleUI(this);
		}

		public void StartGarage(int size)
		{
			garage = new Garage<Vehicle>(size); // set garage size at start (should be by user choice tho)
												//DevGenerateData(size);  // generate dummy vehicles with dummy values (fill half garage default) by calling DummyDataGenerator
												//DevPrintVehicles();
		}

		// Methods
		public void DevGenerateData(int size, double ratio = 0.5)
		{
			int n = (int)Math.Round(size * ratio, 0);

			for (int i = 0; i < n; i++)
			{
				garage.Add(DummyDataGenerator.GetRandomVehicle());
			}
		}

		// add vehicle to garage
		public void Add(Vehicle v)
		{
			garage.Add(v); // todo: throw exception if error
		}

		// remove vehicle from garage (maybe by searching on the reg number instead??)
		public void Remove(Vehicle v)
		{
			garage.Remove(v); // todo: throw exception if error
		}

		// find a vehicle by reg number (remember .toupper)
		public Vehicle Search(string query)
		{
			// todo: throw exception if we get null object back
			return garage.Search(query.ToUpper());
		}

		// list all current vehicles
		// dont know if it should be IEnumerable or IEnumerator?
		// I think IEnumerable is correct by https://stackoverflow.com/questions/619564/what-is-the-difference-between-ienumerator-and-ienumerable
		public IEnumerable<Vehicle> GetVehicles()
		{
			return garage.ToList();
		}
		// list specific type of vehicle and the count of those
		// find using linq "all black with 4 wheels", "all motorcycles pink 3 wheels", "all trucks", "all red"
	}
}
