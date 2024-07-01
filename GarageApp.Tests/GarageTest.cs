using GarageApp.Model;
using GarageApp.Model.Vehicles;

namespace GarageApp.Tests
{
	public class GarageTest
	{
		// Fake Vehicle class
		private class TestVehicle : Vehicle
		{
			public TestVehicle(string regNumber, string color, int wheels)
				: base(regNumber, color, wheels)
			{
			}
		}

        public GarageTest()
        {
            
        }

        [Fact]
		public void Add_AddVehicleGarageNotFull_AddVehicle()
		{
			// Arrange (setup what should be tested, instances variables etc)
			var garage = new Garage<TestVehicle>(1);
			var vehicle = new TestVehicle("ABC123", "Red", 4);

			// Act (execute the test)
			garage.Add(vehicle);

			// Assert (check behaviour, if it behaves as expected)
			Assert.Contains(vehicle, garage);
		}

		[Fact]
		public void Add_AddVehicleGarageIsFull_ThrowInvalidOperationException()
		{
			// Arrange
			var garage = new Garage<TestVehicle>(1);
			var vehicle1 = new TestVehicle("ABC123", "Red", 4);
			var vehicle2 = new TestVehicle("DEF456", "Blue", 4);
			garage.Add(vehicle1);

			// Act + assert
			// before lambda = expected behaviour
			// after lambda = function to execute
			Assert.Throws<InvalidOperationException>(() => garage.Add(vehicle2));
		}

		[Fact]
		public void Remove_VehicleExists_RemoveVehicle()
		{
			// Arrange
			var garage = new Garage<TestVehicle>(2);
			var vehicle1 = new TestVehicle("ABC123", "Red", 4);
			var vehicle2 = new TestVehicle("DEF456", "Blue", 4);
			garage.Add(vehicle1);
			garage.Add(vehicle2);

			// Act
			garage.Remove("ABC123");

			// Assert
			Assert.DoesNotContain(vehicle1, garage);
		}

		[Fact]
		public void Remove_VehicleDoesNotExist_ThrowKeyNotFoundException()
		{
			// Arrange
			var garage = new Garage<TestVehicle>(2);
			var vehicle1 = new TestVehicle("ABC123", "Red", 4);
			var vehicle2 = new TestVehicle("DEF456", "Blue", 4);
			garage.Add(vehicle1);
			garage.Add(vehicle2);

			// Act + assert
			Assert.Throws<KeyNotFoundException>(() => garage.Remove("GHE789"));
		}

		[Fact]
		public void Search_VehicleExists_ReturnsVehicle()
		{
			// Arrange
			var garage = new Garage<TestVehicle>(2);
			var vehicle1 = new TestVehicle("ABC123", "Red", 4);
			var vehicle2 = new TestVehicle("DEF456", "Blue", 4);
			garage.Add(vehicle1);
			garage.Add(vehicle2);

			// Act
			var result = garage.Search("DEF456");

			// Assert
			Assert.Equal(vehicle2, result); // should be the same object
		}

		[Fact]
		public void Search_VehicleDoesNotExist_ThrowKeyNotFoundException()
		{
			// Arrange
			var garage = new Garage<TestVehicle>(2);
			var vehicle1 = new TestVehicle("ABC123", "Red", 4);
			var vehicle2 = new TestVehicle("DEF456", "Blue", 4);
			garage.Add(vehicle1);
			garage.Add(vehicle2);

			// Act + assert
			Assert.Throws<KeyNotFoundException>(() => garage.Search("GHE789"));
		}

		[Fact]
		public void GetEnumerator_LoopOverGarage_ReturnAllVehicles()
		{
			// Arrange
			var garage = new Garage<TestVehicle>(5);
			var vehicle1 = new TestVehicle("ABC123", "Red", 4);
			var vehicle2 = new TestVehicle("DEF456", "Blue", 4);
			var vehicle3 = new TestVehicle("GHE789", "Green", 4);
			garage.Add(vehicle1);
			garage.Add(vehicle2);
			garage.Add(vehicle3);

			// Act
			garage.Remove("DEF456"); // make sure it works even when we remove the middle index
			var vehicles = new List<TestVehicle>();
			foreach (var vehicle in garage)
			{
				vehicles.Add(vehicle);
			}

			// Assert
			Assert.Contains(vehicle1, vehicles);
			Assert.DoesNotContain(vehicle2, vehicles);
			Assert.Contains(vehicle3, vehicles);
		}
	}
}
