#if xLibv3
using UnityEngine;

namespace xLib.xValueClass
{
	[CreateAssetMenu(menuName = "xLib/Node/Unity/Color")]
	public class NodeColor : NodeValue<Color>
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