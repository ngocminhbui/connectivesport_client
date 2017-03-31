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
	public partial class ChallengeManager
	{
		static ChallengeManager defaultInstance = new ChallengeManager();
		IMobileServiceClient client;
		IMobileServiceSyncTable<Achievement> challengeTable;

		private ChallengeManager()
		{
			this.client = new MobileServiceClient(Constants.ApplicationURL);
			var store = new MobileServiceSQLiteStore("localstore.db");
			store.DefineTable<Achievement>();
			this.client.SyncContext.InitializeAsync(store);
			this.challengeTable = client.GetSyncTable<Achievement>();
		}

		public static ChallengeManager DefaultManager
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
			get { return challengeTable is Microsoft.WindowsAzure.MobileServices.Sync.IMobileServiceSyncTable<Achievement>; }
		}

		public async Task<ObservableCollection<Achievement>> GetAchievementsAsync(bool syncItems = false)
		{
			try
			{
				if (syncItems)
				{
					await this.SyncAsync();
				}
				IEnumerable<Achievement> items = await challengeTable
									.ToEnumerableAsync();

				return new ObservableCollection<Achievement>(items);
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

		public async Task SaveTaskAsync(Achievement item)
		{
			if (item.Id == null)
			{
				await challengeTable.InsertAsync(item);
			}
			else
			{
				await challengeTable.UpdateAsync(item);
			}
		}

		public async Task SyncAsync()
		{
			ReadOnlyCollection<MobileServiceTableOperationError> syncErrors = null;

			try
			{
				await this.client.SyncContext.PushAsync();

				await this.challengeTable.PullAsync(
					//The first parameter is a query name that is used internally by the client SDK to implement incremental sync.
					//Use a different query name for each unique query in your program
					"allAchievements",
					this.challengeTable.CreateQuery());
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
