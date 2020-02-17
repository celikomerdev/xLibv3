#if xLibv3
using UnityEngine;

namespace xLib.xValueClass
{
	// [CreateAssetMenu(menuName = "xLib/Node/Unity/AnimationCurve")]
	public class MonoAnimationCurve : MonoValue<AnimationCurve>
	{
		[SerializeField]private ValueAnimationCurve nodeValue = new ValueAnimationCurve();
		protected override xValue<AnimationCurve> Node
		{
			get
			{
				return nodeValue;
			}
		}
	}
}
#endif