#if xLibv3
using UnityEngine;
using xLib.xValueClass;

namespace xLib.xNode.NodeObject
{
	// [CreateAssetMenu(menuName = "xLib/Node/Basic/void")]
	public class MonoVoid : MonoValue<Void>
	{
		[SerializeField]private ValueVoid nodeValue = new ValueVoid();
		protected override xValue<Void> Node
		{
			get
			{
				return nodeValue;
			}
		}
	}
}
#endif