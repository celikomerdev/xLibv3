#if xLibv3
namespace xLib
{
	public static class DeviceDisplay
	{
		public static int scaleFactor
		{
			get
			{
				return _scaleFactor();
			}
		}
		
		#if UNITY_EDITOR
		private static int _scaleFactor()
		{
			return 1;
		}
		#elif UNITY_IPHONE
		[System.Runtime.InteropServices.DllImport("__Internal")]
		private static extern int _scaleFactor();
		#else
		private static int _scaleFactor()
		{
			return 1;
		}
		#endif
	}
}
#endif