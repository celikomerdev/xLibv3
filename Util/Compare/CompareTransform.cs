#if xLibv2
using UnityEngine;
using xLib.ToolEventClass;

namespace xLib.ToolCompare
{
	public class CompareTransform : BaseWorkM
	{
		#region Comparison
		[SerializeField]private Transform value;
		
		private bool Comparison(Transform value)
		{
			return (value == this.value);
		}
		#endregion
		
		#region Compare
		public EventBool onCompare;
		public void Compare(Transform value)
		{
			if(!CanWork) return;
			bool result = Comparison(value);
			if(CanDebug) Debug.LogFormat(this,this.name+":CompareTransform:{0}",result);
			onCompare.Invoke(result);
		}
		#endregion
	}
}
#endif