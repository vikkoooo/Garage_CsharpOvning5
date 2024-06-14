
using GarageApp.Controller;
using GarageApp.Model.Vehicles;
using GarageApp.Viewer;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GarageApp
{
	internal class GarageManager
	{
		private ConsoleUI ui;
		private GarageHandler handler;
		private bool isRunning = false;

		// Variables used to launch app
		//private bool success = false;
		private int size;

		public GarageManager(ConsoleUI ui)
		{
			this.ui = ui;
		}

		internal void Launch()
		{
			// Get garage size before starting the app
			do
			{
				// Ask user for size
				size = ui.PromptNumericInput("Please enter the size of the Garage you want to create: ");

				// Create new GarageHandler if successful input
				if (size > 0)
				{
					handler = new GarageHandler(size);
					ui.PrintLine($"Garage of size {size} created");
				}
				else
				{
					// Try get input again
					ui.PrintLine("Invalid size input, please try again. Enter only numeric values without blankspaces.");
					continue;
				}
			} while (handler == null);

			// Ask if we should generate data
			bool success = false; // make flag
			do
			{
				// Fill with dummy data? (Y/N)
				var (inputSuccess, answer) = ui.PromtYesNoInput("Do you want to fill with dummy data? (Y/N)");

				if (!inputSuccess)
				{
					// Try get input again
					ui.PrintLine("Invalid (Y/N) input entered, please start with Y for Yes or N for No");
					continue;
				}
				else
				{
					success = true;
					if (answer == 'Y')
					{
						ui.PrintLine("Filling the garage with dummy data");
						handler.DevGenerateData(size);
					}
					else if (answer == 'N')
					{
						ui.PrintLine($"Empty garage of size {size} created");
					}
				}
			} while (!success);

			// Start the application
			isRunning = true;
			StartApp();
		}

		private void StartApp()
		{
			while (isRunning)
			{
				MainMenu();
				string choice = Console.ReadLine();
				switch (choice)
				{
					case "1":
						AddVehicle();
						break;
					case "2":
						RemoveVehicle();
						break;
					case "3":
						ListVehicles();
						break;
					case "4":
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

		private void MainMenu()
		{
			ui.PrintLine("Main Menu");
			ui.PrintLine("1. Add Vehicle");
			ui.PrintLine("2. Remove Vehicle");
			ui.PrintLine("3. List Vehicles");
			ui.PrintLine("4. Search Vehicle");
			ui.PrintLine("0. Exit");
			ui.Print("Choice: ");
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
				ui.PrintLine(vehicle.ToString());
			}
		}

		private void SearchVehicle()
		{
			throw new NotImplementedException();
		}
	}
}