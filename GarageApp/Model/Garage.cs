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
	internal class Garage<T> : IEnumerable<T> where T : Vehicle
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

		// Methods
		public void Add(T element)
		{
			if (IsFull())
				throw new InvalidOperationException("Garage is full");
			else
				data[count++] = element; // count is increased after the operation has processed
		}

		public void Remove(string regNumber)
		{
			for (int i = 0; i < count; i++)
			{
				if (data[i].RegNumber.Equals(regNumber))
				{
					data[i] = data[count - 1]; // move the last element of the array to current position to avoid a "hole" in the dataset
					data[count - 1] = null; // clear last position to avoid confusion
					count--; // we have now successfully overwritten the element we was looking for, decrease array count
					return;
				}
			}
			throw new KeyNotFoundException("Did not find any Vehicle with that registration number");
		}

		public T Search(string regNumber)
		{
			for (int i = 0; i < count; i++)
			{
				if (data[i].RegNumber.Equals(regNumber))
				{
					return data[i];
				}
			}
			throw new KeyNotFoundException("Did not find any Vehicle with that registration number");
		}

		private bool IsFull()
		{
			if (count == size)
				return true;
			else
				return false;
		}

		// todo: this will generate some problem according to dimitris
		public IEnumerator<T> GetEnumerator()
		{
			for (int i = 0; i < count; i++)
			{
				yield return data[i];
			}
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}
