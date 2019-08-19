#if xLibv3
using UnityEngine;
using xLib.EventClass;

namespace xLib
{
	public class CompareNull : CompareBase
	{
		#region Values
		private Object right = null;
		public Object Right
		{
			get
			{
				return right;
			}
			set
			{
				if(!CanWork) return;
				if(right == value) return;
				right = value;
				Compare();
			}
		}
		#endregion
		
		
		#region Comparison
		private bool Comparison()
		{
			return (right==null);
		}
		#endregion
		
		
		#region Compare
		[Header("Result")]
		[UnityEngine.Serialization.FormerlySerializedAs("onCompare")]
		[SerializeField]private EventBool eventCompare = new EventBool();
		private void Compare()
		{
			bool result = Comparison();
			if(CanDebug) Debug.LogFormat(this,this.name+":CompareNull:{0}",result);
			eventCompare.Invoke(result);
		}
		#endregion
	}
}
#endif