#if xLibv3
using UnityEngine;

namespace xLib.libLua
{
	public class LuaM : BaseWorkM
	{
		[SerializeField]private TextAsset luaScript = null;
		public void Call()
		{
			//initalize first!
			xValueClass.ValueInt.GetGlobal("DefaultGlobal").Value = 5;
			Debug.Log(xValueClass.ValueInt.GetGlobal("DefaultGlobal").Value);
			
			#if xLua
			xLuaInit.luaEnv.DoString(luaScript.text);
			#endif
		}
	}
}
#endif