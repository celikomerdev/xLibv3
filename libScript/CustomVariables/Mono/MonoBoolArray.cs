#if xLibv3
using UnityEngine;
using xLib.xValueClass;

namespace xLib.xNode.NodeObject
{
	// [CreateAssetMenu(menuName = "xLib/Node/Basic/Array/bool")]
	public class MonoBoolArray : MonoValue<bool[]>
	{
		[SerializeField]private ValueBoolArray nodeValue = new ValueBoolArray();
		protected override xValue<bool[]> Node
		{
			get
			{
				return nodeValue;
			}
		}
	}
}
#endif