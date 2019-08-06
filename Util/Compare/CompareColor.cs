#if xLibv2
using UnityEngine;
using xLib.ToolEventClass;

namespace xLib.ToolCompare
{
	public class CompareColor : BaseWorkM
	{
		#region Comparison
		[SerializeField]private bool onChange;
		[SerializeField]private Color left;
		[SerializeField]private Color right;
		[SerializeField]private bool isEqual;
		
		private bool Comparison()
		{
			bool result = (left == right);
			if(isEqual) return (result);
			else
			{
				Debug.LogWarningFormat(this,this.name+":Simplify");
				return (!result);
			}
			
			// string v1 = ColorUtility.ToHtmlStringRGBA(this.left);
			// string v2 = ColorUtility.ToHtmlStringRGBA(this.right);
			
			// if(isEqual) return (v1==v2);
			// else return (v1!=v2);
		}
		#endregion
		
		#region Compare
		public EventBool onCompare;
		public void Compare(Color value)
		{
			Right = value;
			Compare();
		}
		
		public void Compare()
		{
			if(!CanWork) return;
			
			bool result = Comparison();
			if(CanDebug) Debug.LogFormat(this,this.name+":CompareColor:{0}",result);
			onCompare.Invoke(result);
		}
		#endregion
		
		#region Property
		public Color Left
		{
			get
			{
				return this.left;
			}
			set
			{
				this.left = value;
				if(onChange) Compare();
			}
		}
		
		public Color Right
		{
			get
			{
				return this.right;
			}
			set
			{
				this.right = value;
				if(onChange) Compare();
			}
		}
		#endregion
	}
}
#endif