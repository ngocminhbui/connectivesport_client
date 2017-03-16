using System;
using Newtonsoft.Json;

namespace connectivesport
{
	public class Userf
	{
		public string Id { get; set; }

		[JsonProperty(PropertyName = "text")]
		public string Text { get; set; }

		[JsonProperty(PropertyName = "complete")]
		public bool Complete { get; set; }
	}

	public class UserWrapper : Java.Lang.Object
	{
		public UserWrapper (User item)
		{
			User = item;
		}

		public User User { get; private set; }
	}
}

