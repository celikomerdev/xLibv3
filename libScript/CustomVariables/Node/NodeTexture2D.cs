#if xLibv3
using UnityEngine;
using xLib.xValueClass;

namespace xLib.xNode.NodeObject
{
	[CreateAssetMenu(menuName = "xLib/Node/Unity/Texture2D")]
	public class NodeTexture2D : NodeValue<Texture2D>
	{
		[SerializeField]private ValueTexture2D nodeValue = new ValueTexture2D();
		protected override xValue<Texture2D> Node
		{
			get
			{
				return nodeValue;
			}
		}
	}
}
#endif