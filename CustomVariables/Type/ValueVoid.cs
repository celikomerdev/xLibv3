#if xLibv2
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace xLib.xValueClass
{
	[System.Serializable]
	public class ValueVoid : IAnalyticsSend
	{
		[SerializeField]internal NodeSetting nodeSetting = new NodeSetting();
		
		#region SetActive
		private bool isInit;
		internal void Init(bool init)
		{
			if(isInit == init) return;
			isInit = init;
			Init();
		}
		
		private void Init()
		{
			if(nodeSetting.canDebug) Debug.LogFormat(nodeSetting.objDebug,nodeSetting.objDebug.name+":Init:{0}",isInit);
			CleanListener();
		}
		#endregion
		
		
		#region Call
		public virtual void Call()
		{
			if(nodeSetting.UseRpc && !MnRPC.inRpc)
			{
				if(MnPlayer.IsMy)
				{
					CallMulti();
					return;
				}
			}
			
			CallClient();
		}
		
		internal void CallMulti()
		{
			if(nodeSetting.canDebug) Debug.LogFormat(nodeSetting.objDebug,nodeSetting.objDebug.name+":CallMulti:{0}",MnPlayer.CurrentId);
			MnPhoton.ins.RPC(nodeSetting.RpcTarget,nodeSetting.Key,"");
		}
		
		private void CallClient()
		{
			if(nodeSetting.canDebug) Debug.LogFormat(nodeSetting.objDebug,nodeSetting.objDebug.name+":CallClient:{0}",MnPlayer.CurrentId);
			
			string tempId = MnPlayer.CurrentId;
			
			actionSortedBase.Invoke();
			AnalyticsRegister();
			
			MnPlayer.CurrentId = tempId;
		}
		
		private bool analyticsRegister;
		private void AnalyticsRegister()
		{
			if(analyticsRegister) return;
			if(nodeSetting.analytics==AnalyticsType.Disabled) return;
			if(!MnPlayer.IsMy) return;
			
			analyticsRegister = true;
			StAnalytics.arrayAnalytics.Add(this);
		}
		
		void IAnalyticsSend.AnalyticsSend()
		{
			if(nodeSetting.canDebug) Debug.LogFormat(nodeSetting.objDebug,nodeSetting.objDebug.name+":AnalyticsSend:{0}",MnPlayer.CurrentId);
			if(nodeSetting.analytics==AnalyticsType.Name) StAnalytics.LogEvent("Void",nodeSetting.objDebug.name);
			else StAnalytics.LogEvent("Void",nodeSetting.Key);
			analyticsRegister = false;
		}
		#endregion
		
		
		#region Listener
		private ActionSortedBase actionSortedBase = new ActionSortedSingle();
		public void Listener(bool register,UnityAction call,bool onRegister=false)
		{
			actionSortedBase.Listener(register,call);
			
			if(!register) return;
			if(!onRegister) return;
			call.Invoke();
		}
		
		
		#if UNITY_EDITOR
		private List<BaseActiveM> listenerEditor = new List<BaseActiveM>();
		public void ListenerEditor(bool addition,BaseActiveM call)
		{
			call.CheckErrors();
			if(addition)
			{
				listenerEditor.Add(call);
			}
			else
			{
				listenerEditor.Remove(call);
			}
		}
		#else
		public void ListenerEditor(bool addition,BaseActiveM call){}
		#endif
		
		#region Clean
		public void CleanListener()
		{
			if(nodeSetting.isMulti) actionSortedBase = new ActionSortedMulti();
			else actionSortedBase = new ActionSortedSingle();
			
			#if UNITY_EDITOR
			listenerEditor = new List<BaseActiveM>();
			#endif
		}
		#endregion
		
		#endregion
	}
}
#endif