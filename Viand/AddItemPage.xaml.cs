using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Viand
{
	public partial class AddItemPage : ContentPage
	{
		private List<Item> allItems;
		private bool addToBuyList;

		public AddItemPage()
		{
			InitializeComponent();
		}

		void OnDestinationSwitchToggled(object sender, EventArgs args)
		{
			if (destinationSwitch.IsToggled == true) {
				destinationLabel.Text = "Save to Add list";
				addToBuyList = false;
			} else {
				destinationLabel.Text = "Save to Buy list";
				addToBuyList = true;
			}
		}

		void OnSaveClicked(object sender, EventArgs args)
		{
			string itemName = TrimAndCapitalize(itemEntry.Text);

			if (NotValidItem(itemName)) return;

			bool itemAlreadyExists = false;

			if (Application.Current.Properties.ContainsKey("Items")) {
				allItems = (List<Item>)Application.Current.Properties["Items"];
				Item newItem = new Item(itemName, addToBuyList);
				itemAlreadyExists = CheckListForExistingItem(newItem);

				if (itemAlreadyExists) {
					DisplayAlert("Duplicate Item", "This item already exists in one of your lists.", "OK");
				} else {
					allItems.Add(newItem);
					itemEntry.Text = "";

					App.Database.AddItem(newItem);
			
					if (!addToBuyList) MessagingCenter.Send<AddItemPage>(this, "UpdateAddItemsListFromAddItems");
					else MessagingCenter.Send<AddItemPage>(this, "UpdateBuyItemsListFromAddItems");
				}
			}
		}

		private bool CheckListForExistingItem(Item item)
		{
			if (allItems.BinarySearch(item) >= 0) return true;
			else return false;
		}

		private string TrimAndCapitalize(string name)
		{
			if (String.IsNullOrWhiteSpace(name)) return String.Empty;

			name = name.Trim(); // Remove any leading and trailing spaces

			name = Char.ToUpper(name[0]) + name.Substring(1); // Capitalize first letter

			// Capitalize any other letter preceded by a space
			for (int i = 1; i < name.Length; i++) {
				if (Char.IsWhiteSpace(name[i - 1])) {
					name = name.Substring(0, i) + Char.ToUpper(name[i]) + name.Substring(i + 1);
				}
			}

			return name;
		}

		private bool NotValidItem(string name)
		{
			if (String.IsNullOrEmpty(name) || name.Length == 0) {
				DisplayAlert("Invalid Item Name", "Only valid item names can be added to the list.", "OK");
				return true;
			}
			else return false;
		}
	}
}

