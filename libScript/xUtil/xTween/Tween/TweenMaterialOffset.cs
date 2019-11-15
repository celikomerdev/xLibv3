#if xLibv3
using UnityEngine;

namespace xLib.xTween
{
	public class TweenMaterialOffset : Tween
	{
		[SerializeField]private Material material = null;
		[SerializeField]private string propertyName = "";
		[SerializeField]private Vector2 from = Vector2.zero;
		[SerializeField]private Vector2 to = Vector2.one;
		
		protected override void SetRatio(float value)
		{
			material.SetTextureOffset(propertyName, Vector2.LerpUnclamped(from,to,value));
		}
	}
}
#endif