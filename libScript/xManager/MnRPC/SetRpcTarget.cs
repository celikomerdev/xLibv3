#if xLibv2
using UnityEngine;

namespace xLib.xTool
{
	public class SetRpcTarget : BaseWorkM
	{
		[SerializeField]private xRpcTarget target;
		[SerializeField]private Object[] arrayObject;
		
		private void Awake()
		{
			if(!CanWork) return;
			
			for (int i = 0; i < arrayObject.Length; i++)
			{
				IRpc tempRpc = (IRpc)arrayObject[i];
				tempRpc.RpcTarget = target;
				//tempRpc.UseRpc = true;
			}
		}
	}
}
#endif