#if xLibv3
using UnityEngine;

namespace xLib
{
	public abstract class xValueThreshold<V> : xValueEqual<V>
	{
		#region Virtual
		[Header("Snap")]
		[SerializeField]private bool snapDefault = false;
		protected virtual bool IsDefault(V valueNew)
		{
			return false;
		}
		
		[Header("Threshold")]
		[SerializeField]private bool useThreshold = false;
		[SerializeField]protected V valueThreshold = default(V);
		protected virtual bool IsThreshold(V valueNew)
		{
			return false;
		}
		#endregion
		
		
		#region CanChange
		protected override bool CanChange(V valueNew)
		{
			if(!IsCompare) return true;
			if(IsEqual(valueNew)) return false;
			if(snapDefault && IsDefault(valueNew)) return true;
			if(useThreshold && IsThreshold(valueNew)) return false;
			return true;
		}
		#endregion
	}
}
#endif