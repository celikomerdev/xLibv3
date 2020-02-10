#if xLibv3
using UnityEngine;

namespace xLib
{
	public abstract class xValueEqual<V> : xValue<V>
	{
		#region IsCompare
		[Header("Compare")]
		[SerializeField]private bool skipCompareRpc = true;
		private bool SkipCompareRpc
		{
			get
			{
				if(!nodeSetting.UseRpc) return false;
				if(!ViewCore.inRpc) return false;
				if(ViewCore.CurrentId == string.Empty) return true;
				return skipCompareRpc;
			}
		}
		
		[SerializeField]private bool isCompare = true;
		protected bool IsCompare
		{
			get
			{
				if(SkipCompareRpc) return false;
				return isCompare;
			}
		}
		#endregion
		
		
		#region Virtual
		protected virtual bool IsEqual(V valueNew)
		{
			return false;
		}
		#endregion
		
		
		#region CanChange
		protected override bool CanChange(V valueNew)
		{
			if(!IsCompare) return true;
			if(IsEqual(valueNew)) return false;
			return true;
		}
		#endregion
	}
}
#endif