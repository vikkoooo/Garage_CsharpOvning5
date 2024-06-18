using GarageApp.Model.Vehicles;
using GarageApp.Viewer;
using System.Linq.Expressions;

namespace GarageApp.Controller
{
	internal class GarageManager
	{
		private ConsoleUI ui; // ui instance passed from Program.cs
		private GarageHandler handler; // handler instance which interacts with datastructure
		private bool isRunning = false; // main application flag
		private int size; // tmp variable used on launch to set garage size

		public GarageManager(ConsoleUI ui)
		{
			this.ui = ui;
		}

		// before main application starts, we need to fetch some settings from the user
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
			} while (handler == null); // run until we actually can make a handler instance

			// Ask if we should generate data
			bool success = false; // make flag outside of while scope
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
					// Input from user OK, continue with corresponding choice
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

			// Start the main application
			isRunning = true;
			StartApp();
		}

		// Main application loop
		private void StartApp()
		{
			while (isRunning)
			{
				MainMenu(); // Main menu display
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

		// Main menu display
		private void MainMenu()
		{
			ui.PrintLine("Main Menu");
			ui.PrintLine("1. Add Vehicle");
			ui.PrintLine("2. Remove Vehicle");
			ui.PrintLine("3. List Vehicles");
			ui.PrintLine("4. Search Vehicle");
			ui.PrintLine("5. Filter Vehicles");
			ui.PrintLine("0. Exit");
		}

		// Submenu Add Vehicle display
		private void AddVehicleMenu()
		{
			ui.PrintLine("Add Vehicle");
			ui.PrintLine("1. Airplane");
			ui.PrintLine("2. Boat");
			ui.PrintLine("3. Bus");
			ui.PrintLine("4. Car");
			ui.PrintLine("5. Motorcycle");
		}

		// Add a vehicle to garage
		private void AddVehicle()
		{
			AddVehicleMenu(); // submenu display
			string choice = ui.GetUserTextInput("Choice: ");

			Vehicle vehicle = null; // vehicle instance which will be added in the end of the function
			string regNumber; // needs to be declared outside of scope or we will have to have different names in each switch case
			string color;
			int wheels;

			// Depending on the choice (selections as per AddVehicleMenu), run logic for the specific Vehicle
			switch (choice)
			{
				// Airplane
				case "1":
					// Properties for all vehicles
					regNumber = ui.GetUserTextInput("Registration number (6 characters only numbers and letters): ").ToUpper();
					if (!handler.ValidRegNumber(regNumber))
						goto invalidInput; // early exit when user fails, to avoid user having to enter all data and fail later on

					color = ui.GetUserTextInput("Color: ");
					if (!handler.ValidColor(color))
						goto invalidInput;

					wheels = ui.PromptNumericInput("Number of wheels: ");
					if (!handler.ValidWheels(wheels))
						goto invalidInput;

					// Specific for only Airplane
					int engines = ui.PromptNumericInput("Number of engines: ");
					if (!handler.ValidEngines(engines))
						goto invalidInput;

					// Creating the actual instance 
					vehicle = new Airplane(regNumber, color, wheels, engines);
					break;

				// Boat
				case "2":
					// Properties for all vehicles
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

				// Bus
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

				// Car
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

				// Motorcycle
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

				// We end up here as soon as user enters invalid input according to valid checks in handler class
				invalidInput:
					ui.PrintLine("Invalid input. Going back to main menu");
					return; // note return instead of break to exit function directly

				// Bad input, exit
				default:
					ui.PrintLine("Invalid choice. Going back to main menu");
					return; // note return to exit directly
			}

			// At this point vehicle shall already be created, so add to garage
			try
			{
				if (vehicle != null)
				{
					handler.Add(vehicle);
					ui.PrintLine("Vehicle added successfully.");
				}
			}
			catch (InvalidOperationException ex)
			{
				ui.PrintLine($"{ex.Message}");
			}
			catch (Exception ex)
			{
				ui.PrintLine($"Unknown error: {ex.Message}");
			}
		}

		// Remove vehicle from garage
		private void RemoveVehicle()
		{
			// Get vehicle registration number to remove
			string regNumber = ui.GetUserTextInput("Enter registration number of Vehicle to remove: ");

			// Attempt removal, print error if no success
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

		// Print all vehicles in the garage
		private void ListVehicles()
		{
			IEnumerable<Vehicle> list = handler.GetVehicles(); // iterable collection

			// iterate over the list and print
			foreach (Vehicle vehicle in list)
			{
				ui.PrintLine(vehicle.ToString());
			}
		}

		// Search for a specific vehicle
		private void SearchVehicle()
		{
			// The vehicle to search for
			string regNumber = ui.GetUserTextInput("Enter registration number of Vehicle to search for: ");

			// Attempt the search, print error if no success
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

		// Submenu for filtering
		private void FilterVehiclesMenu()
		{
			ui.PrintLine("Menu for filter. Select the TYPE to filter");
			ui.PrintLine("1. Show All vehicles");
			ui.PrintLine("2. Show Airplanes only");
			ui.PrintLine("3. Show Boats only");
			ui.PrintLine("4. Show Buses only");
			ui.PrintLine("5. Show Cars only");
			ui.PrintLine("6. Show Motorcycles only");
			ui.PrintLine("0. Exit Filter Menu");
		}

		/*
		// Filtering vehicles and attributes
		private void FilterVehiclesOld()
		{
			FilterVehiclesMenu(); // show type filter submenu
			string typeChoice = ui.GetUserTextInput("Enter filter type choice: ");
			string vehicleType; // declare outside of scope to reach after switch case

			// Determine Vehicle Type filter
			switch (typeChoice)
			{
				// All
				case "1":
					vehicleType = "All";
					break;
				// Airplane
				case "2":
					vehicleType = "Airplane";
					break;
				// Boat
				case "3":
					vehicleType = "Boat";
					break;
				// Bus
				case "4":
					vehicleType = "Bus";
					break;
				//Car
				case "5":
					vehicleType = "Car";
					break;
				// Motorcycle
				case "6":
					vehicleType = "Motorcycle";
					break;
				// Exit
				case "0":
					ui.PrintLine("Exiting filter menu.");
					return;
				// Bad input
				default:
					ui.PrintLine("Invalid choice. Please enter a number from 0 to 6");
					return;
			}

			// Attribute filtering
			Dictionary<string, string> filters = new Dictionary<string, string>(); // one pair = one layer of filter
			bool moreFiltering = true; // fetch more layers of filter until user tells us its done
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
		*/

		// Filtering vehicle types
		private void FilterVehicles()
		{
			bool validChoice = false; // flag to keep track of valid input
			string vehicleType = "All"; // default to "All", changed in the switch statement
			while (!validChoice)
			{
				FilterVehiclesMenu(); // show type filter submenu
				string typeChoice = ui.GetUserTextInput("Enter filter type choice: ");

				// Determine Vehicle Type filter
				switch (typeChoice)
				{
					case "1":
						vehicleType = "All";
						validChoice = true;
						break;
					case "2":
						vehicleType = "Airplane";
						validChoice = true;
						break;
					case "3":
						vehicleType = "Boat";
						validChoice = true;
						break;
					case "4":
						vehicleType = "Bus";
						validChoice = true;
						break;
					case "5":
						vehicleType = "Car";
						validChoice = true;
						break;
					case "6":
						vehicleType = "Motorcycle";
						validChoice = true;
						break;
					// Exit
					case "0":
						ui.PrintLine("Exiting filter menu.");
						return;
					// Bad input, start the loop over again
					default:
						ui.PrintLine("Invalid choice. Please enter a number from 0 to 6");
						break;
				}
			}

			// Send to attribute filtering
			FilterAttributes(vehicleType);
		}

		// Filter for attributes
		private void FilterAttributes(string vehicleType)
		{
			Dictionary<string, string> filters = new Dictionary<string, string>(); // one pair = one layer of filter
			bool moreFiltering = true; // fetch more layers of filter until user tells us its done
			while (moreFiltering)
			{
				// Ask user if they want to filter by an attribute
				var (inputSuccess, answer) = ui.PromptYesNoInput("Filter by more attributes? (Y/N) No means execute the filtering search: ");

				if (!inputSuccess)
				{
					ui.PrintLine("Invalid (Y/N) input entered, please start with Y for Yes or N for No");
					break;
				}
				else
				{
					if (answer == 'Y')
					{
						// Fetch filter attribute name
						string attributeName = ui.GetUserTextInput("Enter attribute name (RegNumber, Color, Wheels): ").ToUpper(); // toupper because it should match string switch case in Handler FilterVehicles function
						if (string.IsNullOrWhiteSpace(attributeName)) // Bad input check
						{
							ui.PrintLine($"Invalid attribute name entered {attributeName}");
							break;
						}

						// Fetch attribute value to filter by 
						string attributeValue = ui.GetUserTextInput("Enter filter value: ");
						if (string.IsNullOrWhiteSpace(attributeValue)) // Bad input check
						{
							ui.PrintLine($"Invalid attribute value entered {attributeValue}, going back to main menu");
							break;
						}
						filters.Add(attributeName, attributeValue); // add to current filters datastructure
					}
					else if (answer == 'N')
					{
						moreFiltering = false; // user is done, exit while loop and execute
					}
				}
			}
			FilterSearch(vehicleType, filters); // execute the search
		}

		// Executes search/filtering
		private void FilterSearch(string vehicleType, Dictionary<string, string> filters)
		{
			// Call the filter function in handler, and print the results, or exception if failed.
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