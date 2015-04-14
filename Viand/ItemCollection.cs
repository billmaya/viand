using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using System.Collections.ObjectModel;

namespace Viand
{
	public class ItemCollection : ObservableCollection<Item>
	{
		public string Title { get; set; }

		public ItemCollection(string title)
		{
			Title = title;
		}

		public static IEnumerable<Item> GetSortedAddData()
		{
			List<Item> allItems;

			if (Application.Current.Properties.ContainsKey("Items")) {
				allItems = (List<Item>)Application.Current.Properties["Items"];
				allItems.Sort();
				AddPage.addItems = allItems.Where(item => item.Buy != true);
			}

			return AddPage.addItems;
		}
	}
}

