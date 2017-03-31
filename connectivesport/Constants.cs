
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace connectivesport
{
	public class Constants 
	{
		public const string SenderID = "25395899329"; // Google API Project Number
		public const string ListenConnectionString = "Endpoint=sb://connectivesportservice.servicebus.windows.net/;SharedAccessKeyName=DefaultListenSharedAccessSignature;SharedAccessKey=xwyuOVI8i6gli+8gHATttBeBLc7k+83srfdjJJfy898=";
		public const string NotificationHubName = "ConnectiveSportNotificationHub";
		public const string ApplicationURL = @"https://connectivesport.azurewebsites.net";
	}
}
