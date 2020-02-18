#if xLibv3
#if xLua
using System;
using UnityEngine;
using XLua;

namespace xLib.libLua
{
	public class LuaM : BaseWorkM
	{
		[SerializeField]private TextAsset luaScript = null;
		public void Call(string functionName)
		{
			//Local
			LuaTable envScrip = xLua.envLua.NewTable();
			
			//Meta
			LuaTable envMeta = xLua.envLua.NewTable();
			envMeta.Set("__index",xLua.envLua.Global);
			envScrip.SetMetaTable(envMeta);
			envMeta.Dispose();
			
			//Injection
			envScrip.Set("self", this);
			
			//Parse
			xLua.envLua.DoString(luaScript.text,luaScript.name,env:envScrip);
			
			//Call
			envScrip.Get<Action>(functionName)();
			
			//Dispose
			envScrip.Dispose();
		}
	}
}
#else
using UnityEngine;

namespace xLib.libLua
{
	public class LuaM : BaseWorkM
	{
		[SerializeField]private TextAsset luaScript = null;
		public void Call(string functionName)
		{
			Debug.Log($"LuaM:{functionName}");
		}
	}
}
#endif
#endif