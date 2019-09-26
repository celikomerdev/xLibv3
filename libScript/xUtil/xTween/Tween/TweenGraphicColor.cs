#if xLibv3
#if PackUI
using UnityEngine;
using UnityEngine.UI;

namespace xLib.xTween
{
	public class TweenGraphicColor : Tween
	{
		[SerializeField]private Graphic target = null;
		[SerializeField]private Color from = Color.white;
		[SerializeField]private Color to = Color.white;
		
		override protected void SetRatio(float value)
		{
			target.color = Color.LerpUnclamped(from,to,value);
		}
		
		#if UNITY_EDITOR
		[ContextMenu("Fill")]
		private void Fill()
		{
			if(!target) target = GetComponent<Graphic>();
			if(!target) target = GetComponentInParent<Graphic>();
		}
		#endif
	}
}
#endif
#endif