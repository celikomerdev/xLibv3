#if xLibv3
using UnityEngine;

namespace xLib.xValueClass
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