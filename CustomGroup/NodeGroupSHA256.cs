#if xLibv3
using UnityEngine;
using xLib.xValueClass;

namespace xLib.xNode.NodeObject
{
	[CreateAssetMenu(menuName = "xLib/Node/Group/SHA256")]
	public class NodeGroupSHA256 : NodeGroup
	{
		[SerializeField]internal ValueGroupSHA256 nodeValue = new ValueGroupSHA256();
		protected override ValueGroup Node
		{
			get
			{
				return nodeValue;
			}
		}
	}
}
#endif