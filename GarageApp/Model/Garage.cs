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
				ResizeArray();
			else
				data[count++] = element; // count is increased after the operation has processed
		}

		public void Remove(T element)
		{
			for (int i = 0; i < count; i++)
			{
				if (data[i].Equals(element))
				{
					data[i] = data[count - 1]; // move the last element of the array to current position to avoid a "hole" in the dataset
					data[count - 1] = null; // clear last position to avoid confusion
					count--; // we have now successfully overwritten the element we was looking for, decrease array count
				}
			}
		}

		public T Search(string regNumber)
		{
			for (int i = 0; i < count; i++)
			{
				if (data[i].RegNumber.Equals(regNumber.ToUpper()))
				{
					return data[i];
				}
			}
			return null;
		}

		private bool IsFull()
		{
			if (count == size)
				return true;
			else
				return false;
		}

		private void ResizeArray()
		{
			int newSize = size * 2;
			T[] newArray = new T[newSize];

			// iterate over old array and old size/count (should be the same now)
			for (int i = 0; i < count; i++)
			{
				newArray[i] = data[i]; // copy element by element over
			}

			// rewrite variables
			size = newSize;
			data = newArray;
		}

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
