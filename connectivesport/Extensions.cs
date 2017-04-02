using System;
using System.Collections.Generic;

namespace connectivesport
{
	public class Extensions
	{
		public Extensions()
		{
		}


	}
	public class NotificationPayload
	{
		public NotificationPayload()
		{
			Payload = new Dictionary<string, string>();
		}

		public string Action
		{
			get;
			set;
		}

		public Dictionary<string, string> Payload
		{
			get;
			set;
		}
	}
	public class DeviceRegistration
	{
		public string Platform
		{
			get;
			set;
		}

		public string Handle
		{
			get;
			set;
		}

		public string[] Tags
		{
			get;
			set;
		}
	}
}
