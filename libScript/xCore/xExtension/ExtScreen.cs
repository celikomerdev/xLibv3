#if xLibv3
using UnityEngine;

namespace xLib
{
	public static class ExtScreen
	{
		public static float dpi
		{
			get
			{
				float value = Screen.dpi;
				if(value<=0) value = 160;
				return value;
			}
		}
		
		public static float HeightInc
		{
			get
			{
				return Screen.height/ExtScreen.dpi;
			}
		}
		
		public static float WidthInc
		{
			get
			{
				return Screen.width/ExtScreen.dpi;
			}
		}
	}
}
#endif