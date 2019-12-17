#if xLibv3
using UnityEngine;
using xLib.xValueClass;

namespace xLib.xNode.NodeObject
{
	// [CreateAssetMenu(menuName = "xLib/Node/Basic/float")]
	public class MonoFloat : MonoValue<float>
	{
		[SerializeField]private ValueFloat nodeValue = new ValueFloat();
		protected override xValue<float> Node
		{
			get
			{
				return nodeValue;
			}
		}
	}
}
#endif