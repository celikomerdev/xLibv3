#if xLibv3
using UnityEngine;

namespace xLib
{
	public class WaitForFrames : CustomYieldInstruction
	{
		private readonly int targetFrameCount;
		
		public WaitForFrames(int frame)
		{
			targetFrameCount = Time.frameCount+frame;
		}
		
		public override bool keepWaiting
		{
			get
			{
				return Time.frameCount<targetFrameCount;
			}
		}
	}
}
#endif