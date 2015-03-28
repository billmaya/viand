using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace Viand
{
	public class AddPage : ContentPage
	{
		private List<Item> allItems;
		private IEnumerable<Item> addItems;
		private ListView addView;

		public AddPage()
		{
			Title = "Add";
			Icon = "13-plus.png";

			if (Application.Current.Properties.ContainsKey("Items")) {
				allItems = (List<Item>)Application.Current.Properties["Items"];
				addItems = allItems.Where(item => item.Buy != true);
			}

			addView = new ListView {
				RowHeight = 60,
				ItemsSource = addItems,
				ItemTemplate = new DataTemplate(typeof(TextCell))
			};

			addView.ItemTemplate.SetBinding(TextCell.TextProperty, "Name");

			Content = new StackLayout {
				VerticalOptions = LayoutOptions.FillAndExpand,
				Padding = new Thickness(0, Device.OnPlatform(20, 0, 0), 0, 0),
				Children = { addView }
			};
		}
	}
}


