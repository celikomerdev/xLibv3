#if xLibv3
using UnityEngine;

namespace xLib.xValueClass
{
	//[CreateAssetMenu(menuName = "xLib/Node/Unity/Transform")]
	public class MonoTransform : MonoValue<Transform>
	{
		[SerializeField]private ValueTransform nodeValue = new ValueTransform();
		protected override xValue<Transform> Node
		{
			get
			{
				return nodeValue;
			}
		}
	}
}
#endif