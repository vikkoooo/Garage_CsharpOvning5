using GarageApp.Model.Vehicles;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GarageApp.Controller
{
	internal static class DummyDataGenerator
	{
		private static Random rand = new Random();

		// Generate Random Vehicle
		public static Vehicle GetRandomVehicle()
		{
			// Randomize which Vehicle Type
			switch (rand.Next(1, 6))
			{
				case 1:
					return GenerateAirplane();
				case 2:
					return GenerateBoat();
				case 3:
					return GenerateBus();
				case 4:
					return GenerateCar();
				case 5:
					return GenerateMotorcycle();
				default:
					return null;
			}
		}

		// Private methods
		// Generate Airplane 
		private static Airplane GenerateAirplane()
		{
			string regNumber = GenerateRegNumber();
			string color = GenerateColor();
			int wheels = GenerateWheels();
			int engines = rand.Next(1, 7);

			return new Airplane(regNumber, color, wheels, engines);
		}
		// Generate Boat
		private static Boat GenerateBoat()
		{
			string regNumber = GenerateRegNumber();
			string color = GenerateColor();
			int wheels = GenerateWheels();
			double length = rand.Next(5, 50);

			return new Boat(regNumber, color, wheels, length);
		}
		// Generate Bus
		private static Bus GenerateBus()
		{
			string regNumber = GenerateRegNumber();
			string color = GenerateColor();
			int wheels = GenerateWheels();
			int seats = rand.Next(10, 100);

			return new Bus(regNumber, color, wheels, seats);
		}
		// Generate Car
		private static Car GenerateCar()
		{
			string regNumber = GenerateRegNumber();
			string color = GenerateColor();
			int wheels = GenerateWheels();
			string fuel = GenerateFuel();

			return new Car(regNumber, color, wheels, fuel);
		}
		// Generate Motorcycle
		private static Motorcycle GenerateMotorcycle()
		{
			string regNumber = GenerateRegNumber();
			string color = GenerateColor();
			int wheels = GenerateWheels();
			int volume = rand.Next(125, 1000);

			return new Motorcycle(regNumber, color, wheels, volume);
		}

		private static string GenerateRegNumber()
		{
			string possibleChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
			char[] chars = possibleChars.ToCharArray();
			string result = string.Empty;

			for (int i = 0; i < 6; i++)
			{
				result += chars[rand.Next(0, possibleChars.Length)];
			}
			return result;
		}

		private static int GenerateWheels()
		{
			return rand.Next(1, 11);
		}

		private static string GenerateColor()
		{
			string[] colors = { "Red", "Blue", "Yellow", "White", "Black" };
			return colors[rand.Next(0, colors.Length)];
		}

		private static string GenerateFuel()
		{
			string[] fuelTypes = { "Diesel", "Gasoline", "Electric", "Hybrid" };
			return fuelTypes[rand.Next(0, fuelTypes.Length)];
		}
	}
}
