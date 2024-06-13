using GarageApp.Controller;
using GarageApp.Model.Vehicles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GarageApp.Viewer
{
	internal class ConsoleUI
	{
		// Variables
		private GarageHandler handler;
		private bool isRunning = false;
		private int size = 0;

		public ConsoleUI(GarageHandler handler)
		{
			Console.WriteLine("Welcome to Garage APP! Make sure to start with creating a Garage");
			this.handler = handler;
			isRunning = true;
			StartApp();
		}

		private void MainMenuDisplay()
		{
			Console.WriteLine();
			Console.WriteLine("Main Menu");
			Console.WriteLine("1. Create Garage");
			Console.WriteLine("2. Populate Garage with Vehicles");
			Console.WriteLine("3. Add Vehicle");
			Console.WriteLine("4. Remove Vehicle");
			Console.WriteLine("5. List Vehicles");
			Console.WriteLine("6. Search Vehicle");
			Console.WriteLine("0. Exit");
			Console.Write("Choice: ");
		}

		private void StartApp()
		{
			while (isRunning)
			{
				MainMenuDisplay();
				string choice = Console.ReadLine();
				switch (choice)
				{

					case "1":
						CreateGarage();
						break;
					case "2":
						PopulateGarage();
						break;
					case "3":
						AddVehicle();
						break;
					case "4":
						RemoveVehicle();
						break;
					case "5":
						ListVehicles();
						break;
					case "7":
						SearchVehicle();
						break;
					case "0":
						isRunning = false;
						break;
					default:
						Console.WriteLine("Invalid input, please write only the numbers displayed in the Main Menu");
						break;
				}
			}
		}

		// todo: make sure it is only possible to create one garage
		private void CreateGarage()
		{
			Console.WriteLine("Enter the size of the garage you want to create");
			Console.Write("Size: ");

			if (int.TryParse(Console.ReadLine(), out size))
			{
				handler.StartGarage(size);
				Console.WriteLine($"Garage of size {size} created");
			}
			else
			{
				Console.WriteLine("Invalid size input, please try again");
			}
		}

		private void PopulateGarage()
		{
			handler.DevGenerateData(size);
		}

		private void AddVehicle()
		{
			throw new NotImplementedException();
		}

		private void RemoveVehicle()
		{
			throw new NotImplementedException();
		}

		private void ListVehicles()
		{
			IEnumerable<Vehicle> list = handler.GetVehicles();
			foreach (Vehicle vehicle in list)
			{
				Console.WriteLine(vehicle.ToString());
			}
		}

		private void SearchVehicle()
		{
			throw new NotImplementedException();
		}
	}
}
