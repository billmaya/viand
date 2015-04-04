using System;

namespace Viand
{
	public class Item
	{
		public string Name { get; set; }
		public bool Buy { get; set; }
		public int? Quantity { get; set; }

		public Item(string name, bool buy, int quantity = 1 ) {
			this.Name = name;
			this.Buy = buy;
			this.Quantity = quantity;
		}
	}
}

