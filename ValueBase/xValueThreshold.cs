#if xLibv3
using UnityEngine;

namespace xLib
{
	public abstract class xValueThreshold<V> : xValueEqual<V>
	{
		#region Virtual
		[Header("Snap")]
		[SerializeField]internal bool snapDefault = false;
		protected virtual bool IsDefault(V value)
		{
			return false;
		}
		
		[Header("Threshold")]
		[SerializeField]internal bool useThreshold = false;
		[SerializeField]internal V valueThreshold;
		protected virtual bool IsThreshold(V value)
		{
			return false;
		}
		#endregion
		
		
		#region CanChange
		protected override bool CanChange(V value)
		{
			if(!IsCompare) return true;
			if(IsEqual(value)) return false;
			if(snapDefault && IsDefault(value)) return true;
			if(useThreshold && IsThreshold(value)) return false;
			return true;
		}
		#endregion
	}
}
#endif