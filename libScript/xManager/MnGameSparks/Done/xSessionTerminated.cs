#if xLibv2
#if GameSparks
using GameSparks.Api.Messages;
using UnityEngine;
using xLib.ToolEventClass;

namespace xLib.xGameSparks
{
	public class xSessionTerminated : BaseRegisterM
	{
		[SerializeField]private EventUnity eventUnity;
		
		protected override bool Register(bool value)
		{
			if(value)
			{
				GameSparks.Api.Messages.SessionTerminatedMessage.Listener += Callback;
			}
			else
			{
				GameSparks.Api.Messages.SessionTerminatedMessage.Listener -= Callback;
			}
			return value;
		}
		
		private void Callback(SessionTerminatedMessage value)
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":Callback:{0}",value);
			eventUnity.Invoke();
		}
	}
}
#endif
#endif