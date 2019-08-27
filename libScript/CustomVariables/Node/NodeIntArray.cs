#if xLibv3
using UnityEngine;
using xLib.xValueClass;

namespace xLib.xNode.NodeObject
{
	[CreateAssetMenu(menuName = "xLib/Node/Basic/Array/int")]
	public class NodeIntArray : NodeValue<int[]>
	{
		[SerializeField]private ValueIntArray nodeValue = new ValueIntArray();
		protected override xValue<int[]> Node
		{
			get
			{
				return nodeValue;
			}
		}
	}
}
#endif