#if xLibv3
using System.Collections.Generic;
using UnityEngine;
using xLib.EventClass;

namespace xLib.xNew
{
	public class BackButtonHelper : BaseRegisterM
	{
		private static List<BackButtonHelper> list = new List<BackButtonHelper>();
		
		protected override bool OnRegister(bool value)
		{
			if(value) list.Add(this);
			else list.Remove(this);
			return value;
		}
		
		public void CallFirst()
		{
			if(list.Count==0) return;
			list[0].Call();
		}
		
		public void CallLast()
		{
			if(list.Count==0) return;
			list[list.Count-1].Call();
		}
		
		[SerializeField]private EventUnity onCall = new EventUnity();
		private void Call()
		{
			onCall.Invoke();
		}
	}
}
#endif