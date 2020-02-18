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
		
		[TextArea(20,100)]
		[SerializeField]private string @script;
		public void Call(string functionName)
		{
			//Local
			LuaTable envLocal = xLua.envLua.NewTable();
			
			//Meta
			LuaTable envMeta = xLua.envLua.NewTable();
			envMeta.Set("__index",xLua.envLua.Global);
			envLocal.SetMetaTable(envMeta);
			envMeta.Dispose();
			
			//Injection
			envLocal.Set("self", this);
			
			//Parse
			// xLua.envLua.DoString(luaScript.text,luaScript.name,env:envLocal);
			xLua.envLua.DoString(script,env:envLocal);
			
			//Call
			envLocal.Get<Action>(functionName)();
			
			//Dispose
			envLocal.Dispose();
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