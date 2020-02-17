#if xLibv3
#if ModAudio
using System;
using UnityEngine;

namespace xLib.xValueClass
{
	[Serializable]
	public class ValueAudioClip : xValueEqual<AudioClip>
	{
		#region Global
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		private static void SetDefaultGlobal()
		{
			ValueAudioClip global = new ValueAudioClip();
			global.Globalize();
		}
		#endregion
		
		#region Compare
		protected override bool IsEqual(AudioClip value)
		{
			if(value.xHashCode() != Value.xHashCode()) return false;
			return (value == Value);
		}
		#endregion
	}
}
#endif
#endif