#if xLibv3
using UnityEngine;
using xLib.xValueClass;

namespace xLib.xNode.NodeObject
{
	// [CreateAssetMenu(menuName = "xLib/Node/Basic/Array/int")]
	public class MonoIntArray : MonoValue<int[]>
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