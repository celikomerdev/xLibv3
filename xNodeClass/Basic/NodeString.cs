#if xLibv2
using UnityEngine;
using xLib.xValueClass;

namespace xLib.xNode.NodeObject
{
	[CreateAssetMenu(menuName = "xLib/Node/Basic/string")]
	public class NodeString : NodeValue<string>
	{
		[SerializeField]private ValueString nodeValue = new ValueString();
		protected override xValue<string> Node
		{
			get
			{
				return nodeValue;
			}
		}
	}
}
#endif