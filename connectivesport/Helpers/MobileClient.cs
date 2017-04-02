using System;
using Microsoft.WindowsAzure.MobileServices;

namespace connectivesport
{
	public class MobileClient
	{
		public static MobileServiceClient client = new MobileServiceClient(Constants.ApplicationURL);

		public MobileClient()
		{
		}

		public static MobileServiceClient getclient()
		{
			return client;
		}
	}
}
