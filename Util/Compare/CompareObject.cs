#if xLibv3
using UnityEngine;
using xLib.EventClass;

namespace xLib
{
	public class CompareObject : BaseWorkM
	{
		#region Values
		[Header("Values")]
		[SerializeField]private Object left = null;
		public Object Left
		{
			get
			{
				return left;
			}
			set
			{
				if(!CanWork) return;
				if(left == value) return;
				left = value;
				Compare();
			}
		}
		
		[SerializeField]private Object right = null;
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
			return (left == right);
		}
		#endregion
		
		
		#region Compare
		[Header("Result")]
		[SerializeField]private EventBool eventCompare = new EventBool();
		private void Compare()
		{
			bool result = Comparison();
			if(CanDebug) Debug.LogFormat(this,this.name+":CompareObject:{0}",result);
			eventCompare.Invoke(result);
		}
		#endregion
	}
}
#endif