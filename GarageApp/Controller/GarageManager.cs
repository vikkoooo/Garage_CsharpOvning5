using GarageApp.Model.Vehicles;
using GarageApp.Viewer;
using System.Linq.Expressions;

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
						handler.GenerateData(size);
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
					case "5":
						FilterVehicles();
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
			ui.PrintLine("5. Filter for Vehicles");
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
					if (!handler.ValidRegNumber(regNumber))
						goto invalidInput;

					color = ui.GetUserTextInput("Color: ");
					if (!handler.ValidColor(color))
						goto invalidInput;

					wheels = ui.PromptNumericInput("Number of wheels: ");
					if (!handler.ValidWheels(wheels))
						goto invalidInput;

					int engines = ui.PromptNumericInput("Number of engines: ");
					if (!handler.ValidEngines(engines))
						goto invalidInput;

					vehicle = new Airplane(regNumber, color, wheels, engines);
					break;
				case "2":
					regNumber = ui.GetUserTextInput("Registration number (6 characters only numbers and letters): ").ToUpper();
					if (!handler.ValidRegNumber(regNumber))
						goto invalidInput;

					color = ui.GetUserTextInput("Color: ");
					if (!handler.ValidColor(color))
						goto invalidInput;

					wheels = ui.PromptNumericInput("Number of wheels: ");
					if (!handler.ValidWheels(wheels))
						goto invalidInput;

					double length = ui.PromptNumericInput("Length: ");
					if (!handler.ValidLength(length))
						goto invalidInput;

					vehicle = new Boat(regNumber, color, wheels, length);
					break;
				case "3":
					regNumber = ui.GetUserTextInput("Registration number (6 characters only numbers and letters): ").ToUpper();
					if (!handler.ValidRegNumber(regNumber))
						goto invalidInput;

					color = ui.GetUserTextInput("Color: ");
					if (!handler.ValidColor(color))
						goto invalidInput;

					wheels = ui.PromptNumericInput("Number of wheels: ");
					if (!handler.ValidWheels(wheels))
						goto invalidInput;

					int seats = ui.PromptNumericInput("Number of seats: ");
					if (!handler.ValidSeats(seats))
						goto invalidInput;

					vehicle = new Bus(regNumber, color, wheels, seats);
					break;
				case "4":
					regNumber = ui.GetUserTextInput("Registration number (6 characters only numbers and letters): ").ToUpper();
					if (!handler.ValidRegNumber(regNumber))
						goto invalidInput;

					color = ui.GetUserTextInput("Color: ");
					if (!handler.ValidColor(color))
						goto invalidInput;

					wheels = ui.PromptNumericInput("Number of wheels: ");
					if (!handler.ValidWheels(wheels))
						goto invalidInput;

					string fuel = ui.GetUserTextInput("Type of fuel (Diesel, Gasoline, Electric, Hybrid): ");
					if (!handler.ValidFuel(fuel))
						goto invalidInput;

					vehicle = new Car(regNumber, color, wheels, fuel);
					break;
				case "5":
					regNumber = ui.GetUserTextInput("Registration number (6 characters only numbers and letters): ").ToUpper();
					if (!handler.ValidRegNumber(regNumber))
						goto invalidInput;

					color = ui.GetUserTextInput("Color: ");
					if (!handler.ValidColor(color))
						goto invalidInput;

					wheels = ui.PromptNumericInput("Number of wheels: ");
					if (!handler.ValidWheels(wheels))
						goto invalidInput;

					int volume = ui.PromptNumericInput("Cylinder volume: ");
					if (!handler.ValidVolume(volume))
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
		private void FilterVehiclesMenu()
		{
			ui.PrintLine("Menu for filter type (level 1)");
			ui.PrintLine("1. All vehicles");
			ui.PrintLine("2. Airplanes only");
			ui.PrintLine("3. Boats only");
			ui.PrintLine("4. Buses only");
			ui.PrintLine("5. Cars only");
			ui.PrintLine("6. Motorcycles only");
			ui.PrintLine("0. Exit Filter Menu");
		}

		private void FilterVehicles()
		{
			FilterVehiclesMenu();
			string typeChoice = ui.GetUserTextInput("Enter filter type choice: ");

			string vehicleType = "All";

			switch (typeChoice)
			{
				case "1":
					vehicleType = "All";
					break;
				case "2":
					vehicleType = "Airplane";
					break;
				case "3":
					vehicleType = "Boat";
					break;
				case "4":
					vehicleType = "Bus";
					break;
				case "5":
					vehicleType = "Car";
					break;
				case "6":
					vehicleType = "Motorcycle";
					break;
				case "0":
					ui.PrintLine("Exiting filter menu.");
					return;
				default:
					ui.PrintLine("Invalid choice. Please enter a number from 0 to 6");
					break;
			}

			Dictionary<string, string> filters = new Dictionary<string, string>();
			bool moreFiltering = true;
			while (moreFiltering)
			{
				// Ask user if they want to filter by an attribute
				var (inputSuccess, answer) = ui.PromptYesNoInput("Filter by more attributes? (Y/N) ");

				if (!inputSuccess)
				{
					ui.PrintLine("Invalid (Y/N) input entered, please start with Y for Yes or N for No. Going back to main menu.");
					return;
				}
				else
				{
					if (answer == 'Y')
					{
						string attributeName = ui.GetUserTextInput("Enter attribute name (RegNumber, Color, Wheels): ").ToUpper();
						if (string.IsNullOrWhiteSpace(attributeName))
						{
							ui.PrintLine($"Invalid attribute name entered {attributeName}, going back to main menu");
							return;
						}
						string attributeValue = ui.GetUserTextInput("Enter filter value: ");
						if (string.IsNullOrWhiteSpace(attributeValue))
						{
							ui.PrintLine($"Invalid attribute value entered {attributeValue}, going back to main menu");
							return;
						}
						filters.Add(attributeName, attributeValue);
					}
					else if (answer == 'N')
					{
						moreFiltering = false;
					}
				}
			}
			FilterSearch(vehicleType, filters);
		}
		private void FilterSearch(string vehicleType, Dictionary<string, string> filters)
		{
			try
			{
				IEnumerable<Vehicle> filteredVehicles = handler.FilterVehicles(vehicleType, filters);

				ui.PrintLine($"Filtered results");
				foreach (var v in filteredVehicles)
				{
					ui.PrintLine(v.ToString());
				}
			}
			catch (ArgumentException ex)
			{
				ui.PrintLine($"Error: {ex.Message}");
			}
			catch (Exception ex)
			{
				ui.PrintLine($"Unknown error: {ex.Message}");
			}
		}
	}
}