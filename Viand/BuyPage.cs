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

			buyView = new ListView {
				RowHeight = 60,
				ItemTemplate = new DataTemplate(typeof(BuyCell))
			};

			UpdateBuyItemsList();

			buyView.ItemTemplate.SetBinding(BuyCell.TextProperty, "Name");
			buyView.ItemTemplate.SetBinding(BuyCell.DetailProperty, "Quantity");

			Content = new StackLayout { 
				VerticalOptions = LayoutOptions.FillAndExpand,
				Children = { buyView }
			};

			MessagingCenter.Subscribe<BuyCell>(this, "BoughtItem", ItemBought);
			MessagingCenter.Subscribe<BuyCell>(this, "AddOne", ItemQuantityIncreased);
			MessagingCenter.Subscribe<BuyCell>(this, "SubtractOne", ItemQuantityDecreased);
			MessagingCenter.Subscribe<AddPage>(this, "UpdateBuyItemsList", (sender) => UpdateBuyItemsList());
		}

		internal void UpdateBuyItemsList()
		{
			if (Application.Current.Properties.ContainsKey("Items")) {
				allItems = (List<Item>)Application.Current.Properties["Items"];
				buyItems = allItems.Where(item => item.Buy != false);
			}

			buyView.ItemsSource = buyItems;
		}

		internal void ItemBought(BuyCell item) 
		{
			if (allItems != null) {
				var obj = allItems.First(x => x.Name == item.Text);
				if (obj != null) {
					obj.Buy = false;
					obj.Quantity = 1;
				}
			}

			UpdateBuyItemsList();

			MessagingCenter.Send<BuyPage>(this, "UpdateAddItemsList");
		}

		internal void ItemQuantityIncreased(BuyCell item)
		{
			if (allItems != null) {
				var obj = allItems.First(x => x.Name == item.Text);
				if (obj != null) obj.Quantity += 1;
			}

			UpdateBuyItemsList();
		}

		internal void ItemQuantityDecreased(BuyCell item)
		{
			bool alsoUpdateAddList = false;

			if (allItems != null) {
				var obj = allItems.First(x => x.Name == item.Text);
				if (obj != null) {
					if (obj.Quantity != 1) {
						obj.Quantity -= 1;
					} else {
						obj.Quantity = 1;
						obj.Buy = false;
						alsoUpdateAddList = true;

					}
				}
			}

			UpdateBuyItemsList();
			if (alsoUpdateAddList) MessagingCenter.Send<BuyPage>(this, "UpdateAddItemsList");
		}
	}

	internal class BuyCell : TextCell
	{
		public BuyCell()
		{
			var boughtAction = new MenuItem { Text = "Bought", IsDestructive = true };
			boughtAction.Clicked += (sender, e) => MessagingCenter.Send<BuyCell>(this, "BoughtItem");

			var plusOneAction = new MenuItem { Text = "+1" };
			plusOneAction.Clicked += (sender, e) => MessagingCenter.Send<BuyCell>(this, "AddOne");

			var minusOneAction = new MenuItem { Text = "-1" };
			minusOneAction.Clicked += (sender, e) => MessagingCenter.Send<BuyCell>(this, "SubtractOne");

			ContextActions.Add(boughtAction);
			ContextActions.Add(plusOneAction);
			ContextActions.Add(minusOneAction);
		}
	}
}


