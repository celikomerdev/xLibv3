#if DiscardxLibv2
using System;
using GameSparks.Api.Messages;
using GameSparks.Api.Requests;
using GameSparks.Api.Responses;
using GameSparks.Core;
using UnityEngine;

namespace xLib.xNew
{
	public class TempGameSpark : BaseM
	{
		#region Save
		public void SaveCall()
		{
			Debug.Log(this.name+": SaveCall: ",this);
			
			LogEventRequest requestSave = new LogEventRequest();
			requestSave.SetMaxResponseTimeInMillis(3000);
			
			requestSave.SetEventKey("PrivateDataSet");
			requestSave.SetEventAttribute("key","keyVariable");
			
			GSRequestData data = new GSRequestData("variableJsonData");
			data.AddString("dataKey","variableKeyData");
			data.AddString("dataValue","variableValueData");
			requestSave.SetEventAttribute("value",data);
			
			requestSave.Send(SaveCallback);
		}
		
		private void SaveCallback(LogEventResponse response)
		{
			Debug.Log(this.name+": SaveCallback: ",this);
			//TODO Handle exceptions
		}
		#endregion
		
		
		#region Load
		public void LoadCall()
		{
			Debug.Log(this.name+": LoadCall: ",this);
			
			LogEventRequest requestSave = new LogEventRequest();
			requestSave.SetMaxResponseTimeInMillis(3000);
			
			requestSave.SetEventKey("PrivateDataGet");
			requestSave.SetEventAttribute("key","keyVariable");
			
			requestSave.Send(LoadCallback);
		}
		
		private void LoadCallback(LogEventResponse response)
		{
			Debug.Log(this.name+": LoadCallback: ",this);
			//TODO Handle exceptions
		}
		#endregion
		
		
		
		private void Test()
		{
			GameSparks.Api.GSMessageHandler._AllMessages = ((GSMessage message) => { Debug.Log("ALL HANDLER " + message.MessageId);});
			
			ScriptMessage.Listener = ((ScriptMessage message) => { Debug.Log("We just got a ScriptMessage"); });
			FriendMessage.Listener = ((FriendMessage message) => { Debug.Log("We just got a ScriptMessage"); });
			
			//LoadPlayerByExternalId("FB",myplayerexternalid);
			
			new GetUploadUrlRequest()
			//.SetUploadData(uploadData)
			.Send((response) =>
			{
				GSData scriptData = response.ScriptData;
				string url = response.Url;
			});
			
			new GetUploadedRequest()
			.SetUploadId("uploadId")
			.Send((response) =>
			{
				GSData scriptData = response.ScriptData;
				var size = response.Size;
				string url = response.Url;
			});
			
			new GetPropertyRequest()
			.SetPropertyShortCode("propertyShortCode")
			.Send((response) =>
			{
				GSData property = response.Property;
				GSData scriptData = response.ScriptData;
			});
			
			new GetPropertySetRequest()
			.SetPropertySetShortCode("propertySetShortCode")
			.Send((response) =>
			{
				GSData propertySet = response.PropertySet;
				GSData scriptData = response.ScriptData;
			});
			
			new GetDownloadableRequest()
			.SetShortCode("shortCode")
			.Send((response) =>
			{
				DateTime? lastModified = response.LastModified;
				GSData scriptData = response.ScriptData;
				string shortCode = response.ShortCode;
				var size = response.Size;
				string url = response.Url;
			});
			
			GSRequestData gsDataTemp = new GSRequestData("save data json");
			gsDataTemp.AddString("key","variableKey");
			gsDataTemp.AddString("value","variableValue");
			LogEventRequest requestSave = new LogEventRequest().SetEventKey("SAVE_PLAYER_DATA")
			.SetEventAttribute("key", gsDataTemp)
			.SetMaxResponseTimeInMillis(300);
			
			LogEventRequest requestLoad = new LogEventRequest().SetEventKey("LOAD_PLAYER_DATA");
			requestLoad.SetEventAttribute("key","");
			
			GSData scriptDataTemp = new GSRequestData()
			.AddNumber("myNumber",10)
			.AddString("myString","the value");
			
			GetPropertySetResponse someResponse = new GetPropertySetResponse(scriptDataTemp);
			long? myNumber = someResponse.ScriptData.GetNumber("myNumber");
			string myString = someResponse.ScriptData.GetString("myString");
		}
		
		
	}
}
#endif