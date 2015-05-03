using System;
using Xamarin.Forms;
using Viand.Droid;
using SQLite.Net;
using System.IO;

[assembly: Dependency(typeof(SQLite_Android))]

namespace Viand.Droid
{
	public class SQLite_Android : ISQLite
	{
		const string sqliteFilename = "ItemSQLite.db3";

		public SQLite_Android() {}

		public SQLiteConnection GetConnection()
		{
			string documentsPath;
			string databasePath;

			documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
			databasePath = Path.Combine(documentsPath, sqliteFilename);

			CopyDatabaseIfNotExist(databasePath);

			var platform = new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid();
			var connection = new SQLite.Net.SQLiteConnection(platform, databasePath);

			return connection;
		}

		private static void CopyDatabaseIfNotExist(string databasePath)
		{
			if (!File.Exists(databasePath)) {
				using (var br = new BinaryReader(Android.App.Application.Context.Assets.Open(sqliteFilename))) {
					using (var bw = new BinaryWriter(new FileStream(databasePath, FileMode.Create))) {
						byte[] buffer = new byte[2048];
						int length = 0;
						while ((length = br.Read(buffer, 0, buffer.Length)) > 0) {
							bw.Write(buffer, 0, length);
						}
					}
				}
			}
		}
	}
}
