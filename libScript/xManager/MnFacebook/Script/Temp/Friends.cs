#if xLibv2
#if Facebook
using System.Collections.Generic;
using Facebook.Unity;
using UnityEngine;

namespace xLib.xFacebook
{
	public class Friends : BaseActiveM
	{
		[SerializeField]private string query = "/me/friends";
		public void Call()
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":Call");
			FB.API(query,HttpMethod.GET,Callback);
		}
		
		private void Callback(IGraphResult result)
		{
			if(string.IsNullOrEmpty(result.RawResult)) return;
			if(CanDebug) Debug.LogFormat(this,this.name+":Callback:{0}",result.ToString());
			
			// var dictionary = (Dictionary<string,object>)Facebook.MiniJSON.Json.Deserialize(result.RawResult);
			// var friendsList = (List<object>)dictionary["data"];
			
			// foreach (Dictionary<string,object> item in friendsList)
			// {
			// 	if(CanDebug) Debug.LogFormat(this,this.name+":Friend:{0}",item["name"]);
			// }
		}
	}
}
#endif
#endif