#if xLibv3
using System;
using UnityEngine;

namespace xLib.xValueClass
{
	[Serializable]
	public class ValueAudioClip : xValueEqual<AudioClip>
	{
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