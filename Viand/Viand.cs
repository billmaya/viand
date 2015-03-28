using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Viand
{
	public class App : Application
	{
		public List<Item> allItems;

		public App()
		{
			allItems = GetSampleData();
			this.Properties["Items"] = allItems;

			// The root page of your application
			MainPage = new TabPage();
		}

		protected override void OnStart()
		{
			// Handle when your app starts
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}

		public List<Item> GetSampleData() {
			return new List<Item> {
				new Item("Milk", true),
				new Item("Eggs", true),
				new Item("Carrots", true),
				new Item("Raisins", false),
				new Item("Bread", false)
			};
		}
	}
}

