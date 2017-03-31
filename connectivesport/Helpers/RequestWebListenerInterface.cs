using System;
namespace connectivesport
{
	public interface RequestWebListenerInterface
	{
		void OnWebRequestResult(string responseText,int REQUEST_CODE);
	}
}
