#if xLibv3
using UnityEngine;
using xLib.EventClass;

namespace xLib.ToolCompare
{
	public class CompareInt : BaseWorkM
	{
		#region Comparison
		[SerializeField]private int value;
		[SerializeField]private bool isLess;
		[SerializeField]private bool isEqual;
		[SerializeField]private bool isGreat;
		
		private bool Comparison(int value)
		{
			if(isEqual && value == this.value) return true;
			else if(isLess && value < this.value) return true;
			else if(isGreat && value > this.value) return true;
			else return false;
		}
		#endregion
		
		#region Compare
		public EventBool onCompare;
		public void Compare(int value)
		{
			if(!CanWork) return;
			bool result = Comparison(value);
			if(CanDebug) Debug.LogFormat(this,this.name+":CompareInt:{0}",result);
			onCompare.Invoke(result);
		}
		#endregion
		
		#region Property
		public int Value
		{
			get
			{
				return this.value;
			}
			set
			{
				this.value = value;
			}
		}
		#endregion
	}
}
#endif