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
				Order = ToolbarItemOrder.Default
			});

			if (Application.Current.Properties.ContainsKey("Items")) {
				allItems = (List<Item>)Application.Current.Properties["Items"];
				addItems = allItems.Where(item => item.Buy != true);
			}

			addView = new ListView {
				RowHeight = 60,
				ItemsSource = addItems,
				ItemTemplate = new DataTemplate(typeof(AddCell))
			};

			addView.ItemTemplate.SetBinding(AddCell.TextProperty, "Name");

			Content = new StackLayout {
				VerticalOptions = LayoutOptions.FillAndExpand,
				Children = { addView }
			};
		}
	}

	internal class AddCell : TextCell
	{
		public AddCell()
		{
			var buyAction = new MenuItem { Text = "Buy" };
			var removeAction = new MenuItem { Text = "Remove", IsDestructive = true };

			ContextActions.Add(removeAction);
			ContextActions.Add(buyAction);
		}
	}
}


