using System;

namespace Viand
{
	public class Item
	{
		public string Name { get; set; }
		public bool Buy { get; set; }

		public Item(string name, bool buy) {
			this.Name = name;
			this.Buy = buy;
		}
	}
}

