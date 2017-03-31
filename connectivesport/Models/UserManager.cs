using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;

namespace connectivesport
{
	public partial class UserManager
	{
		static UserManager defaultInstance = new UserManager();
		IMobileServiceClient client;
		IMobileServiceSyncTable<User> userTable;

		private UserManager()
		{
			this.client = new MobileServiceClient(Constants.ApplicationURL);
			var store = new MobileServiceSQLiteStore("localstore.db");
			store.DefineTable<User>();
			this.client.SyncContext.InitializeAsync(store);
			this.userTable = client.GetSyncTable<User>();
		}

		public static UserManager DefaultManager
		{
			get
			{
				return defaultInstance;
			}
			private set
			{
				defaultInstance = value;
			}
		}

		public IMobileServiceClient CurrentClient
		{
			get { return client; }
		}

		public bool IsOfflineEnabled
		{
			get { return userTable is Microsoft.WindowsAzure.MobileServices.Sync.IMobileServiceSyncTable<User>; }
		}

		public async Task<ObservableCollection<User>> GetUsersAsync(bool syncItems = false)
		{
			try
			{
				if (syncItems)
				{
					await this.SyncAsync();
				}
				IEnumerable<User> items = await userTable
									.ToEnumerableAsync();

				return new ObservableCollection<User>(items);
			}
			catch (MobileServiceInvalidOperationException msioe)
			{
				Debug.WriteLine(@"Invalid sync operation: {0}", msioe.Message);
			}
			catch (Exception e)
			{
				Debug.WriteLine(@"Sync error: {0}", e.Message);
			}
			return null;
		}

		public async Task SaveTaskAsync(User item)
		{
			if (item.Id == null)
			{
				await userTable.InsertAsync(item);
			}
			else
			{
				await userTable.UpdateAsync(item);
			}
		}

		public async Task SyncAsync()
		{
			ReadOnlyCollection<MobileServiceTableOperationError> syncErrors = null;

			try
			{
				await this.client.SyncContext.PushAsync();

				await this.userTable.PullAsync(
					//The first parameter is a query name that is used internally by the client SDK to implement incremental sync.
					//Use a different query name for each unique query in your program
					"allUsers",
					this.userTable.CreateQuery());
			}
			catch (MobileServicePushFailedException exc)
			{
				if (exc.PushResult != null)
				{
					syncErrors = exc.PushResult.Errors;
				}
			}

			// Simple error/conflict handling. A real application would handle the various errors like network conditions,
			// server conflicts and others via the IMobileServiceSyncHandler.
			if (syncErrors != null)
			{
				foreach (var error in syncErrors)
				{
					if (error.OperationKind == MobileServiceTableOperationKind.Update && error.Result != null)
					{
						//Update failed, reverting to server's copy.
						await error.CancelAndUpdateItemAsync(error.Result);
					}
					else
					{
						// Discard local change.
						await error.CancelAndDiscardItemAsync();
					}

					Debug.WriteLine(@"Error executing sync operation. Item: {0} ({1}). Operation discarded.", error.TableName, error.Item["id"]);
				}
			}
		}
	}
}
