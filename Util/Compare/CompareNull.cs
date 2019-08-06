#if xLibv2
using UnityEngine;
using xLib.ToolEventClass;

namespace xLib.ToolCompare
{
	public class CompareNull : BaseWorkM
	{
		#region Comparison
		private bool Comparison(Object value)
		{
			return (value==null);
		}
		#endregion
		
		#region Compare
		public EventBool onCompare;
		public void Compare(Object value)
		{
			if(!CanWork) return;
			bool result = Comparison(value);
			if(CanDebug) Debug.LogFormat(this,this.name+":CompareNull:{0}",result);
			onCompare.Invoke(result);
		}
		#endregion
		
		[Header("ToDelete")]
		[SerializeField]private bool isNull = true;
		protected override void OnValidatedForced()
		{
			if(!isNull) Debug.LogFormat(this,this.name+":REVERSE CALL!!!");
		}
	}
}
#endif