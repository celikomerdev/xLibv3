#if xLibv3
using UnityEngine;

namespace xLib.xTween
{
	public class TweenSpriteRendererColor : Tween
	{
		[SerializeField]private SpriteRenderer target = null;
		[SerializeField]private Color from = Color.white;
		[SerializeField]private Color to = Color.white;
		
		protected override void SetRatio(float value)
		{
			target.color = Color.LerpUnclamped(from,to,value);
		}
	}
}
#endif