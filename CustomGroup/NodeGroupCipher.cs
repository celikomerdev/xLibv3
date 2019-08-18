#if xLibv3
using UnityEngine;
using xLib.xValueClass;

namespace xLib.xNode.NodeObject
{
	[CreateAssetMenu(menuName = "xLib/Node/Group/Cipher")]
	public class NodeGroupCipher : NodeGroup
	{
		[SerializeField]private ValueGroupCipher nodeValue = new ValueGroupCipher();
		protected override xValue<ObjectGroup> Node
		{
			get
			{
				return nodeValue;
			}
		}
	}
}
#endif