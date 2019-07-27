#if xLibv3
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace xLib
{
	public abstract class xValue<V> : IAnalyticsSend
	{
		[SerializeField]internal NodeSetting nodeSetting = new NodeSetting();
		
		#region Field
		[Header("Value")]
		[SerializeField]internal V value = default(V);
		#if UNITY_EDITOR
		private V valueDebug;
		#endif
		
		private ValueBase<V> valueBase = new ValueSingle<V>();
		internal V ValueDefault
		{
			get
			{
				return valueBase.valueDefault;
			}
			set
			{
				valueBase.valueDefault = value;
			}
		}
		
		
		#endregion
		
		
		#region Init
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
			
			CleanValue();
			CleanListener();
			
			#if UNITY_EDITOR
			valueDebug = this.value;
			#endif
			
			ValueDefault = this.value;
			valueBase.Value = this.value;
			valueCache = this.value;
		}
		#endregion
		
		
		#region Call
		private void CallClient()
		{
			if(nodeSetting.canDebug) Debug.LogFormat(nodeSetting.objDebug,nodeSetting.objDebug.name+":CallClient:{0}:{1}",ViewCore.CurrentId,ValueToString);
			
			string tempId = ViewCore.CurrentId;
			
			actionSortedBase.Invoke(Value);
			actionSortedBaseCall.Invoke();
			AnalyticsRegister();
			
			ViewCore.CurrentId = tempId;
		}
		
		private bool analyticsRegister;
		private void AnalyticsRegister()
		{
			if(analyticsRegister) return;
			if(nodeSetting.analytics==AnalyticsType.Disabled) return;
			if(!ViewCore.IsMy) return;
			
			analyticsRegister = true;
			ViewCore.arrayAnalytics.Add(this);
		}
		
		#region VirtualAnalytics
		protected virtual string AnalyticLabel
		{
			get
			{
				return ValueToString;
			}
		}
		
		protected virtual string AnalyticValue
		{
			get
			{
				return "0";
			}
		}
		#endregion
		
		void IAnalyticsSend.AnalyticsSend()
		{
			if(nodeSetting.canDebug) Debug.LogFormat(nodeSetting.objDebug,nodeSetting.objDebug.name+":AnalyticsSend:{0}:{1}",ViewCore.CurrentId,ValueToString);
			
			if(nodeSetting.analytics==AnalyticsType.Name) ViewCore.LogEvent("Value",nodeSetting.objDebug.name,AnalyticLabel,AnalyticValue);
			else ViewCore.LogEvent("Value",nodeSetting.Key,AnalyticLabel,AnalyticValue);
			
			analyticsRegister = false;
		}
		#endregion
		
		
		#region Listener
		#region Runtime
		private ActionSortedBase<V> actionSortedBase = new ActionSortedSingle<V>();
		public void Listener(bool register,UnityAction<V> call,bool onRegister=false)
		{
			actionSortedBase.Listener(register,call);
			
			if(!register) return;
			if(!onRegister) return;
			call.Invoke(Value);
		}
		
		private ActionSortedBase actionSortedBaseCall = new ActionSortedSingle();
		public void ListenerCall(bool register,UnityAction call,bool onRegister=false)
		{
			actionSortedBaseCall.Listener(register,call);
			
			if(!register) return;
			if(!onRegister) return;
			call.Invoke();
		}
		#endregion
		
		#region Editor
		#if UNITY_EDITOR
		private List<BaseActiveM> listenerEditor = new List<BaseActiveM>();
		public void ListenerEditor(bool addition,BaseActiveM call)
		{
			call.CheckErrors();
			if(addition)
			{
				if(listenerEditor.Contains(call))
				{
					xDebug.LogExceptionFormat(nodeSetting.objDebug,nodeSetting.objDebug.name+":ListenerEditorAdd:{0}",call.name);
					return;
				}
				listenerEditor.Add(call);
			}
			else
			{
				if(!listenerEditor.Contains(call))
				{
					xDebug.LogExceptionFormat(nodeSetting.objDebug,nodeSetting.objDebug.name+":ListenerEditorRemove:{0}",call.name);
					return;
				}
				listenerEditor.Remove(call);
			}
		}
		#else
		public void ListenerEditor(bool addition,BaseActiveM call){}
		#endif
		#endregion
		
		#region Clean
		private void CleanValue()
		{
			if(nodeSetting.isMulti) valueBase = new ValueMulti<V>();
			else valueBase = new ValueSingle<V>();
		}
		
		public void CleanListener()
		{
			if(nodeSetting.isMulti)
			{
				actionSortedBase = new ActionSortedMulti<V>();
				actionSortedBaseCall = new ActionSortedMulti();
			}
			else
			{
				actionSortedBase = new ActionSortedSingle<V>();
				actionSortedBaseCall = new ActionSortedSingle();
			}
			
			#if UNITY_EDITOR
			listenerEditor = new List<BaseActiveM>();
			#endif
		}
		#endregion
		#endregion
		
		
		#region Virtual
		protected virtual string ValueToString
		{
			get
			{
				return Value.ToString();
			}
		}
		
		protected virtual bool CanChange(V value)
		{
			return true;
		}
		
		protected virtual bool CanCache(V value)
		{
			return true;
		}
		
		protected virtual void KeepProperties(V value){}
		#endregion
		
		
		#region Property
		#region Value
		public virtual V Value
		{
			get
			{
				return valueBase.Value;
			}
			set
			{
				if(!CanChange(value))
				{
					if(nodeSetting.canDebug) Debug.LogWarningFormat(nodeSetting.objDebug,nodeSetting.objDebug.name+":!CanChange:{0}:{1}",ViewCore.CurrentId,value.ToString());
					return;
				}
				KeepProperties(value);
				
				#if UNITY_EDITOR
				if(ViewCore.IsMy) valueDebug = value;
				#endif
				
				valueBase.Value = value;
				
				if(nodeSetting.UseRpc && !ViewCore.inRpc)
				{
					if(ViewCore.IsMy)
					{
						CallMulti();
						return;
					}
				}
				
				CallClient();
			}
		}
		
		internal void CallMulti()
		{
			if(nodeSetting.canDebug) Debug.LogFormat(nodeSetting.objDebug,nodeSetting.objDebug.name+":CallMulti:{0}:{1}",ViewCore.CurrentId,ValueToString);
			ViewCore.RPC(nodeSetting.RpcTarget,nodeSetting.Key,SerializedObject.ToString());
		}
		
		private V valueCache;
		public virtual V ValueCache
		{
			get
			{
				return valueCache;
			}
			set
			{
				if(!CanCache(value)) return;
				valueCache = value;
			}
		}
		#endregion
		
		
		#region ValueAdd
		public virtual V ValueAdd
		{
			set
			{
				//Value = value;
			}
		}
		#endregion
		
		
		#region ValueUpdate
		public void Refresh()
		{
			Value = valueCache;
		}
		
		public void Consume()
		{
			Value = valueCache;
			valueCache = ValueDefault;
		}
		#endregion
		#endregion
		
		
		#region SerializedObject
		internal virtual object SerializedObject
		{
			get
			{
				JToken jToken;
				if(Value.Equals(null)) jToken = JToken.FromObject("");
				else jToken = JToken.FromObject(Value);
				return jToken;
				
				// JsonSerializer jsonSerializer = new JsonSerializer();
				// jsonSerializer.Formatting = Formatting.Indented;
				// if(Value.Equals(null)) jToken = JToken.FromObject("",jsonSerializer);
				// else jToken = JToken.FromObject(Value,jsonSerializer);
			}
			set
			{
				if(value==null) return;
				if(string.IsNullOrEmpty(value.ToString())) return;
				JToken jToken = JToken.FromObject(value);
				Value = jToken.ToObject<V>();
			}
		}
		
		internal virtual object SerializedObjectName
		{
			get
			{
				return SerializedObject;
			}
		}
		#endregion
	}
}
#endif