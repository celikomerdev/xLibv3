#if xLibv3
using UnityEngine;
using xLib.xValueClass;

namespace xLib.xNode.NodeObject
{
	// [CreateAssetMenu(menuName = "xLib/Node/Basic/byte")]
	public class MonoByte : MonoValue<byte>
	{
		[SerializeField]private ValueByte nodeValue = new ValueByte();
		protected override xValue<byte> Node
		{
			get
			{
				return nodeValue;
			}
		}
	}
}
#endif