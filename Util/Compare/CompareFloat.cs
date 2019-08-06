#if xLibv2
using UnityEngine;
using xLib.ToolEventClass;

namespace xLib.ToolCompare
{
	public class CompareFloat : BaseWorkM
	{
		#region Comparison
		[SerializeField]private float value;
		[SerializeField]private bool isLess;
		[SerializeField]private bool isEqual;
		[SerializeField]private bool isGreat;
		
		private bool Comparison(float value)
		{
			if(isEqual && value == this.value) return true;
			else if(isLess && value < this.value) return true;
			else if(isGreat && value > this.value) return true;
			else return false;
		}
		#endregion
		
		#region Compare
		public EventBool onCompare;
		public void Compare(float value)
		{
			if(!CanWork) return;
			bool result = Comparison(value);
			if(CanDebug) Debug.LogFormat(this,this.name+":CompareFloat:{0}",result);
			onCompare.Invoke(result);
		}
		#endregion
	}
}
#endif