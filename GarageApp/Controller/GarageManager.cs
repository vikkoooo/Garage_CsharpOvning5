using GarageApp.Model.Vehicles;
using GarageApp.Viewer;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GarageApp.Controller
{
	internal class GarageManager
	{
		private ConsoleUI ui;
		private GarageHandler handler;
		private bool isRunning = false;
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
				var (inputSuccess, answer) = ui.PromptYesNoInput("Do you want to fill with dummy data? (Y/N): ");

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
				string choice = ui.GetUserInput("Choice: ");
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
						ui.PrintLine("Invalid input, please write only the numbers displayed in the Main Menu");
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
		}

		private void AddVehicleMenu()
		{
			ui.PrintLine("Add Vehicle");
			ui.PrintLine("1. Airplane");
			ui.PrintLine("2. Boat");
			ui.PrintLine("3. Bus");
			ui.PrintLine("4. Car");
			ui.PrintLine("5. Motorcycle");
		}

		private void AddVehicle()
		{
			AddVehicleMenu();
			string choice = ui.GetUserInput("Choice: ");

			string regNumber = ui.GetUserInput("Registration number: ");
			string color = ui.GetUserInput("Color: ");
			int wheels = ui.PromptNumericInput("Number of wheels: ");

			switch (choice)
			{
				case "1":
					int engines = ui.PromptNumericInput("Number of engines: ");
					try
					{
						handler.Add(new Airplane(regNumber, color, wheels, engines));
					}
					catch (IndexOutOfRangeException e)
					{
						ui.PrintLine("Garage is full");
					}
					break;
				case "2":
					int length = ui.PromptNumericInput("Length: ");
					handler.Add(new Boat(regNumber, color, wheels, length));
					break;
				case "3":
					int seats = ui.PromptNumericInput("Number of seats: ");
					handler.Add(new Bus(regNumber, color, wheels, seats));
					break;
				case "4":
					string fuel = ui.GetUserInput("Type of fuel: ");
					handler.Add(new Car(regNumber, color, wheels, fuel));
					break;
				case "5":
					int volume = ui.PromptNumericInput("Cylinder volume: ");
					handler.Add(new Motorcycle(regNumber, color, wheels, volume));
					break;
				default:
					ui.PrintLine("Invalid choice. Going back to main menu");
					break;
			}
		}

		private void RemoveVehicle()
		{
			string regNumber = ui.GetUserInput("Enter registration number of Vehicle to remove: ");
			try
			{
				handler.Remove(regNumber);
				ui.PrintLine($"Success removing Vehicle with registration number: {regNumber}");
			}
			catch (Exception e)
			{
				ui.PrintLine($"Did not find any Vehicle with registration number {regNumber}");
				ui.PrintLine($"DEVSTUFF: exeception e: {e.Message}");
			}
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
			string regNumber = ui.GetUserInput("Enter registration number of Vehicle to search for: ");
			try
			{
				Vehicle vehicle = handler.Search(regNumber);
				ui.PrintLine($"Found vehicle: with registration number{vehicle.RegNumber}");
				ui.PrintLine(vehicle.ToString());
			}
			catch (Exception e)
			{
				ui.PrintLine($"Did not find any Vehicle with registration number {regNumber}");
				ui.PrintLine($"DEVSTUFF: exeception e: {e.Message}");
			}
		}
	}
}