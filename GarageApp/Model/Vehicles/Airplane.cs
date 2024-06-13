using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageApp.Model.Vehicles
{
	internal class Airplane : Vehicle
	{
		public int Engines { get; protected set; }

		public Airplane(string regNumber, string color, int wheels, int engines)
			: base(regNumber, color, wheels)
		{
			Engines = engines;
		}

		public override string ToString()
		{
			return $"{base.ToString()}, Engines: {Engines}";
		}
	}
}
