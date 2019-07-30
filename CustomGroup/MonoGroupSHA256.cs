#if xLibv2
using UnityEngine;
using xLib.xValueClass;

namespace xLib.xNode.NodeObject
{
	// [CreateAssetMenu(menuName = "xLib/Node/Group/SHA256")]
	public class MonoGroupSHA256 : MonoGroup
	{
		[SerializeField]internal ValueGroupSHA256 nodeValue = new ValueGroupSHA256();
		public override ValueGroup Node
		{
			get
			{
				return nodeValue;
			}
		}
	}
}
#endif