using GarageApp.Controller;
using GarageApp.Model.Vehicles;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GarageApp.Viewer
{
	internal class ConsoleUI
	{
		public ConsoleUI()
		{
		}

		// NEW FUNCTIONS HERE AFTER MEETING WITH DIMITRIS
		internal void PrintLine(string message)
		{
			Console.WriteLine(message);
		}

		internal void Print(string message)
		{
			Console.Write(message);
		}

		internal int PromptNumericInput(string message)
		{
			Console.Write(message); // ask user for input
			var (success, number) = FetchNumericInput();

			if (success)
			{

				return number;
			}
			else
			{
				return -1;
			}
		}

		internal (bool, int) FetchNumericInput()
		{
			// Get user input
			string input = Console.ReadLine();

			// Validate input
			if (string.IsNullOrWhiteSpace(input))
			{
				return (false, -1);
			}
			if (int.TryParse(input, out int number))
			{
				return (true, number);
			}
			else
			{
				return (false, -1);
			}
		}

		internal (bool, char) PromtYesNoInput(string message)
		{
			Console.WriteLine(message);
			string input = Console.ReadLine();

			if (!string.IsNullOrWhiteSpace(input))
			{
				char[] answer = input.ToUpper().ToCharArray();
				if (answer[0] == 'Y')
				{
					return (true, 'Y');
				}
				else if (answer[0] == 'N')
				{
					return (true, 'N');
				}
				else
				{
					return (false, ' ');
				}
			}
			else
			{
				return (false, ' ');
			}
		}
	}
}
