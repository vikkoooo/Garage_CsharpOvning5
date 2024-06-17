using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageApp.Interfaces
{
	internal interface IUI
	{
		void PrintLine(string message);
		void Print(string message);
		int PromptNumericInput(string message);
		(bool, char) PromptYesNoInput(string message);
		string GetUserTextInput(string prompt);
	}
}
