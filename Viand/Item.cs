using System;

namespace Viand
{
	public class Item : IComparable<Item>
	{
		public string Name { get; private set; }
		public bool Buy { get; private set; }
		public int Quantity { get; private set; }
		public string Label { get; private set; }

		public Item(string name, bool buy, int quantity = 1 ) 
		{
			this.Name = name;
			this.Buy = buy;
			this.Quantity = quantity;
			this.Label = Name[0].ToString; 
		}
			
		public int CompareTo(Item other) 
		{
			return Name.CompareTo(other.Name);
		}
	}
}

