using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace Viand
{
	public class BuyPage : ContentPage
	{
		private List<Item> allItems;
		private IEnumerable<Item> buyItems;
		private ListView buyView;

		public BuyPage()
		{
			Title = "Buy";
			Icon = "117-todo.png";

			if (Application.Current.Properties.ContainsKey("Items")) {
				allItems = (List<Item>)Application.Current.Properties["Items"];
				buyItems = allItems.Where(item => item.Buy != false);
			}

			buyView = new ListView {
				RowHeight = 60,
				ItemsSource = buyItems,
				ItemTemplate = new DataTemplate(typeof(TextCell))
			};

			buyView.ItemTemplate.SetBinding(TextCell.TextProperty, "Name");

			Content = new StackLayout { 
				VerticalOptions = LayoutOptions.FillAndExpand,
				Children = { buyView }
			};
		}
	}
}


