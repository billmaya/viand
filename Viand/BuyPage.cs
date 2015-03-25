using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Viand
{
	public class BuyPage : ContentPage
	{
//		class Item
//		{
//			public string Name { get; set;}
//			public bool Buy { get; set; }
//
//			public Item(string name, bool buy) {
//				this.Name = name;
//				this.Buy = buy;
//			}
//		}

		public BuyPage()
		{
			Title = "Buy";
			Icon = "19-checkmark.png";

			List<Item> items = new List<Item> {
				new Item("Milk", true),
				new Item("Eggs", true),
				new Item("Carrots", true),
				new Item("Raisins", false),
				new Item("Bread", false)
			};

			ListView itemView = new ListView {
				RowHeight = 60,
				ItemsSource = items,
				ItemTemplate = new DataTemplate(typeof(TextCell))
			};

			itemView.ItemTemplate.SetBinding(TextCell.TextProperty, "Name");

			Content = new StackLayout { 
				VerticalOptions = LayoutOptions.FillAndExpand,
				Padding = new Thickness(0, Device.OnPlatform(20, 0, 0), 0, 0),
				Children = { itemView }
			};
		}
	}
}


