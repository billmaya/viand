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

//			ToolbarItems.Add(new ToolbarItem {
//				Text = "Settings", 
//				Order = ToolbarItemOrder.Default,
//				Command = new Command(() => Navigation.PushAsync(new SettingsPage())) // Figure this line out
//			});

			buyView = new ListView {
				ItemTemplate = new DataTemplate(typeof(BuyCell))
			};

			UpdateBuyItemsList();

			buyView.ItemTemplate.SetBinding(BuyCell.TextProperty, "Name");
			buyView.ItemTemplate.SetBinding(BuyCell.DetailProperty, "Quantity");

			buyView.IsPullToRefreshEnabled = true;
			buyView.Refreshing += (object sender, EventArgs e) => Device.StartTimer(TimeSpan.FromSeconds(5), SyncItems);

			Content = new StackLayout { 
				VerticalOptions = LayoutOptions.FillAndExpand,
				Children = { buyView }
			};

			MessagingCenter.Subscribe<BuyCell>(this, "BoughtItem", ItemBought);
			MessagingCenter.Subscribe<BuyCell>(this, "AddOne", ItemQuantityIncreased);
			MessagingCenter.Subscribe<AddPage>(this, "UpdateBuyItemsList", (sender) => UpdateBuyItemsList());
			MessagingCenter.Subscribe<AddItemPage>(this, "UpdateBuyItemsListFromAddItems", (sender) => UpdateBuyItemsList());
		}

		internal void UpdateBuyItemsList()
		{
			if (Application.Current.Properties.ContainsKey("Items")) {
				allItems = (List<Item>)Application.Current.Properties["Items"];
				allItems.Sort();
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
				App.Database.UpdateItem((Item)obj);
			}

			UpdateBuyItemsList();
			MessagingCenter.Send<BuyPage>(this, "UpdateAddItemsListFromBuyList");
		}

		internal void ItemQuantityIncreased(BuyCell item)
		{
			if (allItems != null) {
				var obj = allItems.First(x => x.Name == item.Text);
				if (obj != null) obj.Quantity += 1;
			}

			UpdateBuyItemsList();
		}

		internal bool SyncItems()
		{
			App.Database.SyncItems();
			buyView.IsRefreshing = false;

			return false;
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

			ContextActions.Add(plusOneAction);
			ContextActions.Add(boughtAction);
		}
	}

}


