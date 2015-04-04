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
			MainPage = new NavigationPage(new TabPage());
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
				new Item("Eggs", true, 2),
				new Item("Carrots", true),
				new Item("Raisins", false),
				new Item("Bread", false),
				new Item("Ice Cream", true),
				new Item("Napkins", true),
				new Item("Pecans", true),
				new Item("Broccoli", true),
				new Item("Hummus", false),
				new Item("Ketchup", false),
				new Item("Chocolate", false),
				new Item("Granola", false),
				new Item("Half and Half", false),
				new Item("Orange Juice", true),
				new Item("Apples", true),
				new Item("Lettuce", true),
				new Item("Walnuts", true),
				new Item("Tea", false),
				new Item("American Cheese", false),
				new Item("Bananas", false),
				new Item("Cucumbers", false),
				new Item("Tomato", false),
				new Item("Peanut Butter", false),
				new Item("Fruit Salad", false)
			};
		}
	}
}

