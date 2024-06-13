using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageApp.Model.Vehicles
{
	internal class Car : Vehicle
	{
		public string FuelType { get; protected set; }

		public Car(string registrationNumber, string color, int numberOfWheels, string fuelType)
			: base(registrationNumber, color, numberOfWheels)
		{
			FuelType = fuelType;
		}
	}
}
