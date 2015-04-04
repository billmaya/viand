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
					
			addView = new ListView {
				RowHeight = 60,
				ItemTemplate = new DataTemplate(typeof(AddCell))
			};

			UpdateAddItemsList();

			addView.ItemTemplate.SetBinding(AddCell.TextProperty, "Name");

			Content = new StackLayout {
				VerticalOptions = LayoutOptions.FillAndExpand,
				Children = { addView }
			};

			MessagingCenter.Subscribe<AddCell>(this, "BuyItem", BuyItem);
			MessagingCenter.Subscribe<AddCell>(this, "RemoveItem", RemoveItem);

			MessagingCenter.Subscribe<BuyPage>(this, "UpdateAddItemsList", (sender) => UpdateAddItemsList());
		}

		internal void UpdateAddItemsList()
		{
			if (Application.Current.Properties.ContainsKey("Items")) {
				allItems = (List<Item>)Application.Current.Properties["Items"];
				addItems = allItems.Where(item => item.Buy != true);
			}

			addView.ItemsSource = addItems;
		}

		internal void BuyItem(AddCell item)
		{
			if (allItems != null) {
				var obj = allItems.First(x => x.Name == item.Text);
				if (obj != null) obj.Buy = true;
			}

			UpdateAddItemsList();

			MessagingCenter.Send<AddPage>(this, "UpdateBuyItemsList");
		}

		internal void RemoveItem(AddCell item)
		{
			if (allItems != null) {
				var obj = allItems.First(x => x.Name == item.Text);
				allItems.Remove(obj);
			}

			UpdateAddItemsList();
		}
	}

	internal class AddCell : TextCell
	{
		public AddCell()
		{
			var buyAction = new MenuItem { Text = "Buy" };
			buyAction.Clicked += (sender, e) => MessagingCenter.Send<AddCell>(this, "BuyItem");

			var removeAction = new MenuItem { Text = "Remove", IsDestructive = true };
			removeAction.Clicked += (sender, e) => MessagingCenter.Send<AddCell>(this, "RemoveItem");

			ContextActions.Add(removeAction);
			ContextActions.Add(buyAction);
		}
	}
}


