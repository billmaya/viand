using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Viand
{
	public class BuyPage : ContentPage
	{
		public List<Item> buyItems;
		public ListView buyView;

		public BuyPage()
		{
			Title = "Buy";
			Icon = "19-checkmark.png";

			if (Application.Current.Properties.ContainsKey("Items")) {
				buyItems = (List<Item>)Application.Current.Properties["Items"];
			}

			buyView = new ListView {
				RowHeight = 60,
				ItemsSource = buyItems,
				ItemTemplate = new DataTemplate(typeof(TextCell))
			};

			buyView.ItemTemplate.SetBinding(TextCell.TextProperty, "Name");

			Content = new StackLayout { 
				VerticalOptions = LayoutOptions.FillAndExpand,
				Padding = new Thickness(0, Device.OnPlatform(20, 0, 0), 0, 0),
				Children = { buyView }
			};
		}
	}
}


