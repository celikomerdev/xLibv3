#if xLibv2
#if GameSparks
using GameSparks.Api.Requests;
using GameSparks.Api.Responses;
using UnityEngine;
using xLib.ToolEventClass;
using xLib.xNode.NodeObject;

namespace xLib.xGameSparks
{
	public class ServiceEvent : BaseWorkM
	{
		[SerializeField]private bool isDurable;
		
		[SerializeField]private string key;
		
		[Header("Node")]
		[SerializeField]private NodeString[] nodeString;
		[SerializeField]private NodeInt[] nodeInt;
		
		[Header("Mono")]
		[SerializeField]private MonoString[] monoString;
		[SerializeField]private MonoInt[] monoInt;
		
		[Header("Result")]
		[SerializeField]private EventBool eventBool;
		
		public void Call()
		{
			if(!CanWork) return;
			if(CanDebug) Debug.LogFormat(this,this.name+":Call");
			
			LogEventRequest request = new LogEventRequest();
			request.SetDurable(isDurable);
			
			request.SetEventKey(key);
			
			for (int i = 0; i < nodeString.Length; i++)
			{
				request.SetEventAttribute(nodeString[i].Key,nodeString[i].Value);
			}
			
			for (int i = 0; i < nodeInt.Length; i++)
			{
				request.SetEventAttribute(nodeInt[i].Key,nodeInt[i].Value);
			}
			
			for (int i = 0; i < monoString.Length; i++)
			{
				request.SetEventAttribute(monoString[i].Key,monoString[i].Value);
			}
			
			for (int i = 0; i < monoInt.Length; i++)
			{
				request.SetEventAttribute(monoInt[i].Key,monoInt[i].Value);
			}
			
			request.Send(Callback);
		}
		
		private void Callback(LogEventResponse response)
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":Callback:{0}",response.JSONString);
			eventBool.Invoke(!response.HasErrors);
		}
	}
}
#endif
#endif