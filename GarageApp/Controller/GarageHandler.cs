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
		public void DevGenerateData(int size, double ratio = 0.5)
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
				garage.Remove(regNumber.ToUpper());
			}
			catch (Exception)
			{
				throw;
			}
		}

		// find a vehicle by reg number (remember .toupper)
		public Vehicle Search(string regNumber)
		{
			// todo: throw exception if we get null object back
			try
			{
				return garage.Search(regNumber.ToUpper());
			}
			catch (Exception)
			{
				throw;
			}

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
