using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageApp.Model.Vehicles
{
	internal abstract class Vehicle
	{
		public string RegistrationNumber { get; protected set; }
		public string Color { get; protected set; }
		public int NumberOfWheels { get; protected set; }

		public Vehicle(string registrationNumber, string color, int numberOfWheels)
		{
			RegistrationNumber = registrationNumber;
			Color = color;
			NumberOfWheels = numberOfWheels;
		}
	}
}
