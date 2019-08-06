#if xLibv2
using UnityEngine;
using xLib.ToolEventClass;

namespace xLib.ToolCompare
{
	public class CompareString : BaseWorkM
	{
		#region Comparison
		[SerializeField]private string value;
		[SerializeField]private bool isEqual = true;
		
		private bool Comparison(string value)
		{
			if(!isEqual)
			{
				Debug.LogWarningFormat(this,this.name+":Simplify");
			}
			
			if(isEqual && value == this.value) return true;
			else return false;
		}
		#endregion
		
		#region Compare
		public EventBool onCompare;
		public void Compare(string value)
		{
			if(!CanWork) return;
			bool result = Comparison(value);
			if(CanDebug) Debug.LogFormat(this,this.name+":CompareString:{0}",result);
			onCompare.Invoke(result);
		}
		#endregion
	}
}
#endif