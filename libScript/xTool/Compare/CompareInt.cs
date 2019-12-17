#if xLibv3
using UnityEngine;
using xLib.EventClass;

namespace xLib
{
	public class CompareInt : CompareBase
	{
		#region Values
		[Header("Values")]
		[UnityEngine.Serialization.FormerlySerializedAs("value")]
		[SerializeField]private int left = 0;
		public int Left
		{
			get
			{
				return left;
			}
			set
			{
				if(left == value) return;
				left = value;
				Compare();
			}
		}
		
		[SerializeField]private int right = 0;
		public int Right
		{
			get
			{
				return right;
			}
			set
			{
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
			if(!CanWork) return;
			bool result = Comparison();
			if(CanDebug) Debug.LogFormat(this,this.name+":CompareInt:{0}",result);
			eventCompare.Invoke(result);
		}
		#endregion
	}
}
#endif