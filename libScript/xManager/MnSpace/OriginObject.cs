#if xLibv2
using UnityEngine;

namespace xLib.ToolSpace
{
	public class OriginObject : BaseRegisterM
	{
		public Transform trans;
		
		protected override bool TryRegister(bool value)
		{
			if(!trans) trans = transform;
			ExtSpace.Listener(OnCall,value);
			return value;
		}
		
		private void OnCall(Vector3 value)
		{
			trans.Translate(value,Space.World);
		}
		
		[ContextMenu("SetTrans")]
		private void SetTrans()
		{
			if(!trans) trans = transform;
		}
	}
}
#endif