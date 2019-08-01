#if xLibv3
using UnityEngine;
using xLib.xValueClass;

namespace xLib.xNode.NodeObject
{
	// [CreateAssetMenu(menuName = "xLib/Node/Group/Cipher")]
	public class MonoGroupCipher : MonoGroup
	{
		[SerializeField]internal ValueGroupCipher nodeValue = new ValueGroupCipher();
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