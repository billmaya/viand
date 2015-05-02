using System;
using SQLite.Net.Attributes;

namespace Viand
{
	public class Item : IComparable<Item>
	{
		[PrimaryKey, AutoIncrement, Column("ID")]
		public int ID { get; set; }

		[Column("Name")]
		public string Name { get; private set; }

		[Column("Buy")]
		public bool Buy { get; set; }

		[Column("Quantity")]
		public int Quantity { get; set; }

		[Column("Label")]
		public string Label { get; private set; }

		public Item() {}

		public Item(string name, bool buy, int quantity = 1 ) 
		{
			this.Name = name;
			this.Buy = buy;
			this.Quantity = quantity;
			this.Label = Name[0].ToString(); 
		}
			
		public int CompareTo(Item other) 
		{
			return Name.CompareTo(other.Name);
		}
	}
}

