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
			database.CreateTable<Item>();
		}
	}

	public interface ISQLite
	{
		SQLiteConnection GetConnection();
	}
}

