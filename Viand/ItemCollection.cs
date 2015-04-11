using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace Viand
{
	public class ItemCollection
	{
		public string Title { get; set; }

		public ItemCollection(string title)
		{
			Title = title;
		}

		public static IEnumerable<Item> GetSortedData()
		{
			List<Item> allItems;
			IEnumerable<Item> addItems;

			if (Application.Current.Properties.ContainsKey("Items")) {
				allItems = (List<Item>)Application.Current.Properties["Items"];
				addItems = allItems.Where(item => item.Buy != true);
			}

			addItems.OrderBy(x => x.Name);

			return addItems;
		}
	}
}

