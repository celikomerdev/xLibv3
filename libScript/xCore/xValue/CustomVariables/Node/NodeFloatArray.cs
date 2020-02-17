#if xLibv3
using UnityEngine;

namespace xLib.xValueClass
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