#if xLibv2
using UnityEngine;

namespace xLib.ToolSpace
{
	public class OriginObject : BaseRegisterM
	{
		public Transform trans;
		
		protected override bool Register(bool value)
		{
			if(!trans) trans = transform;
			
			if(MnSpace.ins==null)
			{
				//Debug.LogWarning("MnSpace.ins==null",this);
				return false;
			}
			MnSpace.ins.Listener(OnCall,value);
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