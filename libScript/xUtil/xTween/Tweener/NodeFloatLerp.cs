#if xLibv3
using UnityEngine;
using xLib.xNode.NodeObject;
using xLib.xTween;

namespace xLib.ToolTweener
{
	public class NodeFloatLerp : BaseTickNodeM
	{
		[SerializeField]private NodeFloat assetFloat = null;
		[SerializeField]private Tween target = null;
		[SerializeField]private float speed = 4;
		[SerializeField]private float multiplier = 1;
		[SerializeField]private bool isAbsolute = false;
		
		#region Behavior
		protected override void Tick(float tickTime)
		{
			Work(tickTime);
		}
		#endregion
		
		#region Work
		protected float valueCurrent;
		protected float valueSmooth;
		protected void Work(float tickTime)
		{
			valueCurrent = multiplier*assetFloat.Value;
			if(isAbsolute) valueCurrent = Mathf.Abs(valueCurrent);
			
			if(speed<100) valueSmooth = Mathf.Lerp(valueSmooth,valueCurrent,tickTime*speed);
			else valueSmooth = valueCurrent;
			
			target.SetBaseRatio(valueSmooth);
		}
		#endregion
	}
}
#endif