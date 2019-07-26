#if xLibv2
using UnityEngine;

namespace xLib.ToolTween
{
	public class TweenMaterialOffset : Tween
	{
		public Material material;
		public string propertyName;
		public Vector2 from;
		public Vector2 to;
		
		override protected void SetRatio(float value)
		{
			material.SetTextureOffset(propertyName, Vector2.LerpUnclamped(from,to,value));
		}
	}
}
#endif