#if xLibv3
#if xLua
using UnityEngine;
using XLua;

namespace xLib.libLua
{
	public static class xLua
	{
		public static LuaEnv envLua = null;
		
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		private static void InitEnvironment()
		{
			envLua = new LuaEnv();
		}
	}
}
#endif
#endif