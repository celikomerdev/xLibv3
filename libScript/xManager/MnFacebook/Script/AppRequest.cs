#if xLibv2
#if Facebook
using System.Collections.Generic;
using Facebook.Unity;
using UnityEngine;

namespace xLib.xFacebook
{
	public class AppRequest : BaseActiveM
	{
		public void Call()
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":Call");
			FB.AppRequest
			(
				"Here is a free gift!",
				null,
				new List<object>(){"app_users"},
				null,
				null,
				null,
				null,
				Callback
			);
		}
		
		private void Callback(IAppRequestResult result)
		{
			if(string.IsNullOrEmpty(result.RawResult)) return;
			if(CanDebug) Debug.LogFormat(this,this.name+":Callback:{0}",result.ToString());
		}
	}
}
#else
namespace xLib.xFacebook
{
	public class AppRequest : BaseActiveM
	{
		public void Call(){}
	}
}
#endif
#endif



// #if xLibv2
// #if Facebook
// using System.Collections.Generic;
// using System.Linq;
// using Facebook.Unity;
// using UnityEngine;

// namespace xLib.xFacebook
// {
// 	public class AppRequest : BaseActiveM
// 	{
// 		public string message;
// 		public OGActionType actionType;
// 		public string objectId;
		
// 		//Specific
// 		public List<string> to;
		
// 		//General
// 		public List<string> filters; //app_users app_non_users
// 		public List<string> excludeIds;
// 		public int maxRecipients;
		
// 		public string data;
// 		public string title;
		
// 		public void OnClick()
// 		{
// 			FB.AppRequest
// 			(
// 				message,
// 				actionType,
// 				string.IsNullOrEmpty(objectId)? null:objectId,
// 				filters.Cast<object>().ToList(),
// 				excludeIds,
// 				maxRecipients,
// 				data,
// 				title,
// 				Callback
// 			);
// 		}
		
// 		public void AppRequestGet()
// 		{
// 			FB.API("/me/apprequests",HttpMethod.GET,Callback);
// 		}
		
// 		protected void Callback(IResult result)
// 		{
// 			if(string.IsNullOrEmpty(result.RawResult)) return;
// 			if(CanDebug) Debug.LogFormat(this,this.name+":Callback:{0}",result.ToString());
// 		}
// 	}
// }


// public class AppRequest : BaseM
// {
// 	public string message;
	
// 	public bool isAction;
// 	public OGActionType actionType;
	
// 	public RequestType requestType;
// 	public enum RequestType
// 	{
// 		Invite = 0,
// 		Turn = 1,
// 		Send = 2,
// 		Ask = 3
// 	}
	
// 	public string objectId; //The Open Graph object ID of the object being sent. //Send - Request
	
// 	//Specific
// 	public List<string> to;
	
	
// 	//General
// 	public List<string> filters; //app_users app_non_users
// 	public List<string> excludeIds;
// 	public int maxRecipients;
	
// 	public string data;
// 	public string title;
	
// 	public void OnClick()
// 	{
// 		switch(requestType)
// 		{
// 			case RequestType.Invite:
// 				Invite();
// 				break;
// 			case RequestType.Turn:
// 				Invite();
// 				break;
// 			case RequestType.Send:
// 				Invite();
// 				break;
// 			case RequestType.Ask:
// 				Invite();
// 				break;
// 		}
// 		FB.AppRequest
// 		(
// 			message,
// 			actionType,
// 			string.IsNullOrEmpty(objectId)? null:objectId,
// 			filters.Cast<object>().ToList(),
// 			excludeIds,
// 			maxRecipients,
// 			data,
// 			title,
// 			Callback
// 		);
		
// 		#region Options
// 		/*
// 		public static void AppRequest(
// 		string message,
// 		OGActionType actionType,
// 		string objectId,
// 		IEnumerable<string> to,
// 		string data = "",
// 		string title = "",
// 		FacebookDelegate<IAppRequestResult> callback = null);
		
		
// 		public static void AppRequest(
// 		string message,
// 		OGActionType actionType,
// 		string objectId,
// 		IEnumerable<object> filters = null,
// 		IEnumerable<string> excludeIds = null,
// 		int? maxRecipients = null,
// 		string data = "",
// 		string title = "",
// 		FacebookDelegate<IAppRequestResult> callback = null);
		
		
// 		public static void AppRequest(
// 		string message,
// 		IEnumerable<string> to = null,
// 		IEnumerable<object> filters = null,
// 		IEnumerable<string> excludeIds = null,
// 		int? maxRecipients = null,
// 		string data = "",
// 		string title = "",
// 		FacebookDelegate<IAppRequestResult> callback = null);
// 		*/
// 		#endregion
// 	}
	
// 	private void Invite()
// 	{
// 		Debug.Log("Invite");
// 	}
	
// 	private void Turn()
// 	{
// 		Debug.Log("Turn");
// 	}
	
// 	private void Send()
// 	{
// 		Debug.Log("Send");
// 	}
	
// 	private void Ask()
// 	{
// 		Debug.Log("Ask");
// 	}
	
	
// 	protected void Callback(IResult result)
// 	{
// 		if(string.IsNullOrEmpty(result.RawResult)) return;
// 		Debug.Log(result.ToString());
		
// 		// if (result == null)
// 		// {
// 		// 	this.LastResponse = "Null Response\n";
// 		// 	LogView.AddLog(this.LastResponse);
// 		// 	return;
// 		// }
		
// 		// this.LastResponseTexture = null;
		
// 		// // Some platforms return the empty string instead of null.
// 		// if (!string.IsNullOrEmpty(result.Error))
// 		// {
// 		// 	this.Status = "Error - Check log for details";
// 		// 	this.LastResponse = "Error Response:\n" + result.Error;
// 		// }
// 		// else if (result.Cancelled)
// 		// {
// 		// 	this.Status = "Cancelled - Check log for details";
// 		// 	this.LastResponse = "Cancelled Response:\n" + result.RawResult;
// 		// }
// 		// else if (!string.IsNullOrEmpty(result.RawResult))
// 		// {
// 		// 	this.Status = "Success - Check log for details";
// 		// 	this.LastResponse = "Success Response:\n" + result.RawResult;
// 		// }
// 		// else
// 		// {
// 		// 	this.LastResponse = "Empty Response\n";
// 		// }
		
// 		// LogView.AddLog(result.ToString());
// 	}
// }