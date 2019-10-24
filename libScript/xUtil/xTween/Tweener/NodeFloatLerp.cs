﻿#if xLibv3
using UnityEngine;
using xLib.xNode.NodeObject;
using xLib.xTween;

namespace xLib.ToolTweener
{
	public class NodeFloatLerp : BaseTickNodeM
	{
		[SerializeField]private NodeFloat assetFloat;
		[SerializeField]private Tween target;
		[SerializeField]private float lerp = 4;
		[SerializeField]private float multiplier = 1;
		[SerializeField]private bool isAbsolute;
		
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
			
			if(lerp<100) valueSmooth = Mathf.Lerp(valueSmooth,valueCurrent,tickTime*lerp);
			else valueSmooth = valueCurrent;
			
			target.SetBaseRatio(valueSmooth);
		}
		#endregion
	}
}
#endif