#if xLibv3
#if ModAudio
using UnityEngine;

namespace xLib.xValueClass
{
	// [CreateAssetMenu(menuName = "xLib/Node/Unity/AudioClip")]
	public class MonoAudioClip : MonoValue<AudioClip>
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
#endif