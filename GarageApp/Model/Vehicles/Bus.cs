using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageApp.Model.Vehicles
{
	internal class Bus : Vehicle
	{
		public int Seats { get; protected set; }

		public Bus(string regNumber, string color, int wheels, int seats)
			: base(regNumber, color, wheels)
		{
			Seats = seats;
		}
		public override string ToString()
		{
			return $"{base.ToString()}, Seats: {Seats}";
		}
	}
}
