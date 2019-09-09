#if xLibv3
using UnityEngine;
using xLib.xValueClass;

namespace xLib.xNode.NodeObject
{
	// [CreateAssetMenu(menuName = "xLib/Node/Unity/Color")]
	public class MonoColor : MonoValue<Color>
	{
		[SerializeField]private ValueColor nodeValue = new ValueColor();
		protected override xValue<Color> Node
		{
			get
			{
				return nodeValue;
			}
		}
	}
}
#endif