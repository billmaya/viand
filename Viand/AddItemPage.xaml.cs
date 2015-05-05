﻿using System;
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
			name = name.Trim();

			name = Char.ToUpper(name[0]) + name.Substring(1);

			return name;
		}

		private bool NotValidItem(string name)
		{
			if (String.IsNullOrEmpty(name) || name.Length == 0) return true;
			else return false;
		}
	}
}

