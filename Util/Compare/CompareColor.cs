#if xLibv3
using UnityEngine;
using xLib.EventClass;

namespace xLib
{
	public class CompareColor : CompareBase
	{
		#region Values
		[Header("Values")]
		[SerializeField]private Color left = Color.white;
		public Color Left
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
		
		[SerializeField]private Color right = Color.white;
		public Color Right
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
		[UnityEngine.Serialization.FormerlySerializedAs("onCompare")]
		[SerializeField]private EventBool eventCompare = new EventBool();
		private void Compare()
		{
			bool result = Comparison();
			if(CanDebug) Debug.LogFormat(this,this.name+":CompareColor:{0}",result);
			eventCompare.Invoke(result);
		}
		#endregion
	}
}
#endif