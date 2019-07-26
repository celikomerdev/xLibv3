#if xLibv2
using UnityEngine;

namespace xLib.ToolTween
{
	public class TweenMaterialColor : Tween
	{
		public Material material;
		public string propertyName;
		public Color from;
		public Color to;
		
		override protected void SetRatio(float value)
		{
			material.SetColor(propertyName, Color.LerpUnclamped(from,to,value));
		}
	}
}
#endif