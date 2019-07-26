#if xLibv3
using UnityEngine;

namespace xLib.ToolTween
{
	public class TweenMaterialOffset : Tween
	{
		[SerializeField]private Material material;
		[SerializeField]private string propertyName;
		[SerializeField]private Vector2 from;
		[SerializeField]private Vector2 to;
		
		override protected void SetRatio(float value)
		{
			material.SetTextureOffset(propertyName, Vector2.LerpUnclamped(from,to,value));
		}
	}
}
#endif