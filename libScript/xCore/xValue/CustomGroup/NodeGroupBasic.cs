#if xLibv3
using UnityEngine;

namespace xLib.xValueClass
{
	[CreateAssetMenu(menuName = "xLib/Node/Group/Basic")]
	public class NodeGroupBasic : NodeGroup
	{
		[SerializeField]private ValueGroup nodeValue = new ValueGroup();
		protected override xValue<ObjectGroup> Node
		{
			get
			{
				return nodeValue;
			}
		}
	}
}
#endif