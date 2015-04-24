using System;
using SQLite.Net;
using Xamarin.Forms;

namespace Viand
{
	public class ItemDatabase
	{
		private SQLiteConnection database; 

		public ItemDatabase()
		{
			database = DependencyService.Get<ISQLite>().GetConnection();

			if (ItemTableDoesNotExist) {
				database.CreateTable<Item>();
			}
				
		}

		public bool ItemTableDoesNotExist()
		{
			int exists = 0;

			exists = database.Query("SELECT count(*) FROM sqlite_master WHERE type='table' AND name='Item'");
		}
	}

	public interface ISQLite
	{
		SQLiteConnection GetConnection();
	}
}

