using GarageApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageApp.Model.Vehicles
{
	internal abstract class Vehicle : IVehicle
	{
		public string RegNumber { get; protected set; }
		public string Color { get; protected set; }
		public int Wheels { get; protected set; }

		public Vehicle(string regNumber, string color, int wheels)
		{
			RegNumber = regNumber;
			Color = color;
			Wheels = wheels;
		}

		public override string ToString()
		{
			return $"Type: {this.GetType().Name}, RegNumber: {RegNumber}, Color: {Color}, Wheels: {Wheels}";
		}
	}
}
