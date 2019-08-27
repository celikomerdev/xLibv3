#if xLibv3
using UnityEngine;
using xLib.xValueClass;

namespace xLib.xNode.NodeObject
{
	[CreateAssetMenu(menuName = "xLib/Node/Basic/long")]
	public class NodeLong : NodeValue<long>
	{
		[SerializeField]private ValueLong nodeValue = new ValueLong();
		protected override xValue<long> Node
		{
			get
			{
				return nodeValue;
			}
		}
	}
}
#endif