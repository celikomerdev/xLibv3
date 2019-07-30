#if xLibv3
using UnityEngine;
using xLib.xValueClass;

namespace xLib.xNode.NodeObject
{
	[CreateAssetMenu(menuName = "xLib/Node/Unity/AudioClip")]
	public class NodeAudioClip : NodeValue<AudioClip>
	{
		[SerializeField]private ValueAudioClip nodeValue = new ValueAudioClip();
		protected override xValue<AudioClip> Node
		{
			get
			{
				return nodeValue;
			}
		}
	}
}
#endif