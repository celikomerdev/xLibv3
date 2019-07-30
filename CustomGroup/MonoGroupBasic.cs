#if xLibv3
using UnityEngine;
using xLib.xValueClass;

namespace xLib.xNode.NodeObject
{
	// [CreateAssetMenu(menuName = "xLib/Node/Group/Basic")]
	public class MonoGroupBasic : MonoGroup
	{
		[SerializeField]internal ValueGroup nodeValue = new ValueGroup();
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