using GarageApp.Controller;
using GarageApp.Interfaces;
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
	internal class ConsoleUI : IUI
	{
		public void PrintLine(string message)
		{
			Console.WriteLine(message);
		}

		public void Print(string message)
		{
			Console.Write(message);
		}

		public int PromptNumericInput(string message)
		{
			string input = GetUserTextInput(message);

			// Validate input
			if (string.IsNullOrWhiteSpace(input))
			{
				return -1;
			}
			if (int.TryParse(input, out int number))
			{
				return number;
			}
			else
			{
				return -1;
			}
		}

		public (bool, char) PromptYesNoInput(string message)
		{
			string input = GetUserTextInput(message);

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

		public string GetUserTextInput(string prompt)
		{
			Print(prompt);
			return Console.ReadLine();
		}
	}
}
