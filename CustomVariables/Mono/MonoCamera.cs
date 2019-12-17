#if xLibv3
using UnityEngine;
using xLib.xValueClass;

namespace xLib.xNode.NodeObject
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