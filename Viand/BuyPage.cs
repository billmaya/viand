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

			ToolbarItems.Add(new ToolbarItem {
				Text = "Settings", 
				Order = ToolbarItemOrder.Default,
				Command = new Command(() => Navigation.PushAsync(new SettingsPage())) // Figure this line out
			});

			if (Application.Current.Properties.ContainsKey("Items")) {
				allItems = (List<Item>)Application.Current.Properties["Items"];
				buyItems = allItems.Where(item => item.Buy != false);
			}

			buyView = new ListView {
				RowHeight = 60,
				ItemsSource = buyItems,
				ItemTemplate = new DataTemplate(typeof(BuyCell))
			};

			buyView.ItemTemplate.SetBinding(BuyCell.TextProperty, "Name");

			Content = new StackLayout { 
				VerticalOptions = LayoutOptions.FillAndExpand,
				Children = { buyView }
			};
		}
	}

	internal class BuyCell : TextCell
	{
		public BuyCell()
		{
			var boughtAction = new MenuItem { Text = "Bought", IsDestructive = true };
			var plusOneAction = new MenuItem { Text = "+1" };

			ContextActions.Add(boughtAction);
			ContextActions.Add(plusOneAction);
		}
	}
}


