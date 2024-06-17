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
				string choice = ui.GetUserTextInput("Choice: ");
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

		// todo: break this function into more functions, its crazy long but works.
		private void AddVehicle()
		{
			AddVehicleMenu();
			string choice = ui.GetUserTextInput("Choice: ");

			// Declare variables outside of switch case
			Vehicle vehicle = null;
			string regNumber;
			string color;
			int wheels;

			switch (choice)
			{
				case "1":
					regNumber = ui.GetUserTextInput("Registration number (6 characters only numbers and letters): ").ToUpper();
					if (!ValidRegNumber(regNumber))
						goto invalidInput;

					color = ui.GetUserTextInput("Color: ");
					if (!ValidColor(color))
						goto invalidInput;

					wheels = ui.PromptNumericInput("Number of wheels: ");

					if (!ValidWheels(wheels))
						goto invalidInput;

					int engines = ui.PromptNumericInput("Number of engines: ");
					if (!ValidEngines(engines))
						goto invalidInput;

					vehicle = new Airplane(regNumber, color, wheels, engines);
					break;
				case "2":
					regNumber = ui.GetUserTextInput("Registration number (6 characters only numbers and letters): ").ToUpper();
					if (!ValidRegNumber(regNumber))
						goto invalidInput;

					color = ui.GetUserTextInput("Color: ");
					if (!ValidColor(color))
						goto invalidInput;

					wheels = ui.PromptNumericInput("Number of wheels: ");
					if (!ValidWheels(wheels))
						goto invalidInput;

					double length = ui.PromptNumericInput("Length: ");
					if (!ValidLength(length))
						goto invalidInput;

					vehicle = new Boat(regNumber, color, wheels, length);
					break;
				case "3":
					regNumber = ui.GetUserTextInput("Registration number (6 characters only numbers and letters): ").ToUpper();
					if (!ValidRegNumber(regNumber))
						goto invalidInput;

					color = ui.GetUserTextInput("Color: ");
					if (!ValidColor(color))
						goto invalidInput;

					wheels = ui.PromptNumericInput("Number of wheels: ");
					if (!ValidWheels(wheels))
						goto invalidInput;

					int seats = ui.PromptNumericInput("Number of seats: ");
					if (!ValidSeats(seats))
						goto invalidInput;

					vehicle = new Bus(regNumber, color, wheels, seats);
					break;
				case "4":
					regNumber = ui.GetUserTextInput("Registration number (6 characters only numbers and letters): ").ToUpper();
					if (!ValidRegNumber(regNumber))
						goto invalidInput;

					color = ui.GetUserTextInput("Color: ");
					if (!ValidColor(color))
						goto invalidInput;

					wheels = ui.PromptNumericInput("Number of wheels: ");
					if (!ValidWheels(wheels))
						goto invalidInput;

					string fuel = ui.GetUserTextInput("Type of fuel (Diesel, Gasoline, Electric, Hybrid): ");
					if (!ValidFuel(fuel))
						goto invalidInput;

					vehicle = new Car(regNumber, color, wheels, fuel);
					break;
				case "5":
					regNumber = ui.GetUserTextInput("Registration number (6 characters only numbers and letters): ").ToUpper();
					if (!ValidRegNumber(regNumber))
						goto invalidInput;

					color = ui.GetUserTextInput("Color: ");
					if (!ValidColor(color))
						goto invalidInput;

					wheels = ui.PromptNumericInput("Number of wheels: ");
					if (!ValidWheels(wheels))
						goto invalidInput;

					int volume = ui.PromptNumericInput("Cylinder volume: ");
					if (!ValidVolume(volume))
						goto invalidInput;

					vehicle = new Motorcycle(regNumber, color, wheels, volume);
					break;
				invalidInput:
					ui.PrintLine("Invalid input. Going back to main menu");
					return;
				default:
					ui.PrintLine("Invalid choice. Going back to main menu");
					return;
			}
			try
			{
				handler.Add(vehicle);
				ui.PrintLine("Vehicle added successfully.");
			}
			catch (InvalidOperationException ex)
			{
				ui.PrintLine("Garage is full");
			}
			catch (Exception ex)
			{
				ui.PrintLine($"Unknown error: {ex.Message}");
			}
		}

		private bool IsTakenRegNumber(string regNumber)
		{
			IEnumerable<Vehicle> list = handler.GetVehicles();
			bool isTaken = list.Any(vehicle => vehicle.RegNumber == regNumber);

			return isTaken;
		}

		private bool ValidRegNumber(string regNumber)
		{
			if ((string.IsNullOrEmpty(regNumber)) || regNumber.Length != 6 || !regNumber.All(char.IsLetterOrDigit) || IsTakenRegNumber(regNumber))
			{
				return false;
			}
			return true;
		}

		private bool ValidColor(string color)
		{
			if (string.IsNullOrEmpty(color))
			{
				return false;
			}
			return true;
		}

		private bool ValidWheels(int wheels)
		{
			if (wheels > 0)
			{
				return true;
			}
			return false;
		}

		private bool ValidEngines(int engines)
		{
			if (engines > 0)
			{
				return true;
			}
			return false;
		}

		private bool ValidLength(double length)
		{
			if (length > 0)
			{
				return true;
			}
			return false;
		}

		private bool ValidSeats(int seats)
		{
			if (seats > 0)
			{
				return true;
			}
			return false;
		}

		private bool ValidFuel(string fuel)
		{
			string[] fuelTypes = { "DIESEL", "GASOLINE", "ELECTRIC", "HYBRID" };
			if (!string.IsNullOrWhiteSpace(fuel) && fuelTypes.Contains(fuel.ToUpper()))
			{
				return true;
			}
			return false;
		}

		private bool ValidVolume(int volume)
		{
			if (volume > 0)
			{
				return true;
			}
			return false;
		}

		private void RemoveVehicle()
		{
			string regNumber = ui.GetUserTextInput("Enter registration number of Vehicle to remove: ");
			try
			{
				handler.Remove(regNumber);
				ui.PrintLine($"Success removing Vehicle with registration number: {regNumber}");
			}
			catch (KeyNotFoundException ex)
			{
				ui.PrintLine($"{ex.Message}");
			}
			catch (Exception ex)
			{
				ui.PrintLine($"Unknown error: {ex.Message}");
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
			string regNumber = ui.GetUserTextInput("Enter registration number of Vehicle to search for: ");
			try
			{
				Vehicle vehicle = handler.Search(regNumber);
				ui.PrintLine($"Found vehicle: with registration number{vehicle.RegNumber}");
				ui.PrintLine(vehicle.ToString());
			}
			catch (KeyNotFoundException ex)
			{
				ui.PrintLine($"{ex.Message}");
			}
			catch (Exception ex)
			{
				ui.PrintLine($"Unknown error: {ex.Message}");
			}
		}
	}
}