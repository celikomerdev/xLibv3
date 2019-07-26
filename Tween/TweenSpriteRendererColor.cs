#if xLibv3
using UnityEngine;

namespace xLib.ToolTween
{
	public class TweenSpriteRendererColor : Tween
	{
		[SerializeField]private SpriteRenderer target;
		[SerializeField]private Color from;
		[SerializeField]private Color to;
		
		override protected void SetRatio(float value)
		{
			target.color = Color.LerpUnclamped(from,to,value);
		}
	}
}
#endif