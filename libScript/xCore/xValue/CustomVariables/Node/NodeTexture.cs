#if xLibv3
using UnityEngine;
using xLib.xValueClass;

namespace xLib.xNode.NodeObject
{
	[CreateAssetMenu(menuName = "xLib/Node/Unity/Texture")]
	public class NodeTexture : NodeValue<Texture>
	{
		[SerializeField]private ValueTexture nodeValue = new ValueTexture();
		protected override xValue<Texture> Node
		{
			get
			{
				return nodeValue;
			}
		}
	}
}
#endif