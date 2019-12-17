#if xLibv3
using UnityEngine;

namespace xLib
{
	public static class ExtRect
	{
		public static void SetRect(this Rect rect,Rect target)
		{
			rect.Set(target.x,target.y,target.width,target.height);
		}
	}
}
#endif