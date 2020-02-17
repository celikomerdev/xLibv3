#if xLibv3
using UnityEngine;

namespace xLib.xValueClass
{
	[CreateAssetMenu(menuName = "xLib/Node/Unity/GameObject")]
	public class NodeGameObject : NodeValue<GameObject>
	{
		[SerializeField]private ValueGameObject nodeValue = new ValueGameObject();
		protected override xValue<GameObject> Node
		{
			get
			{
				return nodeValue;
			}
		}
	}
}
#endif