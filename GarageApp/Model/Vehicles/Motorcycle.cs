using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageApp.Model.Vehicles
{
	internal class Motorcycle : Vehicle
	{
		public int Volume { get; protected set; }

		public Motorcycle(string regNumber, string color, int wheels, int volume)
			: base(regNumber, color, wheels)
		{
			Volume = volume;
		}
		public override string ToString()
		{
			return $"{base.ToString()}, Volume: {Volume}";
		}
	}
}
