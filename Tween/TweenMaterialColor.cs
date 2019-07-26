#if xLibv3
using UnityEngine;

namespace xLib.ToolTween
{
	public class TweenMaterialColor : Tween
	{
		[SerializeField]private Material material;
		[SerializeField]private string propertyName;
		[SerializeField]private Color from;
		[SerializeField]private Color to;
		
		override protected void SetRatio(float value)
		{
			material.SetColor(propertyName, Color.LerpUnclamped(from,to,value));
		}
	}
}
#endif