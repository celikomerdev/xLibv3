#if xLibv3
using UnityEngine;
using xLib.xValueClass;

namespace xLib.xNode.NodeObject
{
	[CreateAssetMenu(menuName = "xLib/Node/Unity/Sprite")]
	public class NodeSprite : NodeValue<Sprite>
	{
		[SerializeField]private ValueSprite nodeValue = new ValueSprite();
		protected override xValue<Sprite> Node
		{
			get
			{
				return nodeValue;
			}
		}
	}
}
#endif