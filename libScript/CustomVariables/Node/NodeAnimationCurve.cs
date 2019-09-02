#if xLibv3
using UnityEngine;
using xLib.xValueClass;

namespace xLib.xNode.NodeObject
{
	[CreateAssetMenu(menuName = "xLib/Node/Unity/AnimationCurve")]
	public class NodeAnimationCurve : NodeValue<AnimationCurve>
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