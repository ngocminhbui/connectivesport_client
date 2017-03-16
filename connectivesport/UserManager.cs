using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;

namespace connectivesport
{
	public partial class UserManager
	{
		static UserManager defaultInstance = new UserManager();
		MobileServiceClient client;
		IMobileServiceTable<User> userTable;
		const string ApplicationURL = @"https://connectivesport.azurewebsites.net";


		private UserManager()
		{
			this.client = new MobileServiceClient(ApplicationURL);
			this.userTable = client.GetTable<User>();
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

		public MobileServiceClient CurrentClient
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
	}
}