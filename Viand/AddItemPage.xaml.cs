using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Viand
{
	public partial class AddItemPage : ContentPage
	{
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
			if (Application.Current.Properties.ContainsKey("Items")) {
				List<Item> allItems = (List<Item>)Application.Current.Properties["Items"];
				allItems.Add(new Item(itemEntry.Text, addToBuyList));
				itemEntry.Text = "";
			}

			if (!addToBuyList) MessagingCenter.Send<AddItemPage>(this, "UpdateAddItemsListFromAddItems");
			else MessagingCenter.Send<AddItemPage>(this, "UpdateBuyItemsListFromAddItems");
		}
	}
}

