using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageApp.Model.Vehicles
{
	internal class Motorcycle : Vehicle
	{
		public int CylinderVolume { get; protected set; }

		public Motorcycle(string registrationNumber, string color, int numberOfWheels, int cylinderVolume)
			: base(registrationNumber, color, numberOfWheels)
		{
			CylinderVolume = cylinderVolume;
		}
	}
}
