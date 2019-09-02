#if xLibv3
using UnityEngine;
using xLib.EventClass;

namespace xLib
{
	public class CompareString : CompareBase
	{
		#region Values
		[Header("Values")]
		[UnityEngine.Serialization.FormerlySerializedAs("value")]
		[SerializeField]private string left = "";
		public string Left
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
		
		[SerializeField]private string right = "";
		public string Right
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
			if(!CanWork) return;
			bool result = Comparison();
			if(CanDebug) Debug.LogFormat(this,this.name+":CompareString:{0}",result);
			eventCompare.Invoke(result);
		}
		#endregion
	}
}
#endif