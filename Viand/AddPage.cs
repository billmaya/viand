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
			Icon = "187-pencil.png";

			ToolbarItems.Add(new ToolbarItem {
				Text = "Add",
//				Icon = "187-pencil.png",
				Order = ToolbarItemOrder.Default
			});

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
				Children = { addView }
			};
		}
	}
}


