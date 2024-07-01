using GarageApp.Model.Vehicles;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageApp.Model
{
	public class Garage<T> : IEnumerable<T> where T : Vehicle
	{
		// Variables
		private T[] data;
		private int count;
		private int size;

		// Constructor
		public Garage(int size)
		{
			data = new T[size];
			count = 0;
			this.size = size;
		}

		// Operation methods
		// Add element to the array, if it is not full.
		public void Add(T element)
		{
			if (IsFull())
				throw new InvalidOperationException("Garage is full");
			else
			{
				var index = Array.IndexOf(data, null);
				data[index] = element; // count is increased after the operation has processed
			}
		}

		// Attempts to remove one element from the array if it is found
		// It will take the last element and move in place of the one we are removing, clearing the latest space
		// This will make sure the array does not contain any "holes"
		public void Remove(string regNumber)
		{
			for (int i = 0; i < data.Length; i++)
			{
				if (data[i] != null && data[i].RegNumber.Equals(regNumber))
				{
					//data[i] = data[count - 1]; // move the last element of the array to current position to avoid a "hole" in the dataset
					data[i] = null!; // clear last position to avoid confusion
					count--; // we have now successfully overwritten the element we was looking for, decrease array count
					return;
				}
			}
			throw new KeyNotFoundException("Did not find any Vehicle with that registration number");
		}

		// Will return Vehicle object if we find an object that matches registration number
		public T Search(string regNumber)
		{
			for (int i = 0; i < count; i++)
			{
				if (data[i].RegNumber.Equals(regNumber, StringComparison.OrdinalIgnoreCase))
				{
					return data[i];
				}
			}
			throw new KeyNotFoundException("Did not find any Vehicle with that registration number");
		}

		// Checking whether the datastructure is expected to be full
		public bool IsFull() => count == size;

		// The GetEnumerator code will generate some problem according to dimitris
		// Answer: I cant see the problem here, we will not go out of bounce, we should not encounter null
		public IEnumerator<T> GetEnumerator()
		{
			for (int i = 0; i < data.Length; i++)
			{
				if (data[i] != null)
					yield return data[i];
			}
		}

		// Send to GetEnumerator() function
		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}
