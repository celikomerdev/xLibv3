#if xLibv3
#if xLua
using UnityEngine;
using XLua;

namespace xLib.libLua
{
	public static class xLuaInit
	{
		public static LuaEnv luaEnv = null;
		
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		private static void InitEnvironment()
		{
			luaEnv = new LuaEnv();
		}
	}
}
#endif
#endif