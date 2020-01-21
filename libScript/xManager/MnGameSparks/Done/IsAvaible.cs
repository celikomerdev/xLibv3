#if xLibv2
#if GameSparks
using GameSparks.Core;
using UnityEngine;
using xLib.xNode.NodeObject;

namespace xLib.xGameSparks
{
	public class IsAvaible : BaseRegisterM
	{
		[SerializeField]private NodeBool isAvaible;
		
		protected override bool TryRegister(bool value)
		{
			if(value)
			{
				GS.GameSparksAvailable += Callback;
			}
			else
			{
				GS.GameSparksAvailable -= Callback;
			}
			
			if(baseRegister.onRegister) Callback(GS.Available);
			return value;
		}
		
		private void Callback(bool value)
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":Callback:{0}",value);
			isAvaible.Value = value;
		}
	}
}
#endif
#endif