#if xLibv3
using UnityEngine;

namespace xLib.xTween
{
	public class TweenMaterialFloat : Tween
	{
		[SerializeField]private Material material = null;
		[SerializeField]private string propertyName = "";
		[SerializeField]private float from = 0;
		[SerializeField]private float to = 1;
		
		protected override void SetRatio(float value)
		{
			material.SetFloat(propertyName,Mathf.LerpUnclamped(from,to,value));
		}
	}
}
#endif