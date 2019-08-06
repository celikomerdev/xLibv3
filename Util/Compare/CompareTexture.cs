#if xLibv2
using UnityEngine;
using xLib.ToolEventClass;

namespace xLib.ToolCompare
{
	public class CompareTexture : BaseWorkM
	{
		#region Comparison
		[SerializeField]private Texture value;
		
		private bool Comparison(Texture value)
		{
			return (this.value == value);
		}
		#endregion
		
		#region Compare
		public EventBool onCompare;
		public void Compare(Texture value)
		{
			if(!CanWork) return;
			bool result = Comparison(value);
			if(CanDebug) Debug.LogFormat(this,this.name+":CompareTexture:{0}",result);
			onCompare.Invoke(result);
		}
		#endregion
	}
}
#endif