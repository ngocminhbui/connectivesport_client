using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.CSharp;

namespace connectivesport
{
	public class RequestWeb
	{
		string requestURL;
		RequestWebListenerInterface onresult;
		public RequestWeb(string request, RequestWebListenerInterface onresult)
		{
			this.requestURL= request;
			this.onresult = onresult;
		}

		public async Task<string> Execute(int REQUEST_CODE)
		{
			string url = this.requestURL;
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
			request.Method = "GET";

			request.ContentType = "application/json";
			HttpWebResponse myResp = (HttpWebResponse)request.GetResponse();
			string responseText;

			using (var response = request.GetResponse())
			{
				using (var reader = new StreamReader(response.GetResponseStream()))
				{
					responseText = reader.ReadToEnd();


					dynamic results = JsonConvert.DeserializeObject<dynamic>(responseText);
					var id = results.Id;
					var name = results.Name;

					onresult.OnWebRequestResult(responseText,REQUEST_CODE);


					return responseText;

				}
			}
		}
	}
}
