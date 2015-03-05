using System;

using Xamarin.Forms;

namespace Viand
{
	public class AddPage : ContentPage
	{
		public AddPage()
		{
			Content = new StackLayout { 
				Children = {
					new Label { Text = "Hello AddPage" }
				}
			};
		}
	}
}


