#if xLibv3
using UnityEngine;

namespace xLib.xTween
{
	public class TweenMaterialColor : Tween
	{
		[SerializeField]private Material material = null;
		[SerializeField]private string propertyName = "";
		[SerializeField]private Color from = Color.white;
		[SerializeField]private Color to = Color.white;
		
		override protected void SetRatio(float value)
		{
			material.SetColor(propertyName, Color.LerpUnclamped(from,to,value));
		}
	}
}
#endif