using System;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.Sync;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using System.Threading.Tasks;

namespace Viand
{
	public class ItemService
	{
		static ItemService instance = new ItemService();

		const string applicationURL = "https://viand.azure-mobile.net";
		const string applicationKey = "kOTRftIGtybaMmFGuOBtYuKITYvzZW70";
		const string localDbPath = "ItemSQLite.db3";

		private MobileServiceClient client;
		private IMobileServiceSyncTable<Item> itemTable;

		public ItemService()
		{
			
			client = new MobileServiceClient(applicationURL, applicationKey);
			itemTable = client.GetSyncTable<Item>();
		}

		public static ItemService DefaultService { get { return instance; } }

		public async Task InitializeStoreAsync()
		{
			var store = new MobileServiceSQLiteStore(localDbPath);

			await client.SyncContext.InitializeAsync(store);
		}
	}
}

