#if xLibv2
#if GameSparks
using UnityEngine;
using xLib.ToolEventClass;

namespace xLib.xGameSparks
{
	public class Reset : BaseWorkM
	{
		[SerializeField]private EventUnity eventUnity;
		
		#region Call
		public void Call()
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":Call");
			GameSparks.Core.GS.Reset();
			Callback();
		}
		
		private void Callback()
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":Callback");
			eventUnity.Invoke();
		}
		#endregion
	}
}
#endif
#endif