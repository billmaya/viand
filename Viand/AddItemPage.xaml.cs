using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Viand
{
	public partial class AddItemPage : ContentPage
	{
		public AddItemPage()
		{
			InitializeComponent();
		}

		void OnDestinationSwitchToggled(object sender, EventArgs args)
		{
			if (destinationSwitch.IsToggled == true) {
				destinationLabel.Text = "Save to Add list";
			} else {
				destinationLabel.Text = "Save to Buy list";
			}
		}
	}
}

