using GarageApp.Model;
using GarageApp.Model.Vehicles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageApp.Controller
{
	internal class GarageHandler
	{
		// Variables
		private Garage<Vehicle> garage;

		// Constructor
		public GarageHandler()
		{
			int size = 20;
			garage = new Garage<Vehicle>(size); // set garage size at start (should be by user choice tho)
			DevGenerateData(size);  // generate dummy vehicles with dummy values (fill half garage default) by calling DummyDataGenerator
			PrintVehicles();
		}

		// Methods
		private void DevGenerateData(int size, double ratio = 0.5)
		{
			int n = (int)Math.Round(size * ratio, 0);

			for (int i = 0; i < n; i++)
			{
				garage.Add(DummyDataGenerator.GetRandomVehicle());
			}
		}


		// add vehicle to garage
		// remove vehicle from garage (maybe by searching on the reg number)
		// find a vehicle by reg number (remember .toupper)

		public void PrintVehicles()
		{
			foreach (var e in garage)
			{
				Console.WriteLine(e.ToString());
			}
		}


		// list all current vehicles
		// list specific type of vehicle and the count of those
		// find using linq "all black with 4 wheels", "all motorcycles pink 3 wheels", "all trucks", "all red"
	}
}
