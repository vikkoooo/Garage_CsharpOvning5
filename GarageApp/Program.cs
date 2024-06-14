using GarageApp.Controller;
using GarageApp.Viewer;

namespace GarageApp
{
	internal class Program
	{
		static void Main(string[] args)
		{
			GarageManager manager = new GarageManager(new ConsoleUI());
			manager.Launch();
		}
	}
}
