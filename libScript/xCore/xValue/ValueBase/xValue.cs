#if xLibv3
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace xLib
{
	public abstract class xValue<V>
	{
		[SerializeField]public NodeSetting nodeSetting = new NodeSetting();
		
		#region Field
		public string Key
		{
			get
			{
				return nodeSetting.Key;
			}
			set
			{
				nodeSetting.Key = value;
			}
		}
		
		[Header("Value")]
		[SerializeField]internal V value = default(V);
		#if UNITY_EDITOR
		private V valueDebug;
		#endif
		
		private ValueBase<V> valueBase = new ValueSingle<V>();
		public V ValueDefault
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
		public void Init(bool init)
		{
			if(isInit == init) return;
			isInit = init;
			if(nodeSetting.canDebug) Debug.LogFormat(nodeSetting.objDebug,nodeSetting.objDebug.name+":Init:{0}",isInit);
			OnInit(init);
		}
		
		protected virtual void OnInit(bool value)
		{
			CleanValue();
			CleanListener();
			
			#if UNITY_EDITOR
			valueDebug = this.value;
			#endif
			
			ValueDefault = this.value;
			valueBase.ValueSet(this.value,"Client");
			valueCache = this.value;
		}
		#endregion
		
		
		#region Call
		private void CallClient()
		{
			if(nodeSetting.canDebug) Debug.LogFormat(nodeSetting.objDebug,nodeSetting.objDebug.name+":CallClient:{0}",ValueToString);
			actionSortedBase.Invoke(Value,viewId:"Client");
			actionSortedBaseCall.Invoke(Value,viewId:"Client");
			analyticDirty = true;
		}
		
		private void CallClientFirst()
		{
			if(nodeSetting.canDebug) Debug.LogFormat(nodeSetting.objDebug,nodeSetting.objDebug.name+":CallClientFirst:{0}",ValueToString);
			actionSortedBase.InvokeFirst(Value,viewId:"Client");
			actionSortedBaseCall.InvokeFirst(Value,viewId:"Client");
			analyticDirty = true;
		}
		
		private void CallClientLast()
		{
			if(nodeSetting.canDebug) Debug.LogFormat(nodeSetting.objDebug,nodeSetting.objDebug.name+":CallClientLast:{0}",ValueToString);
			actionSortedBase.InvokeLast(Value,viewId:"Client");
			actionSortedBaseCall.InvokeLast(Value,viewId:"Client");
			analyticDirty = true;
		}
		#endregion
		
		
		#region Listener
		#region Runtime
		private ActionSortedBase<V> actionSortedBase = new ActionSortedSingle<V>();
		public void Listener(bool register,UnityAction<V> call,string viewId,int order,bool onRegister=false)
		{
			actionSortedBase.Listener(register,call,viewId,order);
			
			if(!register) return;
			if(!onRegister) return;
			call.Invoke(Value);
		}
		
		private ActionSortedBase<object> actionSortedBaseCall = new ActionSortedSingle<object>();
		public virtual void ListenerCall(bool register,UnityAction<object> call,string viewId,int order,bool onRegister=false)
		{
			actionSortedBaseCall.Listener(register,call,viewId,order);
			
			if(!register) return;
			if(!onRegister) return;
			call.Invoke(Value);
		}
		#endregion
		
		#region Editor
		#if UNITY_EDITOR
		private List<BaseActiveM> listenerEditor = new List<BaseActiveM>();
		public virtual void ListenerEditor(bool register,BaseActiveM call)
		{
			call.CheckErrors();
			if(register)
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
		public virtual void ListenerEditor(bool addition,BaseActiveM call){}
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
				actionSortedBaseCall = new ActionSortedMulti<object>();
			}
			else
			{
				actionSortedBase = new ActionSortedSingle<V>();
				actionSortedBaseCall = new ActionSortedSingle<object>();
			}
			
			#if UNITY_EDITOR
			listenerEditor = new List<BaseActiveM>();
			#endif
		}
		#endregion
		#endregion
		
		
		#region Virtual
		public virtual string ValueToString
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
				return valueBase.ValueGet(ViewCore.CurrentId);
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
				
				valueBase.ValueSet(value,ViewCore.CurrentId);
				
				if(nodeSetting.UseRpc && !ViewCore.inRpc)
				{
					if(ViewCore.IsMy)
					{
						Call();
						return;
					}
				}
				
				CallClient();
			}
		}
		
		public virtual void Call()
		{
			if(!nodeSetting.UseRpc) CallClient();
			else
			{
				if(nodeSetting.canDebug) Debug.LogFormat(nodeSetting.objDebug,nodeSetting.objDebug.name+":Call:{0}:{1}",ViewCore.CurrentId,ValueToString);
				ViewCore.RPC(nodeSetting.RpcTarget,Key,SerializedObject.ToString());
			}
		}
		
		public virtual void CallFirst()
		{
			if(!nodeSetting.UseRpc) CallClientFirst();
			else
			{
				if(nodeSetting.canDebug) Debug.LogFormat(nodeSetting.objDebug,nodeSetting.objDebug.name+":CallFirst:{0}:{1}",ViewCore.CurrentId,ValueToString);
				ViewCore.RPC(nodeSetting.RpcTarget,Key,SerializedObject.ToString());
			}
		}
		
		public virtual void CallLast()
		{
			if(!nodeSetting.UseRpc) CallClientLast();
			else
			{
				if(nodeSetting.canDebug) Debug.LogFormat(nodeSetting.objDebug,nodeSetting.objDebug.name+":CallLast:{0}:{1}",ViewCore.CurrentId,ValueToString);
				ViewCore.RPC(nodeSetting.RpcTarget,Key,SerializedObject.ToString());
			}
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
				Value = value;
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
		public virtual object SerializedObject
		{
			get
			{
				JToken jToken;
				if(Value.Equals(null)) jToken = JToken.FromObject("");
				else jToken = JToken.FromObject(Value);
				return jToken;
			}
			set
			{
				if(value==null) return;
				if(string.IsNullOrEmpty(value.ToString())) return;
				JToken jToken = JToken.FromObject(value);
				Value = jToken.ToObject<V>();
			}
		}
		
		public virtual object SerializedObjectName
		{
			get
			{
				return SerializedObject;
			}
		}
		#endregion
		
		
		#region Analytics
		internal bool analyticDirty = false;
		
		internal virtual string AnalyticString
		{
			get
			{
				return ValueToString;
			}
		}
		
		internal virtual string AnalyticDigit
		{
			get
			{
				return "0";
			}
		}
		#endregion
	}
}
#endif