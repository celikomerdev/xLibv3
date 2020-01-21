#if xLibv2
#if GameSparks
using GameSparks.Core;
using UnityEngine;
using xLib.xNode.NodeObject;

namespace xLib.xGameSparks
{
	public class IsAuth : BaseRegisterM
	{
		[SerializeField]private NodeBool isAuth;
		[SerializeField]private NodeString userId;
		[SerializeField]private NodeVoid userChanged;
		
		protected override bool TryRegister(bool value)
		{
			if(value)
			{
				GS.GameSparksAuthenticated += Callback;
				if(baseRegister.onRegister) Callback("Init");
			}
			else
			{
				GS.GameSparksAuthenticated -= Callback;
			}
			
			return value;
		}
		
		private void Callback(string value)
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":Callback:{0}",value);
			isAuth.Value = GS.Authenticated;
			
			if(isAuth.Value)
			{
				if(userId.Value != userId.ValueDefault)
				{
					if(userId.Value != GS.GSPlatform.UserId) userChanged.Call();
				}
				
				userId.Value = GS.GSPlatform.UserId;
			}
			else
			{
				userId.Value = userId.ValueDefault;
			}
		}
	}
}
#endif
#endif