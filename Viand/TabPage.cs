using System;

using Xamarin.Forms;

namespace Viand
{
	public class TabPage : TabbedPage
	{
		public TabPage()
		{
//			Title = "Viand";
			Children.Add(new BuyPage());
			Children.Add(new AddPage());
		}
	}
}


