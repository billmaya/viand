using System;

using Xamarin.Forms;

namespace Viand
{
	public class AddPage : ContentPage
	{
		public AddPage()
		{
			Title = "Add";
			Icon = "13-plus.png";

			Content = new StackLayout { 
				Padding = new Thickness(0, Device.OnPlatform(20, 0, 0), 0, 0),
				Children = {
					new Label { Text = "Hello AddPage" }
				}
			};
		}
	}
}


