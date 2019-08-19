#if xLibv3
using UnityEngine;
using xLib.EventClass;

namespace xLib
{
	public class CompareFloat : CompareBase
	{
		#region Values
		[Header("Values")]
		[UnityEngine.Serialization.FormerlySerializedAs("value")]
		[SerializeField]private float left = 0;
		public float Left
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
		
		[SerializeField]private float right = 0;
		public float Right
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
		[Header("Comparision")]
		[SerializeField]private bool isLess = false;
		[SerializeField]private bool isEqual = false;
		[SerializeField]private bool isGreat = false;
		private bool Comparison()
		{
			if(isEqual && (right==left)) return true;
			if(isLess && (right<left)) return true;
			if(isGreat && (right>left)) return true;
			return false;
		}
		#endregion
		
		
		#region Compare
		[Header("Result")]
		[UnityEngine.Serialization.FormerlySerializedAs("onCompare")]
		[SerializeField]private EventBool eventCompare = new EventBool();
		private void Compare()
		{
			bool result = Comparison();
			if(CanDebug) Debug.LogFormat(this,this.name+":CompareFloat:{0}",result);
			eventCompare.Invoke(result);
		}
		#endregion
	}
}
#endif