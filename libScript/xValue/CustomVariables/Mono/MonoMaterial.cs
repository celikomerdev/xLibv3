#if xLibv3
using UnityEngine;
using xLib.xValueClass;

namespace xLib.xNode.NodeObject
{
	// [CreateAssetMenu(menuName = "xLib/Node/Unity/Material")]
	public class MonoMaterial : MonoValue<Material>
	{
		[SerializeField]private ValueMaterial nodeValue = new ValueMaterial();
		protected override xValue<Material> Node
		{
			get
			{
				return nodeValue;
			}
		}
	}
}
#endif