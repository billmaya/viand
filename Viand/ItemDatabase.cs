using System;
using SQLite.Net;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Linq;

namespace Viand
{
	public class ItemDatabase
	{
		private SQLiteConnection database; 

		public ItemDatabase()
		{
			database = DependencyService.Get<ISQLite>().GetConnection();
		}

		public IEnumerable<Item> GetItems()
		{
			return (from i in database.Table<Item>() select i).ToList();
		}

		public void AddItem(Item item)
		{
			database.Insert(item);
		}

		public void RemoveItem(Item item)
		{
			database.Delete<Item>(item.ID);
		}

		public void UpdateItem(Item item)
		{
			database.Update(item);
		}

		public void SyncItems()
		{
			// Sync code here
		}
	}

	public interface ISQLite
	{
		SQLiteConnection GetConnection();
	}

}

