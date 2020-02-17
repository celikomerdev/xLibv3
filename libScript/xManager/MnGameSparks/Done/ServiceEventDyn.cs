#if xLibv2
#if GameSparks
using GameSparks.Api.Requests;
using GameSparks.Api.Responses;
using GameSparks.Core;
using UnityEngine;
using xLib.ToolEventClass;

namespace xLib.xGameSparks
{
	public class ServiceEventDyn : BaseWorkM
	{
		[SerializeField]private bool canWorkDisable;
		[SerializeField]private bool isDurable;
		
		[SerializeField]private string key;
		
		[SerializeField]private Object serializableObject;
		private ISerializableObject iSerializableObject;
		
		[SerializeField]private bool isJSON = true;
		[SerializeField]private bool delete;
		
		[Header("Result")]
		[SerializeField]private EventBool eventBool;
		
		#region Login
		public void Call()
		{
			if(!CanWork) return;
			if(!enabled && !canWorkDisable) return;
			if(CanDebug) Debug.LogFormat(this,this.name+":Call");
			
			LogEventRequest request = new LogEventRequest();
			request.SetDurable(isDurable);
			
			request.SetEventKey(key);
			
			if(iSerializableObject==null) iSerializableObject = serializableObject.GetGeneric<ISerializableObject>();
			if(iSerializableObject==null) return;
			
			request.SetEventAttribute("Key",iSerializableObject.Key);
			request.SetEventAttribute("Value",new GSRequestData(iSerializableObject.SerializedObjectRaw.ToString()));
			if(delete) request.SetEventAttribute("Delete",1); //TODO remove
			
			request.Send(Callback);
		}
		
		private void Callback(LogEventResponse response)
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":Callback:{0}",response.JSONString);
			UseResponse(response);
			eventBool.Invoke(!response.HasErrors);
		}
		
		private void UseResponse(LogEventResponse response)
		{
			if(iSerializableObject==null) iSerializableObject = serializableObject.GetGeneric<ISerializableObject>();
			if(iSerializableObject==null) return;
			
			if(response.ScriptData == null) return;
			if(!response.ScriptData.ContainsKey("Key")) return;
			if(response.ScriptData.GetString("Key") != iSerializableObject.Key) return;
			
			if(!response.ScriptData.ContainsKey("Value")) return;
			if(isJSON)
			{
				iSerializableObject.SerializedObjectRaw = response.ScriptData.GetGSData("Value").JSON;
			}
			else
			{
				iSerializableObject.SerializedObjectRaw = response.ScriptData.BaseData["Value"];
			}
		}
		#endregion
	}
}
#endif
#endif