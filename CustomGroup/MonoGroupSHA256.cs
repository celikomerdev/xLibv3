#if xLibv3
using UnityEngine;
using xLib.xValueClass;

namespace xLib.xNode.NodeObject
{
	// [CreateAssetMenu(menuName = "xLib/Node/Group/SHA256")]
	public class MonoGroupSHA256 : MonoGroup
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