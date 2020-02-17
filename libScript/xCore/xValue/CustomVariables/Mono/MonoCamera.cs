#if xLibv3
using UnityEngine;

namespace xLib.xValueClass
{
	// [CreateAssetMenu(menuName = "xLib/Node/Unity/Camera")]
	public class MonoCamera : MonoValue<Camera>
	{
		[SerializeField]private ValueCamera nodeValue = new ValueCamera();
		protected override xValue<Camera> Node
		{
			get
			{
				return nodeValue;
			}
		}
	}
}
#endif