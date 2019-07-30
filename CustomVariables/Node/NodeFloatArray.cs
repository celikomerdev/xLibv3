#if xLibv2
using UnityEngine;
using xLib.xValueClass;

namespace xLib.xNode.NodeObject
{
	[CreateAssetMenu(menuName = "xLib/Node/Basic/Array/float")]
	public class NodeFloatArray : NodeValue<float[]>
	{
		[SerializeField]private ValueFloatArray nodeValue = new ValueFloatArray();
		protected override xValue<float[]> Node
		{
			get
			{
				return nodeValue;
			}
		}
	}
}
#endif