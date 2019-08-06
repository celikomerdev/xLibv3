#if xLibv2
using UnityEngine;
using xLib.ToolEventClass;

namespace xLib.ToolCompare
{
	public class CompareGameObject : BaseWorkM
	{
		#region Comparison
		[SerializeField]private GameObject value;
		
		private bool Comparison(GameObject value)
		{
			return (value == this.value);
		}
		#endregion
		
		#region Compare
		public EventBool onCompare;
		public void Compare(GameObject value)
		{
			if(!CanWork) return;
			bool result = Comparison(value);
			if(CanDebug) Debug.LogFormat(this,this.name+":CompareGameObject:{0}",result);
			onCompare.Invoke(result);
		}
		#endregion
	}
}
#endif