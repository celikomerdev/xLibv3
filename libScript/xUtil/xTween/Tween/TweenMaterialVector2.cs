#if xLibv3
using UnityEngine;

namespace xLib.xTween
{
	public class TweenMaterialVector2 : Tween
	{
		[SerializeField]private Material material = null;
		[SerializeField]private string propertyName = "";
		[SerializeField]private Vector2 from = Vector2.zero;
		[SerializeField]private Vector2 to = Vector2.one;
		
		protected override void SetRatio(float value)
		{
			material.SetVector(propertyName,Vector2.LerpUnclamped(from,to,value));
		}
	}
}
#endif