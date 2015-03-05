using System;

using Xamarin.Forms;

namespace Viand
{
	public class BuyPage : ContentPage
	{
		public BuyPage()
		{
			Title = "Buy";
			Icon = "19-checkmark.png";

			Content = new StackLayout { 
				Padding = new Thickness(0, Device.OnPlatform(20, 0, 0), 0, 0),
				Children = {
					new Label { Text = "Hello BuyPage" }
				}
			};
		}
	}
}


