using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using System.Collections.ObjectModel;

namespace Viand
{
	public class AddPage : ContentPage
	{
		internal List<Item> allItems;
		internal static IEnumerable<Item> addItems;
		private ListView addView;

		public AddPage()
		{
			Title = "Add";
			Icon = "187-pencil.png";

			ToolbarItems.Add(new ToolbarItem {
				Text = "Add Item",
				Order = ToolbarItemOrder.Default,
				Command = new Command(() => Navigation.PushAsync(new AddItemPage()))
			});
					
			addView = new ListView {
				ItemTemplate = new DataTemplate(typeof(AddCell)) { Bindings =  {{ AddCell.TextProperty, new Binding("Name") }}},
				IsGroupingEnabled = true,
				GroupDisplayBinding = new Binding("Title"),
				GroupShortNameBinding = new Binding("Title")
			};
					
			addView.ItemsSource = UpdateAddItemsList();

			Content = new StackLayout {
				VerticalOptions = LayoutOptions.FillAndExpand,
				Children = { addView }
			};

			MessagingCenter.Subscribe<AddCell>(this, "BuyItem", BuyItem);
			MessagingCenter.Subscribe<AddCell>(this, "RemoveItem", RemoveItem);
			MessagingCenter.Subscribe<BuyPage>(this, "UpdateAddItemsListFromBuyList", (sender) => UpdateAddItemsListFromBuyList());
		}

		internal void BuyItem(AddCell item)
		{
			if (Application.Current.Properties.ContainsKey("Items")) {
				allItems = (List<Item>)Application.Current.Properties["Items"];
				var obj = allItems.First(x => x.Name == item.Text);
				if (obj != null) obj.Buy = true;
			}
				
			addView.ItemsSource = UpdateAddItemsList();
			MessagingCenter.Send<AddPage>(this, "UpdateBuyItemsList");
		}

		internal void RemoveItem(AddCell item)
		{
			if (Application.Current.Properties.ContainsKey("Items")) {
				allItems = (List<Item>)Application.Current.Properties["Items"];
				var obj = allItems.First(x => x.Name == item.Text);
				allItems.Remove(obj);
			}

			addView.ItemsSource = UpdateAddItemsList();
		}

		internal ObservableCollection<ItemCollection> UpdateAddItemsList()
		{
			var allAddItemGroups = new ObservableCollection<ItemCollection>();

			foreach (Item item in ItemCollection.GetSortedAddData())
			{
				var addItemGroup = allAddItemGroups.FirstOrDefault(g => g.Title == item.Label);

				if (addItemGroup == null)
				{
					addItemGroup = new ItemCollection(item.Label);
					addItemGroup.Add(item);
					allAddItemGroups.Add(addItemGroup);
				}
				else
				{
					addItemGroup.Add(item);
				}
			}
			return allAddItemGroups;
		}

		internal void UpdateAddItemsListFromBuyList()
		{
			addView.ItemsSource = UpdateAddItemsList();
		}
	}

	internal class AddCell : TextCell
	{
		public AddCell()
		{
			var buyAction = new MenuItem { Text = "Buy", IsDestructive = true };
			buyAction.Clicked += (sender, e) => MessagingCenter.Send<AddCell>(this, "BuyItem");

			var removeAction = new MenuItem { Text = "Remove" };
			removeAction.Clicked += (sender, e) => MessagingCenter.Send<AddCell>(this, "RemoveItem");

			ContextActions.Add(removeAction);
			ContextActions.Add(buyAction);
		}
	}
}


