using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Viand
{
	public class AddPage : ContentPage
	{
		public List<Item> addItems;
		public ListView addView;

		public AddPage()
		{
			Title = "Add";
			Icon = "13-plus.png";

			if (Application.Current.Properties.ContainsKey("Items")) {
				addItems = (List<Item>)Application.Current.Properties["Items"];
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


