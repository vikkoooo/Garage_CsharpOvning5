using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageApp.Model.Vehicles
{
	internal class Car : Vehicle
	{
		public string Fuel { get; protected set; }

		public Car(string regNumber, string color, int wheels, string fuel)
			: base(regNumber, color, wheels)
		{
			Fuel = fuel;
		}
		public override string ToString()
		{
			return $"{base.ToString()}, Fuel: {Fuel}";
		}
	}
}
