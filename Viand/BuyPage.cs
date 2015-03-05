using System;

using Xamarin.Forms;

namespace Viand
{
	public class BuyPage : ContentPage
	{
		public BuyPage()
		{
			Content = new StackLayout { 
				Children = {
					new Label { Text = "Hello BuyPage" }
				}
			};
		}
	}
}


