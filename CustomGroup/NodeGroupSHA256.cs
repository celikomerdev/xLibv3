#if xLibv3
using UnityEngine;
using xLib.xValueClass;

namespace xLib.xNode.NodeObject
{
	[CreateAssetMenu(menuName = "xLib/Node/Group/SHA256")]
	public class NodeGroupSHA256 : NodeGroup
	{
		[SerializeField]private ValueGroupSHA256 nodeValue = new ValueGroupSHA256();
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