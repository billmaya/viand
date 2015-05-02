using System;
using Xamarin.Forms;
using SQLite.Net;
using UIKit;
using Foundation;
using System.IO;
using Viand.iOS;

[assembly: Dependency(typeof(SQLite_iOS))]

namespace Viand.iOS
{
	public class SQLite_iOS : ISQLite
	{
		const string sqliteFilename = "ItemSQLite";

		public SQLite_iOS() {}

		public SQLiteConnection GetConnection()
		{
			string documentPath;
			string libraryPath;

			if (UIDevice.CurrentDevice.CheckSystemVersion(8, 0)) {
				var documentUrl = NSFileManager.DefaultManager.GetUrls(NSSearchPathDirectory.DocumentDirectory, NSSearchPathDomain.User)[0];
				documentPath = documentUrl.Path;
			} else {
				documentPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
			}

			libraryPath = Path.Combine(documentPath, "..", "Library", "Databases");

			if (!Directory.Exists(libraryPath)) {
				Directory.CreateDirectory(libraryPath);
			}
				
			var databasePath = Path.Combine(libraryPath, sqliteFilename);

			CopyDatabaseIfNotExists(databasePath);

			var platform = new SQLite.Net.Platform.XamarinIOS.SQLitePlatformIOS();
			var connection = new SQLite.Net.SQLiteConnection(platform, databasePath);

			return connection;
		}

		private static void CopyDatabaseIfNotExists(string databasePath)
		{
			if (!File.Exists(databasePath)) {
				var existingDatabase = NSBundle.MainBundle.PathForResource(sqliteFilename, "db3");
				File.Copy(existingDatabase, databasePath);
			}
		}
			
	}
}

