#if xLibv3
using UnityEngine;
using xLib.xValueClass;

namespace xLib.xNode.NodeObject
{
	[CreateAssetMenu(menuName = "xLib/Node/Basic/byte")]
	public class NodeByte : NodeValue<byte>
	{
		[SerializeField]private ValueByte nodeValue = new ValueByte();
		protected override xValue<byte> Node
		{
			get
			{
				return nodeValue;
			}
		}
	}
}
#endif