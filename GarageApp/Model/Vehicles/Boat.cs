using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageApp.Model.Vehicles
{
	internal class Boat : Vehicle
	{
		public double Length { get; protected set; }

		public Boat(string regNumber, string color, int wheels, double length)
			: base(regNumber, color, wheels)
		{
			Length = length;
		}
		public override string ToString()
		{
			return $"{base.ToString()}, Length: {Length}";
		}
	}
}
